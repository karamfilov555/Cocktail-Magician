using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class SearchResultDTO
    {
        public ICollection<BarDTO> Bars { get; set; } = new List<BarDTO>();
        public ICollection<CocktailDto> Cocktails { get; set; } = new List<CocktailDto>();
    }
}
