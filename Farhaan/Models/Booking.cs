using Farhaan.Areas.Identity.Data;
using Farhaan.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Farhaan.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public appUser CustomerID { get; set; }
        public int CarID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public int TotalPrice { get; set; }
        public Customer Customer { get; set; }   
       
        public ICollection<Car> Cars { get; set; }
    }
}
