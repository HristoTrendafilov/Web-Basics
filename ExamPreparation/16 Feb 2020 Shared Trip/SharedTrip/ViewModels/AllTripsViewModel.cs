using System;
using System.Globalization;

namespace SharedTrip.ViewModels
{
    public class AllTripsViewModel 
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DepartureTimeAsString => this.DepartureTime.ToString("s", CultureInfo.InvariantCulture);

        public int Seats { get; set; }
    }
}
