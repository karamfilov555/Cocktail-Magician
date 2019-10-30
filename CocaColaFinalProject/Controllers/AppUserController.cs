using CM.Services;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Controllers
{
    public class AppUserController:Controller
    {
        private readonly IAppUserServices _appUserServices;

        public AppUserController(IAppUserServices appUserServices)
        {
           _appUserServices = appUserServices;
        }

        public async Task<IActionResult> ListUsers()
        {

            var userDTOs = await _appUserServices.GetAllUsers();
            var listUserVM = userDTOs.Select(u=>u.MapAppUserToVM()).ToList();
            return View(listUserVM);
        }
    }
}
