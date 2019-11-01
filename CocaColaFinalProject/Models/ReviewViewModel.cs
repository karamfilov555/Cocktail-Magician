using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    public class ReviewViewModel
    {
        public String Id { get; set; }
        public String Description { get; set; }
        public decimal Rating { get; set; }
        public string UserName { get; set; }
        public string ReviewDate { get; set; }
        public ReviewViewModel()
        {

        }
    }
}
