using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;
using GetWatch.Services.Handlers;
using GetWatch.Interfaces.Db;

namespace GetWatch.Services
{
    public class UserCreationService
    {

        private GetWatchContext? Context;
        private IRepositoryFactory? Factory;
        private IUnitOfWork? UnitOfWork;
        private IRepository<DbUser>? UserRepository;

         public void CreateUser(DbUser user)
{
    try
    {
        Context = new GetWatchContext();
        Context.Database.EnsureCreated();
            
        Factory = new RepositoryFactory(Context);
        UnitOfWork = new UnitOfWork(Context, Factory);

        UserRepository = UnitOfWork.GetRepository<DbUser>();
        if (UserRepository == null)
        {
            throw new InvalidOperationException("UserRepository is not initialized.");
        }

        var existingEmails = UserRepository.GetAll().Select(u => u.Email).ToList();
        var existingUsernames = UserRepository.GetAll().Select(u => u.Name).ToList();

        // Build the chain
        var usernameValidation = new UsernameDuplicationHandler(existingUsernames);
        var emailValidation = new EmailValidationHandler();
        var emailDuplication = new EmailDuplicationHandler(existingEmails);
        var passwordValidation = new PasswordValidationHandler();

        usernameValidation.SetNext(emailValidation);
        emailValidation.SetNext(emailDuplication);
        emailDuplication.SetNext(passwordValidation);

        // Start the chain
        usernameValidation.Handle(user);

        // If all validations pass, proceed with user creation
        UnitOfWork.Begin();
        UserRepository.Insert(user);    
        UnitOfWork.SaveChanges();
        UnitOfWork.Commit();
        Console.WriteLine("User created successfully!");
    }
    catch (Exception ex)
    {
        // Handle the exception (e.g., log it or notify the user)
        Console.WriteLine($"Error: {ex.Message}");
    }
}

        
    }
}