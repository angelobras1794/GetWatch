using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;

namespace GetWatch.Services.Tickets
{
    public class SupportTicketMapper : ISupportTicketMapper
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISupportTicketBuilder _supportBuilder = new SupportTicketBuilder();

        public SupportTicketMapper(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;

        }

        public List<ISupportTicket> GetAll(Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbSupportTickets>();
            if (repository == null)
            {
                return new List<ISupportTicket>();
            }
            // fetch all support tickets of the user
            var dbSupportTickets = repository.GetAll()?.Where(ticket => ticket.UserId == userId).ToList() ?? new List<DbSupportTickets>();

            // Transform DbServer items into IServer instances
            var supportTickets = dbSupportTickets.Select(dbSupportTickets =>
                _supportBuilder.SetId(dbSupportTickets.Id)
                    .SetSubject(dbSupportTickets.Subject)
                    .SetDescription(dbSupportTickets.Description)
                    .SetResolved(dbSupportTickets.IsResolved)
                    .Build()).ToList();

            return supportTickets;
        }

        public ISupportTicket? Get(Guid id)
        {
            var repository = _unitOfWork.GetRepository<DbSupportTickets>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbSupportTikcets is null.");
            }
            var dbSupportTicket = repository.Get(id);
            if (dbSupportTicket == null)
            {
                throw new KeyNotFoundException($"Support with ID {id} not found.");
            }

            var supportTicket = _supportBuilder.SetId(dbSupportTicket.Id)
                .SetSubject(dbSupportTicket.Subject)
                .SetDescription(dbSupportTicket.Description)
                .SetResolved(dbSupportTicket.IsResolved)
                .Build();
            return supportTicket;
        }

        public void Insert(ISupportTicket supportTicket, DbUser dbUser)
        {
            var userId = dbUser.Id;
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            }
            if (supportTicket == null)
            {
                throw new ArgumentNullException(nameof(supportTicket), "Support ticket cannot be null.");
            }
            {
                var repository = _unitOfWork.GetRepository<DbSupportTickets>();
                if (repository == null)
                {
                    throw new InvalidOperationException("Repository for DbServer is null.");
                }
                var dbSupportTicket = new DbSupportTickets
                {
                    Id = supportTicket.Id,
                    Subject = supportTicket.Subject,
                    Description = supportTicket.Description,
                    IsResolved = supportTicket.IsResolved,
                    UserId = dbUser.Id, // Assuming ISupportTicket has a UserId property
                    User = dbUser
                };
                _unitOfWork.Begin();
                repository.Insert(dbSupportTicket);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
        }

        public void Remove(ISupportTicket supportTicket)
        {
            var repository = _unitOfWork.GetRepository<DbSupportTickets>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbServer is null.");
            }
            var dbSupportTickets = repository.Get(supportTicket.Id);
            if (dbSupportTickets == null)
            {
                throw new KeyNotFoundException($"support with ID {supportTicket.Id} not found.");
            }
            _unitOfWork.Begin();
            repository.Delete(dbSupportTickets);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        
    }

}    
