using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using GetWatch.Services.Handlers;

namespace GetWatch.Services
{
    public class UserLoginService
    {
        
        private GetWatchContext? Context;
        private IRepositoryFactory? Factory;
        private IUnitOfWork? UnitOfWork;
        private IRepository<DbUser>? UserRepository;
        

        public void Userlogin(DbUser user)
    {
        try
        {
            Context = new GetWatchContext();
            Context.Database.EnsureCreated();

            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

            UserRepository = UnitOfWork.GetRepository<DbUser>();

            // Build the chain
            var existingEmails = UserRepository?.GetAll().Select(u => u.Email).ToList() ?? new List<string>();

            var emailExistence = new EmailExistenceHandler(existingEmails);
            var storedUser = UserRepository?.GetAll().FirstOrDefault(u => u.Email == user.Email);
            if (storedUser == null)
            {
                throw new ArgumentNullException(nameof(storedUser), "Stored user cannot be null.");
            }
            var passwordCorrespondence = new PasswordCorrespondenceHandler(storedUser);

            emailExistence.SetNext(passwordCorrespondence);

            // Start the chain
            emailExistence.Handle(user);

            // If all validations pass, proceed with login
            Console.WriteLine("User login successful!");
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log it or notify the user)
            Console.WriteLine($"Error during login: {ex.Message}");
        }
    }
        
    }
}