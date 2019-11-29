using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public async Task AssignCorrectValues_OfCtxModel()
        {
            var options = TestUtils.GetOptions(nameof(AssignCorrectValues_OfCtxModel));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var ingredient = new Ingredient { Id = "1", Name = "ChernoPiperrChe" };
                var ingredient2 = new Ingredient { Id = "2", Name = "ingredient" };
                assertContext.Ingredients.Add(ingredient);
                assertContext.Ingredients.Add(ingredient2);

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
                var bar = new Bar
                {
                    Id = barId,
                    Name = barName,
                    Website = "www.abv.bg",
                    Image = "www.abv.bg",
                    BarRating = 3.3,
                    BarCocktails = new List<BarCocktail> { barCocktail }
                };

                assertContext.Cocktails.Add(cocktail);
                assertContext.Cocktails.Add(cocktail2);
                assertContext.Bars.Add(bar);
                assertContext.BarCocktails.Add(barCocktail);
                assertContext.BarCocktails.Add(barCocktail2);
                await assertContext.SaveChangesAsync();

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
}