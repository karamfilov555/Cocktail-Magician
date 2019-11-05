using Microsoft.AspNetCore.Http;

namespace CM.Services
{
    public interface IFileUploadService
    {
        string UploadFile(IFormFile uploadedImage);
    }
}