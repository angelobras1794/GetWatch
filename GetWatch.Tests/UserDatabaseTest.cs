// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using GetWatch.Interfaces.Db;
// using GetWatch.Services.Db;
// using GetWatch.Services.Handlers;


// namespace GetWatch.Tests
// {
//     public class UserDatabaseTest
//     {
//         DbUser User;
//         GetWatchContext Context;
//         IRepositoryFactory Factory;
//         IUnitOfWork UnitOfWork;
//         IRepository<DbUser> UserRepository;
//         UsernameDuplicationHandler   usernameValidation ;
//         EmailValidationHandler emailValidation ;
//         EmailDuplicationHandler emailDuplication ;
//         PasswordValidationHandler passwordValidation ;
//         public UserDatabaseTest()
//         {
//             User = new DbUser {
//                 Id = Guid.NewGuid(),
//                 Name = "TestUser",
//                 Email = "testUser@gmail.com",
//                 Password = "TestPassword"
//             };
        
//             Context = new MemoryGetWatchContext();
//             Context.Database.EnsureCreated();
            
//             Factory = new RepositoryFactory(Context);
//             UnitOfWork = new UnitOfWork(Context, Factory);

//             UserRepository = UnitOfWork.GetRepository<DbUser>();


//             UnitOfWork.Begin();
//             UserRepository.Insert(User);
            
//             UnitOfWork.SaveChanges();
//             UnitOfWork.Commit();

//             var existingEmails = UserRepository.GetAll().Select(u => u.Email).ToList();
//             var existingUsernames = UserRepository.GetAll().Select(u => u.Name).ToList();

//             usernameValidation = new UsernameDuplicationHandler(existingUsernames);
//              emailValidation = new EmailValidationHandler();
//              emailDuplication = new EmailDuplicationHandler(existingEmails);
//              passwordValidation = new PasswordValidationHandler();
//         }

//          [Fact]
//         public void AddUser_WithDuplicateEmail_ShouldThrowException()
//         {
//             // Arrange: Create a new user with the same email
//             var duplicateUser = new DbUser
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "DuplicateUser",
//                 Email = "testUser@gmail.com", // Same email as the existing user
//                 Password = "AnotherPassword"
//             };

//             usernameValidation.SetNext(emailValidation);
//             emailValidation.SetNext(emailDuplication);
//             emailDuplication.SetNext(passwordValidation);

//             var exception = Assert.Throws<Exception>(() =>
//             {
//             usernameValidation.Handle(duplicateUser);
//             });

//             Assert.Equal("Email is already registered.", exception.Message);

            
//         }
//         [Fact]
//         public void AddUser_WithDuplicateUsername_ShouldThrowException()
//         {
//             // Arrange: Create a new user with the same username
//             var duplicateUser = new DbUser
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "TestUser", // Same username as the existing user
//                 Email = "jdadada@gmai.com",
//                 Password = "AnotherPassword"
//             };

//             usernameValidation.SetNext(emailValidation);
//             emailValidation.SetNext(emailDuplication);
//             emailDuplication.SetNext(passwordValidation);

//             var exception = Assert.Throws<Exception>(() =>
//             {
//             usernameValidation.Handle(duplicateUser);
//             });

//             Assert.Equal("Username is already taken.", exception.Message);


//         }
//         [Fact]
//         public void AddUser_WithInvalidEmail_ShouldThrowException()
//         {
//             // Arrange: Create a new user with an invalid email format
//             var invalidEmailUser = new DbUser
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "InvalidEmailUser",
//                 Email = "invalid-email", // Invalid email format
//                 Password = "AnotherPassword"
//             };

//             usernameValidation.SetNext(emailValidation);
//             emailValidation.SetNext(emailDuplication);
//             emailDuplication.SetNext(passwordValidation);

//             var exception = Assert.Throws<Exception>(() =>
//             {
//                 usernameValidation.Handle(invalidEmailUser);
//             });

//             Assert.Equal("Invalid email format.", exception.Message);
//         }

//         //check if a user was added successfully
//         [Fact]
//         public void AddUser_ShouldAddSuccessfully()
//         {
//             // Arrange: Create a new user with a unique email and username
//             var newUser = new DbUser
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "NewUser",
//                 Email = "ola@gmail.com",
//                 Password = "-Secr3t."
//             };

//             usernameValidation.SetNext(emailValidation);
//             emailValidation.SetNext(emailDuplication);
//             emailDuplication.SetNext(passwordValidation);

//             // Act: Handle the new user
//             usernameValidation.Handle(newUser);

//             // Assert: Check if the user was added successfully
//             UnitOfWork.Begin();
//             UserRepository.Insert(newUser);    
//             UnitOfWork.SaveChanges();
//             UnitOfWork.Commit();

//             Assert.Contains(newUser, UserRepository.GetAll().ToList());
//         }

//     }
// }