using Microsoft.EntityFrameworkCore;
using GetWatch.Services.Db;
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

        private SupportTicketMapper supportTicketMapper;

        public SupportTicketService(GetWatchContext context, IRepositoryFactory factory, IUnitOfWork unitOfWork,
            SupportTicketMapper supportTicketMapper)
        {
            Context = context;
            Factory = factory;
            UnitOfWork = unitOfWork;
            TicketsRepository = unitOfWork.GetRepository<DbSupportTickets>();
            this.supportTicketMapper = supportTicketMapper;
        }
        
        public async Task CreateSupportTicketAsync(DbSupportTickets ticket)
        {   
           
                try
                {
                    var userEmail = ticket.User?.Email; 
                    

                    var userRepository = UnitOfWork.GetRepository<DbUser>();
                    var user = userRepository.GetAll().FirstOrDefault(u => u.Email == userEmail);
        
                    if (Context.Entry(user).State == EntityState.Detached)
                    {
                        Context.Attach(user);
                    }

                    ticket.UserId = user.Id;
                    ticket.User = user;

                    UnitOfWork.Begin();
                    if (TicketsRepository == null)
                    {
                        throw new InvalidOperationException("TicketsRepository is not initialized.");
                    }
                    TicketsRepository.Insert(ticket);
                    UnitOfWork.SaveChanges();
                    UnitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }
    }

