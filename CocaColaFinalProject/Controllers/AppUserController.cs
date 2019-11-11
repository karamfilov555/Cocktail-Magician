using CM.Services.Contracts;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
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
    }
}
