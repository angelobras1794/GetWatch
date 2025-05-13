using System;

namespace GetWatch.Services.User;
using System.Collections.Generic;
using GetWatch.Interfaces.ShoppingCart; // Add the namespace for IShoppingCartMapper
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.User;
using GetWatch.Services.Db;
using GetWatch.Services.Db.Purchases;
using GetWatch.Services.Db.CartItem;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.Tickets;



public class UserMapper : IUserMapper
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShoppingCartMapper _cartMapper;
    private readonly ISupportTicketMapper _supportTicketMapper;
    private readonly ICartItemMapper _transactionMapper;

    public UserMapper(IUnitOfWork unitOfWork, IShoppingCartMapper cartMapper, ISupportTicketMapper supportTicketMapper, ICartItemMapper transactionMapper)
    {
        _unitOfWork = unitOfWork;
        _cartMapper = cartMapper;
        _supportTicketMapper = supportTicketMapper;
        _transactionMapper = transactionMapper;
    }

    public List<IUser> GetAll()
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUsers = repository.GetAll()?.ToList() ?? new List<DbUser>();

        var users = dbUsers.Select(dbUser => new User
        (
            _unitOfWork,
            dbUser.Name,
            dbUser.Email,
            dbUser.Password,
            dbUser.Phone,
            dbUser.Id
        )
        {
            Cart = _cartMapper.Get(dbUser.Cart.Id), 
            // Tenho de fazer assim o support tickets porque o metodo retorna uma lista de ISupportTicket mas o support ticket no user é uma lista de SupportTicket
           SupportTickets = _supportTicketMapper.GetAll(dbUser.Id)
            .Select(st => (SupportTicket)st)
            .ToList(),
            Transactions = _transactionMapper.GetAll(dbUser.Id) 
        }).ToList();

        return users.Cast<IUser>().ToList();
    }

    public IUser? Get(Guid id)
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUser = repository.Get(id);
        if (dbUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        return new User
        (
            _unitOfWork,
            dbUser.Name,
            dbUser.Email,
            dbUser.Password,
            dbUser.Phone,
            dbUser.Id
        )
        {
            Cart = _cartMapper.Get(dbUser.Cart.Id),
             SupportTickets = _supportTicketMapper.GetAll(dbUser.Id)
            .Select(st => (SupportTicket)st)
            .ToList(), 
            Transactions = _transactionMapper.GetAll(dbUser.Id) 
        };
    }

    public void Update(IUser user)
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUser = repository.Get(user.Id);
        if (dbUser == null)
        {
            throw new KeyNotFoundException($"User with ID {user.Id} not found.");
        }

        dbUser.Name = user.Name;
        dbUser.Email = user.Email;
        dbUser.Password = user.Password;
        dbUser.Phone = user.Phone;

        // Atualiza o carrinho, tickets e transações usando os mapeadores
        if (user.Cart != null)
        {
            _cartMapper.Insert(user.Cart, user.Id);
        }

        if (user.SupportTickets != null)
        {
            foreach (var ticket in user.SupportTickets)
            {
                _supportTicketMapper.Insert(ticket, user.Id);
            }
        }

        if (user.Transactions != null)
        {
            foreach (var transaction in user.Transactions)
            {
                _transactionMapper.Insert(transaction, user.Id);
            }
        }

        _unitOfWork.Begin();
        repository.Update(dbUser);
        _unitOfWork.SaveChanges();
        _unitOfWork.Commit();
    }

    public void Insert(IUser user)
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUser = new DbUser
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Phone = user.Phone
        };

        _unitOfWork.Begin();
        repository.Insert(dbUser);
        _unitOfWork.SaveChanges();

        // Insere o carrinho, tickets e transações usando os mapeadores
        if (user.Cart != null)
        {
            _cartMapper.Insert(user.Cart, user.Id);
        }

        if (user.SupportTickets != null)
        {
            foreach (var ticket in user.SupportTickets)
            {
                _supportTicketMapper.Insert(ticket, user.Id);
            }
        }

        if (user.Transactions != null)
        {
            foreach (var transaction in user.Transactions)
            {
                _transactionMapper.Insert(transaction, user.Id);
            }
        }

        _unitOfWork.Commit();
    }

    public void Remove(Guid id)
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUser = repository.Get(id);
        if (dbUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        var cart = _cartMapper.Get(dbUser.Cart.Id);
        if (dbUser.Cart != null)
        {
            _cartMapper.Remove(cart);
        }

        var tickets = _supportTicketMapper.GetAll(dbUser.Id);
        foreach (var ticket in tickets)
        {
            _supportTicketMapper.Remove(ticket);
        }

        var transactions = _transactionMapper.GetAll(dbUser.Id);
        foreach (var transaction in transactions)
        {
            _transactionMapper.Remove(transaction);
        }

        _unitOfWork.Begin();
        repository.Delete(dbUser);
        _unitOfWork.SaveChanges();
        _unitOfWork.Commit();
    }
}