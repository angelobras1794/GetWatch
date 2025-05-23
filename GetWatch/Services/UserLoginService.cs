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
        private readonly CustomAuthenticationStateProvider _authStateProvider;
        private GetWatchContext? Context;
        private IRepositoryFactory? Factory;
        private IUnitOfWork? UnitOfWork;
        private IRepository<DbUser>? UserRepository;

        public UserLoginService(CustomAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
            Context = new GetWatchContext();
            Context.Database.EnsureCreated();
            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);
            UserRepository = UnitOfWork.GetRepository<DbUser>();

        }
        
        

        public async Task Userlogin(DbUser user)
    {
            
            var existingEmails = UserRepository?.GetAll().Select(u => (string?)u.Email).ToList() ?? new List<string?>();

            var emailExistence = new EmailExistenceHandler(existingEmails.Where(email => email != null).Cast<string>().ToList());
            var storedUser = UserRepository?.GetAll().FirstOrDefault(u => u.Email == user.Email);
            if (storedUser == null)
            {
                throw new ArgumentNullException(nameof(storedUser), "Stored user cannot be null.");
            }
            var passwordCorrespondence = new PasswordCorrespondenceHandler(storedUser);
            emailExistence.SetNext(passwordCorrespondence);
            emailExistence.Handle(user);
            
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException(nameof(user.Email), "User email cannot be null or empty.");
            }
            await _authStateProvider.NotifyUserAuthentication(user.Email, user.Id.ToString());
           
        
        
        }
    
        
    }
}