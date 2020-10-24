using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.InputModels;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext db;

        public TripService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            var hasUserJoinedTrip = this.db.UsersTrips.Any(x => x.TripId == tripId && x.UserId == userId);

            if (hasUserJoinedTrip)
            {
                return;
            }

            var trip = this.db.Trips.Find(tripId);
            trip.Seats -= 1;
            
            this.db.UsersTrips.Add(new UserTrip() { TripId = tripId, UserId = userId });
            this.db.SaveChanges();
        }

        public bool HasAvailableSeats(string tripId)
        {
            return this.db.Trips.Find(tripId).Seats > 0;
        }

        public void CreateTrip(AddTripInputModel tripModel)
        {
            var trip = new Trip()
            {
                DepartureTime = DateTime.ParseExact(tripModel.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Description = tripModel.Description,
                EndPoint = tripModel.EndPoint,
                ImagePath = tripModel.imagePath,
                Seats = tripModel.Seats,
                StartPoint = tripModel.StartPoint
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public IEnumerable<AllTripsViewModel> GetAll()
        {
            return this.db.Trips
                .Select(x => new AllTripsViewModel()
                {
                    Id = x.Id,
                    DepartureTime = x.DepartureTime,
                    EndPoint = x.EndPoint,
                    Seats = x.Seats,
                    StartPoint = x.StartPoint
                }).ToList();
        }

        public TripDetailsViewModel GetTripDetails(string tripId)
        {
            return this.db.Trips
                .Where(x => x.Id == tripId)
                .Select(x => new TripDetailsViewModel()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    DepartureTime = x.DepartureTime,
                    Description = x.Description,
                    EndPoint = x.EndPoint,
                    Seats = x.Seats,
                    StartPoint = x.StartPoint
                }).FirstOrDefault();
        }
    }
}
