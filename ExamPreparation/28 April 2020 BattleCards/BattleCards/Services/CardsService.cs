using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddCard(string name, string image, string keyword, int attack, int health, string description)
        {
            var card = new Card()
            {
                Attack = attack,
                Description = description,
                Health = health,
                ImageUrl = image,
                Keyword = keyword,
                Name = name,
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();
            return card.Id;
        }

        public void AddCardToCollction(string userId, int cardId)
        {
            if(this.db.UsersCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }

            this.db.UsersCards.Add(new UserCard { CardId = cardId, UserId = userId });
            this.db.SaveChanges();
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return db.Cards.Select(x => new CardViewModel
            {
                Attack = x.Attack,
                Description = x.Description,
                Health = x.Health,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Keyword = x.Keyword,
                Name = x.Name
            }).ToList();
        }

        public ICollection<CardViewModel> GetCardsById(string userId)
        {
            return this.db.UsersCards.Where(x => x.User.Id == userId)
                .Select(x => new CardViewModel 
                {
                    Attack = x.Card.Attack,
                    Description = x.Card.Description,
                    Health = x.Card.Health,
                    ImageUrl = x.Card.ImageUrl,
                    Keyword = x.Card.Keyword,
                    Name = x.Card.Name,
                    Id = x.Card.Id
                }).ToList();
        }

        public void RemoveFromCollection(string userId, int cardId)
        {
            var userCard = this.db.UsersCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);

            if(userCard == null)
            {
                return;
            }

            this.db.UsersCards.Remove(userCard);
            this.db.SaveChanges();
        }
    }
}
