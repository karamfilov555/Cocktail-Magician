using CM.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class BarDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public String ImageUrl { get; set; }
        public IFormFile BarImage { set; get; }
        public string CountryId { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public String Details { get; set; }
        public string Website { get; set; }
        public DateTime? DateDeleted { get; set; }
        public List<CocktailDto> Cocktails { get; set; }
            = new List<CocktailDto>();

        public ICollection<BarReview> BarReviews { get; set; }
           = new List<BarReview>();

        public BarDTO()
        {

        }
    }
}
