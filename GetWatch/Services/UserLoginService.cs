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

        public UserLoginService(CustomAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }
        
        private GetWatchContext? Context;
        private IRepositoryFactory? Factory;
        private IUnitOfWork? UnitOfWork;
        private IRepository<DbUser>? UserRepository;

        public async Task Userlogin(DbUser user)
    {
            Context = new GetWatchContext();
            Context.Database.EnsureCreated();

            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

            UserRepository = UnitOfWork.GetRepository<DbUser>();
            var existingEmails = UserRepository?.GetAll().Select(u => u.Email).ToList() ?? new List<string>();

            var emailExistence = new EmailExistenceHandler(existingEmails);
            var storedUser = UserRepository?.GetAll().FirstOrDefault(u => u.Email == user.Email);
            if (storedUser == null)
            {
                throw new ArgumentNullException(nameof(storedUser), "Stored user cannot be null.");
            }
            var passwordCorrespondence = new PasswordCorrespondenceHandler(storedUser);
            emailExistence.SetNext(passwordCorrespondence);
            emailExistence.Handle(user);
            await _authStateProvider.NotifyUserAuthentication(user.Email);
            Console.WriteLine("User login successful!");
        
        
    }
    
        
    }
}