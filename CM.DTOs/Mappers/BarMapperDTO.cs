using CM.Models;
using System;
using System.Collections.Generic;
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
            newBarDTO.Website = bar.Website;
            newBarDTO.DateDeleted = bar.DateDeleted;

            return newBarDTO;
        }
    }
}
