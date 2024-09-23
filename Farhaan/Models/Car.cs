using Microsoft.AspNetCore.Identity;

namespace Farhaan.Models
{
    public class Car
    {
        // Unique identifier for each car
        public int CarID { get; set; }

        // Brand of the car (e.g., Toyota, Ford)
        public string Brand { get; set; }

        // Year the car was manufactured
        public int Year { get; set; }

        // Price per day for renting the car
        public int PricePerDay { get; set; }

        public ICollection<Booking> Bookings { get; set; }



    }
    }
