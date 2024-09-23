using Farhaan.Areas.Identity.Data;
using Farhaan.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Farhaan.Models
{
    public class Booking
    {
        // Unique identifier for each booking
        public int BookingID { get; set; }

        // Foreign key linking to the appUser (customer) making the booking
        [ForeignKey("appUser")]
        public string appUserID { get; set; }

        // Foreign key linking to the car being booked
        public int CarID { get; set; }

        // Required date for the booking
        [Required]
        [DataType(DataType.Date)] // Specifies that this property is a date type
        public DateTime Date { get; set; }

        // Required time for the booking
        [DataType(DataType.Time)] // Specifies that this property is a time type
        [Required]
        public DateTime Time { get; set; }

        // Optional location for picking up the car
        public string Location { get; set; }

        // Total price for the booking
        public int TotalPrice { get; set; }

        
        public appUser appUser { get; set; }

       
        public Car Car { get; set; }
    }
}