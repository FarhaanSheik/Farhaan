using Farhaan.Areas.Identity.Data;

namespace Farhaan.Models
{
    public class Customer: appUser
    {

        public ICollection<Booking> Bookings { get; set; }
    }
}
