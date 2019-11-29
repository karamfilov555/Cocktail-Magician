using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IStreamWriterServices
    {
        void WriteToFile(string recepie, string cocktailId);
    }
}
