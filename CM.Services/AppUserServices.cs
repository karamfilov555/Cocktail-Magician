using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
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


        public AppUserServices(CMContext context, UserManager<AppUser> userManager)//tested
        {
            _context = context
                            ?? throw new MagicException(ExceptionMessages.ContextNull);
            _userManager = userManager
                            ?? throw new MagicException(ExceptionMessages.UserManagerNull);
        }
        public async Task<AppUser> GetUserByID(string id) //tested
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            var user = await _context.Users.FindAsync(id);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            return user;
        }

        public async Task<AppUserDTO> GetUserDToByID(string id)//tested
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            var user = await this.GetUserByID(id);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            var userDTO = user.MapToAppUserDTO();
            user.ValidateIfNull(ExceptionMessages.AppUserDtoNull);
            userDTO.Role = await this.GetRole(user);
            return userDTO;
        }

        public async Task<string> GetProfilePictureURL(string id) // tested
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            var pictureURL = await _context.Users.Where(u => u.Id == id)
                .Select(u => u.ImageURL)
                .FirstOrDefaultAsync();
            return pictureURL;
        }

        public async Task SetProfilePictureURL(string userId, string url) //tested
        {
            userId.ValidateIfNull(ExceptionMessages.IdNull);
            var user = await _context.Users.Where(u => u.Id == userId && u.DateDeleted == null)
                .FirstOrDefaultAsync();
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            user.ImageURL = url;
            await _context.SaveChangesAsync();

        }
        public async Task<string> GetUsernameById(string id) //tested
        {
            if (id == null)
                return "annonymous";
            var user = await _context.Users.FindAsync(id);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            return user.UserName;
        }

        public async Task<ICollection<AppUserDTO>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(u => u.DateDeleted == null)
                .ToListAsync().ConfigureAwait(false);

            users.ValidateIfNull(); // tested, without this validation

            var userDTOs = new List<AppUserDTO>();
            foreach (var user in users)
            {
                var newDTO = user.MapToAppUserDTO();

                newDTO.Role = await GetRole(user);
                userDTOs.Add(newDTO);
            }
            return userDTOs;
        }

        public async Task<string> GetRole(AppUser user) //tested
        {
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
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

        public async Task ConvertToManager(string id) //tested
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            var user = await this.GetUserByID(id);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, roles);

            //foreach (var role in roles)
            //{
            //    await _userManager.RemoveFromRoleAsync(user, role);
            //}
            await _userManager.AddToRoleAsync(user, "Manager");
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)//tested
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            var user = await this.GetUserByID(id);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            user.DateDeleted = DateTime.Now.Date;
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser> GetAdmin()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(role => role.Name.ToLower() == "administrator").ConfigureAwait(false);
            adminRole.ValidateIfNull(ExceptionMessages.RoleNull);
            var adminId = await _context.UserRoles.FirstOrDefaultAsync(role => role.RoleId == adminRole.Id).ConfigureAwait(false);
            adminId.ValidateIfNull(ExceptionMessages.UserRoleNull);
            var admin = await GetUserByID(adminId.UserId).ConfigureAwait(false);
            admin.ValidateIfNull(ExceptionMessages.AppUserNull);
            return admin;
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            username.ValidateIfNull(ExceptionMessages.UserNameNull);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == username).ConfigureAwait(false);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            return user;
        }

    }
}
