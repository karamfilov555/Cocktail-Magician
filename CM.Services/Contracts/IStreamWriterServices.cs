using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IStreamWriterServices
    {
        Task WriteToFile(string recepie, string cocktailId);
    }
}
