using CM.DTOs;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class UserMapper
    {
       
            public static AppUserViewModel MapAppUserToVM(this AppUserDTO user)
            {
                var newAppUserVM = new AppUserViewModel();
                newAppUserVM.Id = user.Id;
                newAppUserVM.Username = user.Username;
                newAppUserVM.Email = user.Email;
                newAppUserVM.Role = user.Role;
                newAppUserVM.ImageURL = user.ImageURL;

            return newAppUserVM;
            }
        
    }
}
