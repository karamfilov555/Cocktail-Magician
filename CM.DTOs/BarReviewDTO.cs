﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class BarReviewDTO
    {
        public String Id { get; set; }
        public String BarId { get; set; }
        public String Description { get; set; }
        public double? Rating { get; set; }
        public string UserName { get; set; }
        public string BarName { get; set; }
        public string UserID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
        public BarReviewDTO()
        {

        }
    }
}
