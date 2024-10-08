﻿using Farhaan.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Farhaan.Areas.Identity.Data
{
    public class appUser : IdentityUser
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; }


    }
}
