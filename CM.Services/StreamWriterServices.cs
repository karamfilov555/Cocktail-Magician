using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class StreamWriterServices : IStreamWriterServices
    {
        public StreamWriterServices()
        {

        }
        public async Task WriteToFile(string recepie, string cocktailId)
        {

            // CARE THIS PATH WONT WORK on othr pc!!!!!!!!!!
            string docPath = "wwwroot/assets/recepies";

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{cocktailId}.txt")))
            {
                    outputFile.WriteLine(recepie);
            }
        }

    }
}
