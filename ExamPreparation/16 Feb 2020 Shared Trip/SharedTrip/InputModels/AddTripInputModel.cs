﻿using System;

namespace SharedTrip.InputModels
{
    public class AddTripInputModel
    {
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public string imagePath { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
