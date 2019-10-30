using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<List<AppUserDTO>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync().ConfigureAwait(false);


            var userDTOs = new List<AppUserDTO>();
            foreach (var user in users)
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
                var newDTO = user.MapToAppUserDTO();
                newDTO.Role = role;
                userDTOs.Add(newDTO);
            }
            return userDTOs;
        }

        public async Task ConvertToManager(string id)
        {
            var user = await _context.Users.FindAsync(id);
            user.ValidateIfNull();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
            await _userManager.RemoveFromRoleAsync(user, role);
            }
            await _userManager.AddToRoleAsync(user, "Manager");
            await _context.SaveChangesAsync();
        }

    }
}
