using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using GetWatch.Interfaces.Compra;
using GetWatch.Services.Db.Purchases;
using GetWatch.Services.Movies;


namespace GetWatch.Services.Bilhete
{
    public class BilheteMapper : IBilheteMapper
    {
        private readonly IUnitOfWork _unitOfWork;


        public BilheteMapper(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;

        }

        public List<IBilhete> GetAll(Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbTicketPurchase>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbTicketPurchase is null.");
            }
            // fetch all support tickets of the user
            var dbbilhetes = repository.GetAll()?.Where(ticket => ticket.UserId == userId).ToList() ?? new List<DbTicketPurchase>();

            // Transform DbServer items into IServer instances
            var bilhetes = dbbilhetes.Select(dbbilhetes => new BilheteBase
            {
                Id = dbbilhetes.Id,
                PersonAmount = dbbilhetes.PersonAmount,
                Seats = dbbilhetes.Seats
            }).ToList();
            

            return bilhetes.Cast<IBilhete>().ToList();
        }

        public IBilhete? Get(Guid id)
        {
            var repository = _unitOfWork.GetRepository<DbTicketPurchase>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbTicketPurchase is null.");
            }
            var dbbilhetes = repository.Get(id);
            if (dbbilhetes == null)
            {
                throw new KeyNotFoundException($"Support with ID {id} not found.");
            }

             var bilhetes = new BilheteBase
            {
                Id = dbbilhetes.Id,
                PersonAmount = dbbilhetes.PersonAmount,
                Seats = dbbilhetes.Seats
            };
            return bilhetes;
        }

        public void Insert(IBilhete bilhete,Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbTicketPurchase>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbServer is null.");
            }
            var dbBilhetes = new DbTicketPurchase
            {
                Id = bilhete.Id,
                PersonAmount = bilhete.PersonAmount,
                Seats = bilhete.Seats,
                UserId = userId // Assuming IBilhete has a UserId property
            };
         
            _unitOfWork.Begin();
            repository.Insert(dbBilhetes);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }

        public void Remove(IBilhete bilhete)
        {
            var repository = _unitOfWork.GetRepository<DbTicketPurchase>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbServer is null.");
            }
            var dbBilhetes = repository.Get(bilhete.Id);
            if (dbBilhetes == null)
            {
                throw new KeyNotFoundException($"support with ID {bilhete.Id} not found.");
            }
            _unitOfWork.Begin();
            repository.Delete(dbBilhetes);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        
    }

}    
