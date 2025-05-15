using System;

namespace GetWatch.Services.User;
using System.Collections.Generic;
using GetWatch.Interfaces.ShoppingCart; // Add the namespace for IShoppingCartMapper
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.User;
using GetWatch.Services.Db;
using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.Tickets;



public class UserMapper : IUserMapper
{
    private readonly IUnitOfWork _unitOfWork;
    public  IShoppingCartMapper _cartMapper;
    public  ISupportTicketMapper _supportTicketMapper;
    public  ICartItemMapper _transactionMapper;
    private UserBuilder _userBuilder;
    public UserMapper(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cartMapper = new ShoppingCartMapper(unitOfWork);
        _supportTicketMapper = new SupportTicketMapper(unitOfWork);
        _transactionMapper = new CartItemMapper(unitOfWork);
        _userBuilder = new UserBuilder();
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
            dbUser.Name ?? string.Empty,
            dbUser.Email ?? string.Empty,
            dbUser.Password ?? string.Empty,
            dbUser.Phone,
            dbUser.Id
        )
        {
            Cart = _cartMapper.Get(dbUser.Cart.Id), 
           SupportTickets = _supportTicketMapper.GetAll(dbUser.Id),
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
            dbUser.Name ?? string.Empty,
            dbUser.Email ?? string.Empty,
            dbUser.Password ?? string.Empty,
            dbUser.Phone,
            dbUser.Id
        )
        {
            Cart = _cartMapper.Get(dbUser.Id),
            SupportTickets = _supportTicketMapper.GetAll(dbUser.Id),
            Transactions = _transactionMapper.GetAll(dbUser.Id)
        };
    
    }
    

    public void Insert(IUser user)
    {
        var repository = _unitOfWork.GetRepository<DbUser>();
        if (repository == null)
        {
            throw new InvalidOperationException("Repository for DbUser is null.");
        }

        var dbUser = _userBuilder.SetUserEmail(user.Email?? string.Empty).SetUserName(user.Name ?? string.Empty)
            .SetUserPassword(user.Password ?? string.Empty).SetUserPhone(user.Phone?? string.Empty).BuildUser();

        _unitOfWork.Begin();
        repository.Insert(dbUser);
        _unitOfWork.SaveChanges();

        
        InsertCart(user);
        InsertSupportTickets(user);
        InsertTransactions(user);
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

        RemoveCart(dbUser);
        RemoveSupportTickets(dbUser);
        RemoveTransactions(dbUser);

        _unitOfWork.Begin();
        repository.Delete(dbUser);
        _unitOfWork.SaveChanges();
        _unitOfWork.Commit();
    }

    private void RemoveCart(DbUser dbUser)
    {
        if (dbUser.Cart != null)
        {
            var cart = _cartMapper.Get(dbUser.Cart.Id);
            if (cart != null)
            {
                _cartMapper.Remove(cart);
            }
        }
    }
    private void RemoveSupportTickets(DbUser dbUser)
    {
        var tickets = _supportTicketMapper.GetAll(dbUser.Id);
        foreach (var ticket in tickets)
        {
            _supportTicketMapper.Remove(ticket);
        }
    }
    private void RemoveTransactions(DbUser dbUser)
    {
        var transactions = _transactionMapper.GetAll(dbUser.Id);
        foreach (var transaction in transactions)
        {
            _transactionMapper.Remove(transaction);
        }
    }

    public void InsertCart(IUser user)
    {
        if (user.Cart == null)
        {
            throw new InvalidOperationException("User's cart is null.");
        }
        _cartMapper.Insert(user.Cart, user.Id);
    }
    public void InsertSupportTickets(IUser user)
    {
        
        var userRepository = _unitOfWork.GetRepository<DbUser>();
        
        var dbUser = userRepository.Get(user.Id);
        
        foreach (var ticket in user.SupportTickets)
        {
            _supportTicketMapper.Insert(ticket, dbUser);
        }
    }
    public void InsertTransactions(IUser user)
    {
        if (user.Transactions == null)
        {
            throw new InvalidOperationException("User's transactions are null.");
        }
        foreach (var transaction in user.Transactions)
        {
            _transactionMapper.Insert(transaction, user.Id);
        }
    }

}