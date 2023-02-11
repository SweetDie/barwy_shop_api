using DAL.Entities.Image;
using Infrastructure.Constants;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<BaseFileEntity> UploadImageAsync(IFormFile file, string innerFolders = "")
        {
            try
            {
                var fileExt = Path.GetExtension(file.FileName);
                var fileName = Path.GetRandomFileName() + fileExt;
                var dir = Path.Combine(ImagesConstants.ImagesFolder, innerFolders);
                var fullName = Path.Combine(dir, fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), fullName);

                await using (var stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }

                return new BaseFileEntity
                {
                    FileName = fileName,
                    Path = dir,
                    FullName = fullName
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TFileEntity> UploadImageAsync<TFileEntity>(IFormFile file, string innerFolders = "")
            where TFileEntity : BaseFileEntity, new()
        {
           return (TFileEntity)await UploadImageAsync(file, innerFolders);
        }

        public bool DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            try
            {
                if (!File.Exists(fullPath)) return true;
                File.Delete(fullPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}