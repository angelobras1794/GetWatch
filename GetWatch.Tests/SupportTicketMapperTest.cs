// filepath: c:\Users\angel\OneDrive\Ambiente de Trabalho\Universidade\Universidade\3-Ano\2-Semestre\ES\ProjetoGit\GetWatch\Services\Tickets\SupportTicketMapperTest.cs
using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.Db;
using GetWatch.Services.Tickets;

namespace GetWatch.Tests
{
    public class SupportTicketMapperTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<DbSupportTickets>> _mockRepository;
        private readonly SupportTicketMapper _mapper;

        public SupportTicketMapperTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<IRepository<DbSupportTickets>>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbSupportTickets>())
                .Returns(_mockRepository.Object);

            _mapper = new SupportTicketMapper(_mockUnitOfWork.Object);
        }

        [Fact]
        public void Remove_SupportTicketExists_ShouldRemoveSuccessfully()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var supportTicket = new Mock<ISupportTicket>();
            supportTicket.Setup(t => t.Id).Returns(ticketId);

            var dbSupportTicket = new DbSupportTickets { Id = ticketId };

            _mockRepository.Setup(repo => repo.Get(ticketId)).Returns(dbSupportTicket);

            // Act
            _mapper.Remove(supportTicket.Object);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Once);
            _mockRepository.Verify(repo => repo.Delete(dbSupportTicket), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Remove_SupportTicketDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var supportTicket = new Mock<ISupportTicket>();
            supportTicket.Setup(t => t.Id).Returns(ticketId);

            _mockRepository.Setup(repo => repo.Get(ticketId)).Returns((DbSupportTickets?)null);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _mapper.Remove(supportTicket.Object));
            Assert.Equal($"support with ID {ticketId} not found.", exception.Message);

            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Never);
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<DbSupportTickets>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never);
        }

        [Fact]
        public void Remove_RepositoryIsNull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var supportTicket = new Mock<ISupportTicket>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<DbSupportTickets>())
                .Returns<IRepository<DbSupportTickets>>(null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _mapper.Remove(supportTicket.Object));
            Assert.Equal("Repository for DbServer is null.", exception.Message);

            _mockUnitOfWork.Verify(uow => uow.Begin(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never);
        }
    }
}