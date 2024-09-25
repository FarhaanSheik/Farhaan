using System.ComponentModel.DataAnnotations;

namespace Farhaan.Models
{
    public class Car
    {
        // Unique identifier for each car
        [Key] 
        public int CarID { get; set; }

        // Brand of the car (e.g., Toyota, Ford)
        [Required] // makes the Brand field mandatory
        [StringLength(100)] // Limits the length of the Brand string
        public string Brand { get; set; }

        // Year the car was manufactured
        [Range(1870, 2100)] // Range between when the car can be booked
        public int Year { get; set; }

        // Price per day for renting the car
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive value")] // Ensures that the price is positive
        public int PricePerDay { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}