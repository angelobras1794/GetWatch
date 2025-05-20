using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using GetWatch.Interfaces.Cards;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using GetWatch.Services.Cards;

namespace GetWatch.Tests.Services.Cards
{
    public class CardMapperTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<DbCard>> _mockRepository;
        private readonly CardMapper _cardMapper;

        public CardMapperTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<IRepository<DbCard>>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCard>()).Returns(_mockRepository.Object);
            _cardMapper = new CardMapper(_mockUnitOfWork.Object);
        }

        [Fact]
        public void GetAll_ReturnsCardsForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dbCards = new List<DbCard>
            {
                new DbCard { Id = Guid.NewGuid(), UserId = userId, cardNumber = 123, cardOwner = "A", expiryDate = "12/25", cvv = 111 },
                new DbCard { Id = Guid.NewGuid(), UserId = userId, cardNumber = 456, cardOwner = "B", expiryDate = "11/24", cvv = 222 }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(dbCards);

            // Act
            var result = _cardMapper.GetAll(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, c => Assert.Equal(userId, dbCards.First(x => x.Id == c.Id).UserId));
        }

        [Fact]
        public void GetAll_ReturnsEmptyList_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCard>()).Returns((IRepository<DbCard>?)null);
            var cardMapper = new CardMapper(_mockUnitOfWork.Object);

            // Act
            var result = cardMapper.GetAll(Guid.NewGuid());

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_ReturnsEmptyList_WhenNoCardsForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<DbCard>());

            // Act
            var result = _cardMapper.GetAll(userId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Get_ReturnsCard_WhenFound()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var dbCard = new DbCard { Id = cardId, cardNumber = 123, cardOwner = "A", expiryDate = "12/25", cvv = 111 };
            _mockRepository.Setup(r => r.Get(cardId)).Returns(dbCard);

            // Act
            var result = _cardMapper.Get(cardId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cardId, result.Id);
            Assert.Equal(123, result.CardNumber);
        }

        [Fact]
        public void Get_ReturnsNull_WhenCardNotFound()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            _mockRepository.Setup(r => r.Get(cardId)).Returns((DbCard?)null);

            // Act
            var result = _cardMapper.Get(cardId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_ThrowsInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCard>()).Returns((IRepository<DbCard>?)null);
            var cardMapper = new CardMapper(_mockUnitOfWork.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cardMapper.Get(Guid.NewGuid()));
        }

        [Fact]
        public void Insert_InsertsCardSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cardMock = new Mock<ICard>();
            cardMock.SetupGet(c => c.CardNumber).Returns(123);
            cardMock.SetupGet(c => c.CardOwner).Returns("A");
            cardMock.SetupGet(c => c.ExpiryDate).Returns("12/25");
            cardMock.SetupGet(c => c.Cvv).Returns(111);

            // Act
            _cardMapper.Insert(cardMock.Object, userId);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(r => r.Insert(It.Is<DbCard>(db =>
                db.cardNumber == 123 &&
                db.cardOwner == "A" &&
                db.expiryDate == "12/25" &&
                db.cvv == 111 &&
                db.UserId == userId
            )), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Insert_ThrowsInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCard>()).Returns((IRepository<DbCard>?)null);
            var cardMapper = new CardMapper(_mockUnitOfWork.Object);
            var cardMock = new Mock<ICard>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cardMapper.Insert(cardMock.Object, Guid.NewGuid()));
        }

        [Fact]
        public void Remove_RemovesCardSuccessfully()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var dbCard = new DbCard { Id = cardId };
            var cardMock = new Mock<ICard>();
            cardMock.SetupGet(c => c.Id).Returns(cardId);
            _mockRepository.Setup(r => r.Get(cardId)).Returns(dbCard);

            // Act
            _cardMapper.Remove(cardMock.Object);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(r => r.Delete(dbCard), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Remove_ThrowsInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbCard>()).Returns((IRepository<DbCard>?)null);
            var cardMapper = new CardMapper(_mockUnitOfWork.Object);
            var cardMock = new Mock<ICard>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cardMapper.Remove(cardMock.Object));
        }

        [Fact]
        public void Remove_ThrowsInvalidOperationException_WhenCardNotFound()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var cardMock = new Mock<ICard>();
            cardMock.SetupGet(c => c.Id).Returns(cardId);
            _mockRepository.Setup(r => r.Get(cardId)).Returns((DbCard?)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cardMapper.Remove(cardMock.Object));
        }
    }
}