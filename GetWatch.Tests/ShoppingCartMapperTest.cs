using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.Db;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Tests
{
    public class ShoppingCartMapperTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<DbCart>> _mockRepository;
        private readonly Mock<ICartItemMapper> _mockCartItemMapper;
        private readonly ShoppingCartMapper _shoppingCartMapper;

        public ShoppingCartMapperTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<IRepository<DbCart>>();
            _mockCartItemMapper = new Mock<ICartItemMapper>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCart>()).Returns(_mockRepository.Object);
            _shoppingCartMapper = new ShoppingCartMapper(_mockUnitOfWork.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllShoppingCarts()
        {
            // Arrange
            var dbCarts = new List<DbCart>
            {
                new DbCart { Id = Guid.NewGuid(), TotalPrice = 100 },
                new DbCart { Id = Guid.NewGuid(), TotalPrice = 200 }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(dbCarts);
            _mockCartItemMapper.Setup(mapper => mapper.GetAll(It.IsAny<Guid>())).Returns(new List<ICartItem>());

            // Act
            var result = _shoppingCartMapper.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(dbCarts[0].Id, result[0].Id);
            Assert.Equal(dbCarts[1].Id, result[1].Id);
        }



        [Fact]
        public void Get_ShouldReturnNull_WhenShoppingCartNotFound()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.Get(cartId)).Returns((DbCart?)null);

            // Act
            var result = _shoppingCartMapper.Get(cartId);

            // Assert
            Assert.Null(result);
        }

        

        [Fact]
        public void Insert_ShouldInsertShoppingCartSuccessfully()
        {
            // Arrange
            IShoppingCart shoppingCart = new GetWatch.Services.ShoppingCart.ShoppingCart(100,new Guid());
            var userId = Guid.NewGuid();

            // Act
            _shoppingCartMapper.Insert(shoppingCart, userId);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Insert(It.Is<DbCart>(cart =>
                cart.Id == shoppingCart.Id &&
                cart.TotalPrice == shoppingCart.Price &&
                cart.UserId == userId
            )), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Insert_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCart>()).Returns((IRepository<DbCart>?)null);
            var shoppingCart = new GetWatch.Services.ShoppingCart.ShoppingCart(100,new Guid());
            var userId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _shoppingCartMapper.Insert(shoppingCart, userId));
        }

        [Fact]
        public void Remove_ShouldRemoveShoppingCartSuccessfully()
        {
            // Arrange
            var dbCart = new DbCart { Id = Guid.NewGuid(), TotalPrice = 150 };
            _mockRepository.Setup(repo => repo.Get(dbCart.Id)).Returns(dbCart);

            // Act
            _shoppingCartMapper.Remove(new GetWatch.Services.ShoppingCart.ShoppingCart { Id = dbCart.Id });

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Delete(dbCart), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Remove_ShouldThrowKeyNotFoundException_WhenShoppingCartNotFound()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.Get(cartId)).Returns((DbCart?)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _shoppingCartMapper.Remove(new GetWatch.Services.ShoppingCart.ShoppingCart{ Id = cartId }));
        }

        [Fact]
        public void Remove_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCart>()).Returns((IRepository<DbCart>?)null);
            var shoppingCart = new GetWatch.Services.ShoppingCart.ShoppingCart { Id = Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _shoppingCartMapper.Remove(shoppingCart));
        }
    }
}