using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CM.Services
{
    public interface IFileUploadService
    {
        string UploadFile(IFormFile uploadedImage);
        string SetUniqueImagePathForBar(IFormFile uploadedImage);
        string SetUniqueImagePathForCocktail(IFormFile uploadedImage);
    }
}