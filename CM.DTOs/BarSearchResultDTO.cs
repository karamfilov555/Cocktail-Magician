using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class BarSearchResultDTO
    {
        //image, name, address and rating
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double? BarRating { get; set; }
    }
}
