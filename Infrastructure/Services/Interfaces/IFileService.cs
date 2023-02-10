using DAL.Entities.Image;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces
{
    public interface IFileService
    {
        Task<BaseImage> UploadImageAsync(IFormFile image, string innerFolder = "");
    }
}
