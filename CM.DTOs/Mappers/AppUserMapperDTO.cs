using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class AppUserMapperDTO
    {
        public static AppUserDTO MapToAppUserDTO(this AppUser user)
        {
            var userDTO = new AppUserDTO();
            userDTO.Id = user.Id;
            userDTO.Username = user.UserName;
            userDTO.Email = user.Email;
            return userDTO;
        }
    }
}
