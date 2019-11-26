using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NToastNotify;

namespace CM.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AppUserController:Controller
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IToastNotification _toast;


        public AppUserController(IAppUserServices appUserServices, IToastNotification toast)
        {
           _appUserServices = appUserServices;
            _toast = toast;
        }
   
        public async Task<IActionResult> ListUsers()
        {

            try
            {
            var userDTOs = await _appUserServices.GetAllUsers();
            var listUserVM = userDTOs.Select(u=>u.MapAppUserToVM()).ToList();
            return View(listUserVM);
            }
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
        
        public async Task<IActionResult> ChangeRoleToManager(string id)
        {
            try
            {
            await _appUserServices.ConvertToManager(id);
            return RedirectToAction("ListUsers", "AppUser");

            }
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var userDTO = await _appUserServices.GetUserDToByID(id);
                var appUserVM = userDTO.MapAppUserToVM();
                return View(appUserVM);

            }
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {

            await _appUserServices.Delete(id);
            return RedirectToAction("ListUsers", "AppUser");

            }
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }


        public async Task<IActionResult> CurrentUser()
        {
            try
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
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
    }
}
