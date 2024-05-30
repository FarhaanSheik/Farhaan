using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Farhaan.Models
{
    public class User : IdentityUser 
    {
        public string UserName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        
        public int PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
       
        public ICollection <Booking> Bookings { get; set; } 
    }
}
