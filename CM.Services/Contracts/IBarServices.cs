using System.Collections.Generic;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Services.Common;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
        Task<ICollection<BarDTO>> GetHomePageBars();
        Task<BarDTO> GetBarByID(string id);
        Task<PaginatedList<BarDTO>> GetAllBars(int? pageNumber, string sortOrder);
        Task<string> AddBar(BarDTO barDTO);
        Task<string> Delete(string id);
        Task<string> Update(BarDTO barDTO);
        Task<ICollection<BarDTO>> GetAllBarsByName(string searchCriteria);

    }
}