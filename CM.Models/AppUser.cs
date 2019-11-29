﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CM.Models
{
    public class AppUser : IdentityUser
    {
        public string ImageURL { get; set; }
        public DateTime? DateDeleted { get; set; }
        public AppUser()
        {
        }

        public ICollection<CocktailReview> CocktailReviews { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
        public ICollection<BarReviewLike> BarReviewLikes { get; set; }
        public ICollection<CocktailReviewLike> CocktailReviewLikes { get; set; }
        public ICollection<Notification> Notifications { get; set; }

    }

    //public DateTime? DateBanned { get; set; } May be another model
}

