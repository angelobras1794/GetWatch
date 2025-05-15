using Microsoft.EntityFrameworkCore;
using GetWatch.Services.Db;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.Handlers;
using GetWatch.Interfaces.Db;

namespace GetWatch.Services.Tickets
{
    public class SupportTicketService
    {
        private GetWatchContext? Context;
        private IRepositoryFactory? Factory;
        private IUnitOfWork? UnitOfWork;
        private IRepository<DbSupportTickets>? TicketsRepository;

        private ISupportTicketMapper supportTicketMapper;

        public SupportTicketService(GetWatchContext context, IRepositoryFactory factory, IUnitOfWork unitOfWork,
            ISupportTicketMapper supportTicketMapper)
        {
            Context = context;
            Factory = factory;
            UnitOfWork = unitOfWork;
            TicketsRepository = unitOfWork.GetRepository<DbSupportTickets>();
            this.supportTicketMapper = supportTicketMapper;
        }
        
        public async Task CreateSupportTicketAsync(SupportTicket ticket,DbUser dbUser)
        {

            try
            {


                var userRepository = UnitOfWork.GetRepository<DbUser>();
                var user = userRepository.GetAll().FirstOrDefault(u => u.Email == dbUser.Email);

                if (Context.Entry(user).State == EntityState.Detached)
                {
                    Context.Attach(user);
                }

                // UnitOfWork.Begin();
                // if (TicketsRepository == null)
                // {
                //     throw new InvalidOperationException("TicketsRepository is not initialized.");
                // }
                // TicketsRepository.Insert(ticket);
                // UnitOfWork.SaveChanges();
                // UnitOfWork.Commit();

                supportTicketMapper.Insert(ticket, dbUser);
                
                }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            }
        }
    }

