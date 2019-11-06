using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Details { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
