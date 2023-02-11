using DAL.Entities.Image;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces
{
    public interface IFileService
    {
        Task<TFileEntity> UploadImageAsync<TFileEntity>(IFormFile file, string innerFolders = "")
            where TFileEntity : BaseFileEntity, new();
        Task<BaseFileEntity> UploadImageAsync(IFormFile file, string innerFolders = "");
        bool DeleteFile(string path);
    }
}
