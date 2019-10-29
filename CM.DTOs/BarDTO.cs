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
        public string Website { get; set; }
        public DateTime? DateDeleted { get; set; }
        
        public BarDTO()
        {
           
        }
    }
}
