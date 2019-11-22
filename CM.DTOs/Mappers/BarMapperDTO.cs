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
            newBarDTO.Cocktails = bar.BarCocktails.Select(bc => bc.Cocktail.MapToCocktailDto()).ToList();
            newBarDTO.DateDeleted = bar.DateDeleted;
            return newBarDTO;
        }

        public static HomePageBarDTO MapBarToHomePageBarDTO(this Bar bar) //tested
        {
            var newBarDTO = new HomePageBarDTO();
            newBarDTO.Id = bar.Id;
            newBarDTO.Name = bar.Name;
            newBarDTO.ImageUrl = bar.Image;
            try
            {
                newBarDTO.City = bar.Address.City; //check if throw if adress not included
            }
            catch (Exception)
            {
                throw new Exception("Address cannot be null");
            }
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
            newBarDTO.Cocktails = bar.BarCocktails.Select(bc => bc.Cocktail.MapToCocktailDto()).ToList();
            newBarDTO.DateDeleted = bar.DateDeleted;
            if (bar.Address != null)
            {
                newBarDTO.Country = bar.Address.Country.Name;
                newBarDTO.City = bar.Address.City;
                newBarDTO.Details = bar.Address.Details;
            }
            return newBarDTO;
        }

        public static Bar MapBarDTOToBar(this BarDTO bar) // tested
        {
            var newBar = new Bar();
            newBar.Id = bar.Id;
            newBar.Name = bar.Name;
            newBar.Image = bar.ImageUrl;
            newBar.Website = bar.Website;
            newBar.DateDeleted = bar.DateDeleted;
            return newBar;
        }
        public static BarSearchResultDTO MapBarToSearchDTO(this Bar bar)
        {
            var searchBarDTO = new BarSearchResultDTO();
            searchBarDTO.Id = bar.Id;
            searchBarDTO.Name = bar.Name;
            searchBarDTO.Image = bar.Image;
            searchBarDTO.BarRating = bar.BarRating;
            searchBarDTO.Address = bar.Address.Country.Name+", "+bar.Address.City+", "+bar.Address.Details;
            return searchBarDTO;
        }

        public static Bar EditBarDTOToBar(this BarDTO barDTO, Bar bar)
        {
            if (barDTO.Name != bar.Name)
            {
                bar.Name = barDTO.Name;
            }
            if (barDTO.BarImage!=null)
            {
                bar.Image = barDTO.ImageUrl;
            }
            if (barDTO.CountryId != bar.Address.CountryId)
            {
                bar.Address.CountryId = barDTO.CountryId;
            }
            if (barDTO.City != bar.Address.City)
            {
                bar.Address.City = barDTO.City;
            }
            if (barDTO.Details != bar.Address.Details)
            {
                bar.Address.City = barDTO.City;
            }
            if (barDTO.Website != bar.Website)
            {
                bar.Website = barDTO.Website;
            }
            return bar;
        }
    }
}
