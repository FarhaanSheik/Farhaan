namespace Farhaan.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public int UserID { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int PricePerDay { get; set; }


        public ICollection<Booking> Bookings { get; set; }    
        public User User { get; set; }    

       
        }
    }
