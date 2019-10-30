using System.Collections.Generic;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services
{
    public interface IAppUserServices
    {
        Task<List<AppUserDTO>> GetAllUsers();
        Task ConvertToManager(string id);
    }
}