using SharedTrip.InputModels;
using SharedTrip.ViewModels;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripService
    {
        void CreateTrip(AddTripInputModel tripModel);

        IEnumerable<AllTripsViewModel> GetAll();

        TripDetailsViewModel GetTripDetails(string tripId);

        void AddUserToTrip(string userId, string tripId);

        bool HasAvailableSeats(string tripId);
    }
}
