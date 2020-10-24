using SharedTrip.InputModels;
using SharedTrip.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripService.GetAll();
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
        public HttpResponse Add(AddTripInputModel tripModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(tripModel.StartPoint))
            {
                return this.Error("Starting point is required!");
            }

            if (string.IsNullOrEmpty(tripModel.EndPoint))
            {
                return this.Error("End point is required!");
            }

            if (tripModel.Seats < 2 || tripModel.Seats > 6)
            {
                return this.Error("Seats must be between 2 and 6!");
            }

            if (string.IsNullOrEmpty(tripModel.Description) || tripModel.Description.Length > 80)
            {
                return this.Error("Description must be below 80 characters!");
            }

            if (!DateTime
               .TryParseExact(tripModel.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error("Invalid date!");
            };

            this.tripService.CreateTrip(tripModel);
            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripService.GetTripDetails(tripId);
            return this.View(viewModel);
        }

        [HttpGet]
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (!this.tripService.HasAvailableSeats(tripId))
            {
                return this.Error("No more free seats for this trip!");
            }

            this.tripService.AddUserToTrip(userId, tripId);
            return this.Redirect("/Trips/All");
        }
    }
}
