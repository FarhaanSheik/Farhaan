using Farhaan.Areas.Identity.Data;
using Farhaan.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farhaan.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        [ForeignKey("appUser")]
        public string appUserID { get; set; }
        public int CarID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public int TotalPrice { get; set; }
        public appUser appUser { get; set; }   

        public Car Car { get; set; } 

    }
}
