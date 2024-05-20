using Farhaan.Controllers;

namespace Farhaan.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int CarID { get; set; }      
        public int Date { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
        public int TotalPrice { get; set; }

        public User User { get; set; }
        public Car Car { get; set; }
     
          
    }
}
