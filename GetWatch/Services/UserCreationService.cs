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

        public UserCreationService(){
            Context = new GetWatchContext();
            Context.Database.EnsureCreated();
            
            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

            UserRepository = UnitOfWork.GetRepository<DbUser>();
        }

         public void CreateUser(DbUser user)
         {
    
            userValidation(user);

           
            UnitOfWork.Begin();
            UserRepository.Insert(user);    
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
       
       }

       public void userValidation(DbUser user)
       {
           var existingEmails = UserRepository.GetAll().Select(u => u.Email).Where(email => email != null).Cast<string>().ToList();
           var existingUsernames = UserRepository.GetAll().Select(u => u.Name).Where(name => name != null).Cast<string>().ToList();

           UserHandler usernameValidation = new UsernameDuplicationHandler(existingUsernames);
           UserHandler emailValidation = new EmailValidationHandler();
           UserHandler emailDuplication = new EmailDuplicationHandler(existingEmails);
           UserHandler passwordValidation = new PasswordValidationHandler();

           usernameValidation.SetNext(emailValidation);
           emailValidation.SetNext(emailDuplication);
           emailDuplication.SetNext(passwordValidation);

           usernameValidation.Handle(user);
       }
       

        
    }
}