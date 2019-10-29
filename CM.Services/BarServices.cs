using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class BarServices : IBarServices
    {
        private readonly CMContext _context;
        public BarServices(CMContext context)
        {
            _context = context;
        }

        public async Task<ICollection<BarDTO>> GetHomePageBars()
        {
            //TODO reduce number and get only required fields
            var bars = await _context.Bars.Where(b=>b.DateDeleted==null)
                .ToListAsync()
                .ConfigureAwait(false);
            var barDTOs = bars.Select(b => b.MapBarToDTO()).ToList();
            return barDTOs;
        }

    }

}
