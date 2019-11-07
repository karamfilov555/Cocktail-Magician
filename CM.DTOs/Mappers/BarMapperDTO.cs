using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class BarMapperDTO
    {
        public static BarDTO MapBarToDTO(this Bar bar)
        {
            var newBarDTO = new BarDTO();
            newBarDTO.Id = bar.Id;
            newBarDTO.Name = bar.Name;
            newBarDTO.ImageUrl = bar.Image;
            newBarDTO.Rating = bar.BarRating;
            newBarDTO.Website = bar.Website;
            newBarDTO.CocktailsNames = bar.BarCocktails.Select(b => b.Cocktail.Name).ToList();
            newBarDTO.DateDeleted = bar.DateDeleted;
            return newBarDTO;
        }

        public static BarDTO MapBarToDTOWithFullAdress(this Bar bar)
        {
            var newBarDTO = new BarDTO();
            newBarDTO.Id = bar.Id;
            newBarDTO.Name = bar.Name;
            newBarDTO.ImageUrl = bar.Image;
            newBarDTO.Rating = bar.BarRating;
            newBarDTO.Website = bar.Website;
            newBarDTO.CocktailsNames = bar.BarCocktails.Select(b => b.Cocktail.Name).ToList();
            newBarDTO.DateDeleted = bar.DateDeleted;
            if (bar.Address != null)
            {
                newBarDTO.Country = bar.Address.Country;
                newBarDTO.City = bar.Address.City;
                newBarDTO.Details = bar.Address.Details;
            }
            return newBarDTO;
        }

        public static Bar MapBarDTOToBar(this BarDTO bar)
        {
            var newBar = new Bar();
            newBar.Id = bar.Id;
            newBar.Name = bar.Name;
            newBar.Image = bar.ImageUrl;
            newBar.Website = bar.Website;
            newBar.DateDeleted = bar.DateDeleted;
            return newBar;
        }
    }
}
