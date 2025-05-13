using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.User;
using GetWatch.Interfaces.ShoppingCart;

using GetWatch.Interfaces.SupportTickets;

using GetWatch.Services.Db;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Services.User.Tests
{
    public class UserMapperTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<DbUser>> _userRepositoryMock;
        private readonly Mock<IShoppingCartMapper> _cartMapperMock;
        private readonly Mock<ISupportTicketMapper> _supportTicketMapperMock;
        private readonly Mock<ICartItemMapper> _transactionMapperMock;
        private readonly UserMapper _userMapper;

        public UserMapperTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IRepository<DbUser>>();
            _cartMapperMock = new Mock<IShoppingCartMapper>();
            _supportTicketMapperMock = new Mock<ISupportTicketMapper>();
            _transactionMapperMock = new Mock<ICartItemMapper>();

            _unitOfWorkMock.Setup(u => u.GetRepository<DbUser>()).Returns(_userRepositoryMock.Object);

            _userMapper = new UserMapper(_unitOfWorkMock.Object)
            {
                _cartMapper = _cartMapperMock.Object,
                _supportTicketMapper = _supportTicketMapperMock.Object,
                _transactionMapper = _transactionMapperMock.Object
            };
        }

        [Fact]
        public void Get_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dbUser = new DbUser
            {
                Id = userId,
                Name = "Test User",
                Email = "test@example.com",
                Password = "password",
                Phone = "123456789"
            };

            _userRepositoryMock.Setup(r => r.Get(userId)).Returns(dbUser);
            _cartMapperMock.Setup(c => c.Get(dbUser.Cart.Id)).Returns(new GetWatch.Services.ShoppingCart.ShoppingCart());
            _supportTicketMapperMock.Setup(s => s.GetAll(userId)).Returns(new List<ISupportTicket>());
            _transactionMapperMock.Setup(t => t.GetAll(userId)).Returns(new List<ICartItem>());

            // Act
            var result = _userMapper.Get(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbUser.Name, result.Name);
            Assert.Equal(dbUser.Email, result.Email);
            Assert.Equal(dbUser.Phone, result.Phone);
        }

        [Fact]
        public void Get_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.Get(userId)).Returns((DbUser?)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _userMapper.Get(userId));
        }

        [Fact]
        public void Get_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.GetRepository<DbUser>()).Returns((IRepository<DbUser>?)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _userMapper.Get(Guid.NewGuid()));
        }
    }
}