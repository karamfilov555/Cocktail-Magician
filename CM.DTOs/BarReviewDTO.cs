using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class BarReviewDTO
    {
        public String Id { get; set; }
        public String Description { get; set; }
        public decimal Rating { get; set; }
        public string UserName { get; set; }
        public string ReviewDate { get; set; }
        public BarReviewDTO()
        {

        }
    }
}
