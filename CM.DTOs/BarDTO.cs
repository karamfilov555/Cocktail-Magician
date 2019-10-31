using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class BarDTO
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String ImageUrl { get; set; }
        public String Address { get; set; }
        public string Website { get; set; }
        public DateTime? DateDeleted { get; set; }
        public List<Cocktail> Cocktails { get; set; }
            = new List<Cocktail>();
        public ICollection<Review> Reviews { get; set; }
           = new List<Review>();

        public BarDTO()
        {
           
        }
    }
}
