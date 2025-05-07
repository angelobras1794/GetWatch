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
        
        public async Task CreateSupportTicketAsync(DbSupportTickets ticket)
        {   
            Context = new GetWatchContext();
            await Context.Database.EnsureCreatedAsync();
                    
            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

                
            TicketsRepository = UnitOfWork.GetRepository<DbSupportTickets>();

                try
                {
                    var userEmail = ticket.User?.Email; 
                    if (string.IsNullOrEmpty(userEmail))
                    {
                        throw new Exception("User email is required to associate the ticket with a user.");
                    }

                    var userRepository = UnitOfWork.GetRepository<DbUser>();
                    var user = userRepository.GetAll().FirstOrDefault(u => u.Email == userEmail);
                    if (user == null)
                    {
                        throw new Exception($"No user found with email: {userEmail}");
                    }

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

