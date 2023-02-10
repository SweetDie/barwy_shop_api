using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile image, string innerFolder = "");
    }
}
