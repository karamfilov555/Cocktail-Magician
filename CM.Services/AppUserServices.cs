using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services
{
    public class AppUserServices : IAppUserServices

    {
        private readonly CMContext _context;
        private readonly UserManager<AppUser> _userManager;


        public AppUserServices(CMContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<AppUser> GetUserByID(string id)
        {
            var user = await _context.Users.FindAsync(id);
            user.ValidateIfNull();
            return user;
        }

        public async Task<AppUserDTO> GetUserDToByID(string id)
        {
            var user = await this.GetUserByID(id);
            user.ValidateIfNull();
            var userDTO= user.MapToAppUserDTO();
            userDTO.Role =await this.GetRole(user);
            return userDTO;
        }

        public async Task<string> GetProfilePictureURL(string id)
        {
            var pictureURL = await _context.Users.Where(u=>u.Id==id)
                .Select(u=>u.ImageURL)
                .FirstOrDefaultAsync();
            return pictureURL;
        }

        public async Task SetProfilePictureURL(string userId, string url)
        {

            var user = await _context.Users.Where(u => u.Id == userId && u.DateDeleted==null)
                .FirstOrDefaultAsync();
            user.ValidateIfNull("No such user!");
            user.ImageURL = url;
            await _context.SaveChangesAsync();
           
        }
        public async Task<string> GetUsernameById(string id)
        {
            if (id == null)
                return "annonymous";

            var user = await _context.Users.FindAsync(id);
            return user.UserName;
        }

        public async Task<ICollection<AppUserDTO>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(u=>u.DateDeleted==null)
                .ToListAsync().ConfigureAwait(false);
            users.ValidateIfNull();

            var userDTOs = new List<AppUserDTO>();
            foreach (var user in users)
            {
                var newDTO = user.MapToAppUserDTO();
                
                newDTO.Role = await GetRole(user); ;
                userDTOs.Add(newDTO);
            }
            return userDTOs;
        }

        private async Task<string> GetRole(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            string role;
            if (roles.Count == 0)
            {
                role = "No role";
            }
            else
            {
                role = roles[0];
            }
            return role;
        }

        public async Task ConvertToManager(string id)
        {
            var user = await this.GetUserByID(id);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
            await _userManager.RemoveFromRoleAsync(user, role);
            }
            await _userManager.AddToRoleAsync(user, "Manager");
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            //TODO Update?
            var user = await this.GetUserByID(id);
            user.DateDeleted = DateTime.Now.Date;
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser> GetAdmin()
        {
            var adminRole = await _context.Roles.FirstAsync(role => role.Name.ToLower() == "administrator").ConfigureAwait(false);
            var adminId = await _context.UserRoles.FirstAsync(role => role.RoleId == adminRole.Id).ConfigureAwait(false);
            var admin = await GetUserByID(adminId.UserId).ConfigureAwait(false);
            return admin;
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                //Some includes if needed (ban,cancel)
                .FirstOrDefaultAsync(m => m.UserName == username).ConfigureAwait(false);
            return user;
        }

        //public async Task<AppUser> GetImageUrlForUser(string username)
        //{
        //    var user = await _context.Users
        //        //Some includes if needed (ban,cancel)
        //        .FirstOrDefaultAsync(m => m.UserName == username).ConfigureAwait(false);
        //    return user;
        //}


    }
}
