using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.cardsService.GetAll();

            return this.View(viewModel);
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(string name, string image, string keyword, int attack, int health, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name) || name.Length < 5 || name.Length > 15)
            {
                return this.Error("name should be between 5 and 15 characters");
            }

            if (!Uri.TryCreate(image, UriKind.Absolute, out _))
            {
                return this.Error("Image url should be valid.");
            }

            if (string.IsNullOrEmpty(image))
            {
                return this.Error("Image is required.");
            }

            if (string.IsNullOrEmpty(keyword))
            {
                return this.Error("Keyword is required.");
            }

            if(attack < 0)
            {
                return this.Error("Attack can't be negative number");
            }

            if(health < 0)
            {
                return this.Error("Health can't be negative number");
            }

            if(string.IsNullOrEmpty(description) || description.Length > 200)
            {
                return this.Error("Description should be maximum 200 characters.");
            }
;
            var cardId = this.cardsService.AddCard(name, image, keyword, attack, health, description);
            var userId = this.GetUserId();
            this.cardsService.AddCardToCollction(userId, cardId);
            return this.Redirect("/Cards/All");
        }

        [HttpGet]
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            var viewModel = this.cardsService.GetCardsById(userId);

            return this.View(viewModel);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            this.cardsService.AddCardToCollction(userId, cardId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            this.cardsService.RemoveFromCollection(userId, cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
