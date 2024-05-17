using Farhaan.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Farhaan.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int CarID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public int TotalPrice { get; set; }

        public User User { get; set; }
        public Car Car { get; set; }
     
          
    }
}
