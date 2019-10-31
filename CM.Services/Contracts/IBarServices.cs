using System.Collections.Generic;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
        Task<ICollection<BarDTO>> GetHomePageBars();
        Task<BarDTO> GetBarByID(string id);
        Task<ICollection<BarDTO>> GetAllBars();

    }
}