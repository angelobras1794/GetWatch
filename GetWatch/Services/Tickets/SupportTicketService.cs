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
            Context.Database.EnsureCreated();
                    
            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

                
            TicketsRepository = UnitOfWork.GetRepository<DbSupportTickets>();

            // Print the ticket details to the console
            Console.WriteLine($"Ticket ID: {ticket.Id}");
            Console.WriteLine($"User ID: {ticket.UserId}");
            Console.WriteLine($"Subject: {ticket.Subject}");
            Console.WriteLine($"Description: {ticket.Description}");
            Console.WriteLine($"Is Resolved: {ticket.IsResolved}");

            try
{
    // Retrieve the user by email or another unique identifier
    var userEmail = ticket.User?.Email; // Assuming the ticket has a User object with an Email property
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

    // Attach the user to the context to ensure it's treated as an existing entity
    if (Context.Entry(user).State == EntityState.Detached)
    {
        Context.Attach(user);
    }

    // Associate the ticket with the retrieved user
    ticket.UserId = user.Id;
    ticket.User = user;

    UnitOfWork.Begin();
    Console.WriteLine($"Inserting support ticket: {ticket.Id}");
    TicketsRepository.Insert(ticket);
    Console.WriteLine($"Saving changes for support ticket: {ticket.Id}");
    UnitOfWork.SaveChanges();
    Console.WriteLine($"Committing changes for support ticket: {ticket.Id}");
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

