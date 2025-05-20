using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;

namespace GetWatch.Services.Cards
{
    public class CardMapper : ICardMapper
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ICard> GetAll(Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbCard>();
            if (repository == null)
            {
                Console.WriteLine("Repository for DbCartItem is null.");
                return new List<ICard>();

            }

            var dbCardItems = repository.GetAll()?.Where(x => x.UserId == userId).ToList() ?? new List<DbCard>();
            List<ICard> cardItems = dbCardItems.Select(dbCard =>
            {
                return new Card(dbCard.cardNumber, dbCard.cardOwner, dbCard.expiryDate, dbCard.cvv, dbCard.Id);
            }).Cast<ICard>().ToList();
            return cardItems;
        }

        public ICard? Get(Guid id)
        {
            var repository = _unitOfWork.GetRepository<DbCard>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCard is null.");
            }
            var dbCardItem = repository.Get(id);
            if (dbCardItem == null)
            {
                return null;
            }
            return new Card(dbCardItem.cardNumber, dbCardItem.cardOwner, dbCardItem.expiryDate, dbCardItem.cvv, dbCardItem.Id);
        }

        public void Insert(ICard card, Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbCard>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCard is null.");
            }
            var dbCard = new DbCard
            {
                cardNumber = card.CardNumber,
                cardOwner = card.CardOwner,
                expiryDate = card.ExpiryDate,
                cvv = card.Cvv,
                UserId = userId
            };
            _unitOfWork.Begin();
            repository.Insert(dbCard);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }

        public void Remove(ICard card)
        {
            var repository = _unitOfWork.GetRepository<DbCard>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCard is null.");
            }
            var dbCard = repository.Get(card.Id);
            if (dbCard == null)
            {
                throw new InvalidOperationException("Card not found.");
            }
            _unitOfWork.Begin();
            repository.Delete(dbCard);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        
    }
}