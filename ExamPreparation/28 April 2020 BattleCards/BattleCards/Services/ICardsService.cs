using BattleCards.Data.Models;
using BattleCards.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAll();

        int AddCard(string name, string imageUrl, string keyword, int attack, int health, string description);

        ICollection<CardViewModel> GetCardsById(string userId);

        void AddCardToCollction(string userId, int cardId);

        void RemoveFromCollection(string userId, int cardId);
    }
}
