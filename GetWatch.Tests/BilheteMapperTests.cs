using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Bilhete;
using GetWatch.Services.Db.Purchases;

namespace GetWatch.Tests
{
    public class BilheteMapperTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<DbTicketPurchase>> _repositoryMock;
        private readonly BilheteMapper _bilheteMapper;

        public BilheteMapperTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IRepository<DbTicketPurchase>>();
            _unitOfWorkMock.Setup(u => u.GetRepository<DbTicketPurchase>()).Returns(_repositoryMock.Object);
            _bilheteMapper = new BilheteMapper(_unitOfWorkMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoBilhetesExistForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetAll()).Returns(new List<DbTicketPurchase>().AsQueryable().ToList());

            // Act
            var result = _bilheteMapper.GetAll(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.GetRepository<DbTicketPurchase>()).Returns((IRepository<DbTicketPurchase>)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _bilheteMapper.GetAll(userId));
        }

        [Fact]
        public void GetAll_ShouldReturnBilhetes_WhenUserIdIsValid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dbTickets = new List<DbTicketPurchase>
            {
                new DbTicketPurchase { Id = Guid.NewGuid(), UserId = userId, PersonAmount = 2, Seats = new[] { "A1", "A2" } },
                new DbTicketPurchase { Id = Guid.NewGuid(), UserId = userId, PersonAmount = 3, Seats = "B1,B2,B3".Split(',') }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(dbTickets.AsQueryable().ToList());

            // Act
            var result = _bilheteMapper.GetAll(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, b => Assert.Equal(userId, dbTickets.First(d => d.Id == b.Id).UserId));
        }

        [Fact]
        public void Get_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.GetRepository<DbTicketPurchase>()).Returns((IRepository<DbTicketPurchase>)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _bilheteMapper.Get(ticketId));
        }

        [Fact]
        public void Get_ShouldReturnBilhete_WhenIdIsValid()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var dbTicket = new DbTicketPurchase { Id = ticketId, PersonAmount = 2, Seats = "A1,A2".Split(',') };
            _repositoryMock.Setup(r => r.Get(ticketId)).Returns(dbTicket);

            // Act
            var result = _bilheteMapper.Get(ticketId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ticketId, result.Id);
            Assert.Equal(dbTicket.PersonAmount, result.PersonAmount);
            Assert.Equal(dbTicket.Seats, result.Seats);
        }

        [Fact]
        public void Get_ShouldThrowKeyNotFoundException_WhenIdIsInvalid()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.Get(invalidId)).Returns((DbTicketPurchase)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _bilheteMapper.Get(invalidId));
        }

        [Fact]
        public void Insert_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bilhete = new BilheteBase { Id = Guid.NewGuid(), PersonAmount = 2, Seats = "A1,A2".Split(',') };
            _unitOfWorkMock.Setup(u => u.GetRepository<DbTicketPurchase>()).Returns((IRepository<DbTicketPurchase>)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _bilheteMapper.Insert(bilhete, userId));
        }

        [Fact]
        public void Insert_ShouldAddBilheteSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bilhete = new BilheteBase { Id = Guid.NewGuid(), PersonAmount = 2, Seats = "A1,A2".Split(',') };
            _repositoryMock.Setup(r => r.Insert(It.IsAny<DbTicketPurchase>()));

            // Act
            _bilheteMapper.Insert(bilhete, userId);

            // Assert
            _repositoryMock.Verify(r => r.Insert(It.Is<DbTicketPurchase>(d =>
                d.Id == bilhete.Id && d.PersonAmount == bilhete.PersonAmount && d.Seats == bilhete.Seats && d.UserId == userId)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void Remove_ShouldThrowInvalidOperationException_WhenRepositoryIsNull()
        {
            // Arrange
            var bilhete = new BilheteBase { Id = Guid.NewGuid() };
            _unitOfWorkMock.Setup(u => u.GetRepository<DbTicketPurchase>()).Returns((IRepository<DbTicketPurchase>)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _bilheteMapper.Remove(bilhete));
        }

        [Fact]
        public void Remove_ShouldDeleteBilheteSuccessfully()
        {
            // Arrange
            var bilheteId = Guid.NewGuid();
            var dbTicket = new DbTicketPurchase { Id = bilheteId };
            _repositoryMock.Setup(r => r.Get(bilheteId)).Returns(dbTicket);
            _repositoryMock.Setup(r => r.Delete(dbTicket));

            // Act
            _bilheteMapper.Remove(new BilheteBase { Id = bilheteId });

            // Assert
            _repositoryMock.Verify(r => r.Delete(dbTicket), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void Remove_ShouldThrowKeyNotFoundException_WhenBilheteNotFound()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.Get(invalidId)).Returns((DbTicketPurchase)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _bilheteMapper.Remove(new BilheteBase { Id = invalidId }));
        }
    }
}