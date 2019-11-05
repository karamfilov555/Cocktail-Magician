using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CM.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _environment;

        public FileUploadService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public string UploadFile(IFormFile uploadedImage)
        {

            if (uploadedImage != null)
            {
                string uniqueFileName = GetUniqueFileName(uploadedImage.FileName);
                var uploads = Path.Combine(_environment.WebRootPath, "images");
                var filePath = Path.Combine(uploads, uniqueFileName);
                uploadedImage.CopyTo(new FileStream(filePath, FileMode.Create));
                return "/images/" + uniqueFileName;
            }
            else
            {
                throw new InvalidOperationException("No image is uploaded");
            }

        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
