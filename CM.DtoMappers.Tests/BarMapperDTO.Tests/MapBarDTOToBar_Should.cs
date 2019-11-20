using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.DtoMappers.Tests
{
    [TestClass]
    public class MapBarDTOToBar_Should
    {
        private string barId = "1";
        private string barName = "BarName";
        private string barWebsite = "https://www.abvb.bg";
        private double barRating = 2.5;

        [TestMethod]
        public void Return_BarCtxModel_WhenValidValuesPassed()
        {

            var barDto = new BarDTO
            {
                Id = barId,
                Name = barName,
                Website = barWebsite,
                BarImage = new Mock<IFormFile>().Object
            };
            var bar = barDto.MapBarDTOToBar();

            Assert.IsInstanceOfType(bar, typeof(Bar));
        }

        [TestMethod]
        public void Correctly_ReturnCollection_OfCtxModels()
        {
            var barDto = new BarDTO
            {
                Id = barId,
                Name = barName,
                Website = barWebsite,
                BarImage = new Mock<IFormFile>().Object
            };
            var barDto2 = new BarDTO
            {
                Id = "2",
                Name = "Bash",
                Website = "www.abv.bg",
                BarImage = new Mock<IFormFile>().Object
            };
            var listDtos = new List<BarDTO>();
            listDtos.Add(barDto);
            listDtos.Add(barDto2);
            var barList = listDtos.Select(b=>b.MapBarDTOToBar()).ToList();

            Assert.AreEqual(listDtos[0].Name, barList[0].Name);
            Assert.AreEqual(listDtos[0].Id, barList[0].Id);
            Assert.AreEqual(listDtos[0].Website, barList[0].Website);
            Assert.AreEqual(listDtos[1].Name, barList[1].Name);
            Assert.AreEqual(listDtos[1].Id, barList[1].Id);
            Assert.AreEqual(listDtos[1].Website, barList[1].Website);

            Assert.IsInstanceOfType(barList, typeof(List<Bar>));
        }
        [TestMethod]
        public void AssignCorrectValues_OfCtxModel()
        {
            var barDto = new BarDTO
            {
                Id = barId,
                Name = barName,
                Website = barWebsite,
                BarImage = new Mock<IFormFile>().Object
            };
            var bar = barDto.MapBarDTOToBar();

            Assert.AreEqual(barDto.Id, bar.Id);
            Assert.AreEqual(barDto.Name, bar.Name);
            Assert.AreEqual(barDto.Website, bar.Website);
        }
    }
}
