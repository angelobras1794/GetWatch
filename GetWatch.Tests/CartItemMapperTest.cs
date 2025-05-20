using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.Db.CartItem;
using GetWatch.Services.Db;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Tests
{
    public class CartItemMapperTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<DbCartItem>> _mockRepository;
        private readonly CartItemMapper _cartItemMapper;
        private readonly Mock<IRepository<DbCart>> _mockCartRepository;

        public CartItemMapperTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<IRepository<DbCartItem>>();
            _mockCartRepository = new Mock<IRepository<DbCart>>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCartItem>()).Returns(_mockRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCart>()).Returns(_mockCartRepository.Object);

            // Setup the cart repository to return a cart for any ID
            _mockCartRepository.Setup(repo => repo.Get(It.IsAny<Guid>()))
                .Returns((Guid id) => new DbCart { Id = id });

            _cartItemMapper = new CartItemMapper(_mockUnitOfWork.Object);
        }

        [Fact]
        public void Insert_ShouldInsertBluRayProductSuccessfully()
        {
            // Arrange
            var bluRayProduct = new BluRayProduct (19.99,646464,1,new Guid());
            var cartId = Guid.NewGuid();

            // Act
            _cartItemMapper.Insert(bluRayProduct, cartId);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Insert(It.Is<DbBluRayCart>(item =>
                item.MovieId == bluRayProduct.movieId &&
                item.Price == bluRayProduct.Price &&
                item.CartId == cartId
            )), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Insert_ShouldInsertRentalProductSuccessfully()
        {
            // Arrange
            var rentalProduct = new RentalProduct (19.99,646464,1,new Guid());
            var cartId = Guid.NewGuid();

            // Act
            _cartItemMapper.Insert(rentalProduct, cartId);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Insert(It.Is<DbRentItem>(item =>
                item.MovieId == rentalProduct.movieId &&
                item.Price == rentalProduct.Price &&
                item.CartId == cartId
            )), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Insert_ShouldInsertMovieTicketProductSuccessfully()
        {
            // Arrange
            var ticketProduct = new MovieTicketProduct(19.99,646464,1,new Guid(), 2, new[] { "A1", "A2" });
            var cartId = Guid.NewGuid();

            // Act
            _cartItemMapper.Insert(ticketProduct, cartId);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Insert(It.Is<DbTicketCart>(item =>
                item.MovieId == ticketProduct.movieId &&
                item.Price == ticketProduct.Price &&
                item.PersonAmount == ticketProduct.getPersonAmount() &&
                item.Seats == ticketProduct.getSeats() &&
                item.CartId == cartId
            )), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Insert_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCartItem>()).Returns((IRepository<DbCartItem>?)null);
            var bluRayProduct = new BluRayProduct (19.99,646464,1,new Guid());
            var cartId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cartItemMapper.Insert(bluRayProduct, cartId));
        }

        [Fact]
        public void Insert_ShouldThrowInvalidOperationException_ForUnhandledCartItemType()
        {
            // Arrange
            var unhandledCartItem = new Mock<ICartItem>().Object;
            var cartId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cartItemMapper.Insert(unhandledCartItem, cartId));
        }
    }
}