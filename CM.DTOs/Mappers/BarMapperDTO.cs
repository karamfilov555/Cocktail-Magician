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
            newBarDTO.Address = bar.Address;
            newBarDTO.DateDeleted = bar.DateDeleted;
            return newBarDTO;
        }

        public static Bar MapBarDTOToBar(this BarDTO bar)
        {
            var newBar = new Bar();
            newBar.Id = bar.Id;
            newBar.Name = bar.Name;
            newBar.Image = bar.ImageUrl;
            newBar.Website = bar.Website;
            newBar.Address = bar.Address;
            newBar.DateDeleted = bar.DateDeleted;
            return newBar;
        }
    }
}
