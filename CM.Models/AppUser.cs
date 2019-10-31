using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CM.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime? DateDeleted { get; set; }
        public AppUser()
        {
        }

        public ICollection<Review> Reviews { get; set; }

    }

    //public DateTime? DateBanned { get; set; } May be another model
}

