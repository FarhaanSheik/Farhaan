using System.ComponentModel.DataAnnotations;

namespace Farhaan.Models
{
    public class User
    {
        public int UserID { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(15)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        public int PhoneNumber { get; set; }
        public int LicenseNumber { get; set; }
       
        public ICollection <Booking> Bookings { get; set; } 
    }
}
