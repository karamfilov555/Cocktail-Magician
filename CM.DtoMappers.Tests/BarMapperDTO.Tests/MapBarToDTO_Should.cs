using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DtoMappers.Tests.BarMapperDTO.Tests
{
    [TestClass]
    public class MapBarToDTO_Should
    {
        private string barId = "1";
        private string barName = "BarName";

        [TestMethod]
        public void Return_BarDto_WhenValidValuesPassed()
        {
           
            var bar = new Bar
            {
                Id = barId,
                Name = barName,
                Image = "www.abv.bg",
                Website = "www.abv.bg",
                Reviews = new List<BarReview>(),
                BarCocktails = new List<BarCocktail>()
            };
            var result = bar.MapBarToDTO();

            Assert.IsInstanceOfType(result, typeof(BarDTO));
        }
        [TestMethod]
        public void AssignCorrectValues_OfCtxModel()
        {

            var ingredient = new Ingredient { Id = "1", Name = "ChubriKa" };
            var ingredient2 = new Ingredient { Id = "2", Name = "ingredient" };

            var cocktail = 
                new Cocktail
                {
                    Id = "1",
                    Name = "test",
                    CocktailComponents = new List<CocktailComponent>()
                    {
                        new CocktailComponent {CocktailId = "1", IngredientId = "1" },
                        new CocktailComponent {CocktailId = "1", IngredientId = "2"}
                    }
                };

            var cocktail2 =
                new Cocktail
                {
                    Id = "2",
                    Name = "test2",
                    CocktailComponents = new List<CocktailComponent>()
                    {
                      new CocktailComponent {CocktailId = "2", IngredientId = "1" },
                      new CocktailComponent {CocktailId = "2", IngredientId = "2" }
                    }
                };




            var barCocktail = new BarCocktail { BarId = barId, CocktailId = "1" };
            var barCocktail2 = new BarCocktail { BarId = barId, CocktailId = "2" };

            var assosiativeList = new List<BarCocktail>();
            assosiativeList.Add(barCocktail);
            assosiativeList.Add(barCocktail2);

            var bar = new Bar
            {
                Id = barId,
                Name = barName,
                Website = "www.abv.bg",
                Image = "www.abv.bg",
                BarRating = 3.3,
                BarCocktails = assosiativeList,
                DateDeleted = DateTime.Now
            };

            var result = bar.MapBarToDTO();

            Assert.AreEqual(bar.Id, result.Id);
            Assert.AreEqual(bar.Name, result.Name);
            Assert.AreEqual(bar.Website, result.Website);
            Assert.AreEqual(bar.Image, result.ImageUrl);
            Assert.AreEqual(bar.BarRating, result.Rating);
            Assert.AreEqual(bar.DateDeleted, result.DateDeleted);
        }
    }
    //public static BarDTO MapBarToDTO(this Bar bar)
    //{
    //    var newBarDTO = new BarDTO();
    //    newBarDTO.Id = bar.Id;
    //    newBarDTO.Name = bar.Name;
    //    newBarDTO.ImageUrl = bar.Image;
    //    newBarDTO.Rating = bar.BarRating;
    //    newBarDTO.Website = bar.Website;
    //    newBarDTO.Cocktails = bar.BarCocktails.Select(bc => bc.Cocktail.MapToCocktailDto()).ToList();
    //    newBarDTO.DateDeleted = bar.DateDeleted;
    //    return newBarDTO;
    //}
}
