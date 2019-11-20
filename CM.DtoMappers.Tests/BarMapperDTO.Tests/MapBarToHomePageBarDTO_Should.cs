using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DtoMappers.Tests.BarMapperDTO.Tests
{
    [TestClass]
    public class MapBarToHomePageBarDTO_Should
    {
        private string barId = "1";
        private string barName = "BarName";


        [TestMethod]
        public void Return_BarHomePageDto_WhenValidValuesPassed()
        {
            var address = new Address
            {
                Id = "1",
                BarId = barId,
                City = "Sofia"
            };
            var bar = new Bar
            {
                Id = barId,
                Name = barName,
                Image = "www.abv.bg",
                Website = "www.abv.bg",
                Address = address,
                Reviews = new List<BarReview>(),
                BarCocktails = new List<BarCocktail>()
            };
            /*.MapBarToHomePageBarDTO();*/
           var result = bar.MapBarToHomePageBarDTO();

            Assert.IsInstanceOfType(result, typeof(HomePageBarDTO));
        }
        [TestMethod]
        public void Throw_ArgumentNullEx_WhenAddressNotIncluded()
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

            Assert.ThrowsException<NullReferenceException>(() => bar.MapBarToHomePageBarDTO());
        }
        [TestMethod]
        public void Correctly_ReturnCollection_OfBarHomePageDtoModels()
        {
            var address = new Address
            {
                Id = "1",
                BarId = barId,
                City = "Sofia"
            };
            var bar = new Bar
            {
                Id = barId,
                Name = barName,
                Address = address
            };
            var bar2 = new Bar
            {
                Id = "2",
                Name = "Bash",
                Address = address
            };
            var listBars = new List<Bar>();
            listBars.Add(bar);
            listBars.Add(bar2);
            var result = listBars.Select(b => b.MapBarToHomePageBarDTO()).ToList();
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(listBars[0].Name, result[0].Name);
            Assert.AreEqual(listBars[0].Id, result[0].Id);
            Assert.AreEqual(listBars[1].Name, result[1].Name);
            Assert.AreEqual(listBars[1].Id, result[1].Id);

            Assert.IsInstanceOfType(result, typeof(List<HomePageBarDTO>));
        }
    }
}
