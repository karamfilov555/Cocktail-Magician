using System.Collections.Generic;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Models;

namespace CM.Services.Contracts
{
    public interface IAppUserServices
    {
        Task<ICollection<AppUserDTO>> GetAllUsers();
        Task ConvertToManager(string id);
        Task Delete(string id);
        Task<string> GetUsernameById(string id);
        Task<AppUser> GetAdmin();
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}