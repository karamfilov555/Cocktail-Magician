using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
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

        public async Task<IActionResult> ChangeRoleToManager(string id)
        {
            await _appUserServices.ConvertToManager(id);
            return RedirectToAction("ListUsers", "AppUser");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _appUserServices.Delete(id);
            return RedirectToAction("ListUsers", "AppUser");
        }

        public async Task<IActionResult> CurrentUser()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new AppUserViewModel();
            if (userID==null)
            {
                return View(viewModel);
            }
            var user = await _appUserServices.GetUserDToByID(userID);
            viewModel = user.MapAppUserToVM();
            return View(viewModel);
        }
    }
}
