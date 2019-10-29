using CM.DTOs;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class BarMapper
    {
        public static BarViewModel MapBarToVM(this BarDTO bar)
        {
            var newBarVM = new BarViewModel();
            newBarVM.Id = bar.Id;
            newBarVM.Name = bar.Name;
            newBarVM.ImageURL = bar.ImageUrl;
            newBarVM.Website = bar.Website;

            return newBarVM;
        }
    }
}
