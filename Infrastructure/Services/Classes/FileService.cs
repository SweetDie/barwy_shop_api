using DAL.Entities.Image;
using Infrastructure.Constants;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<BaseImage> UploadImageAsync(IFormFile image, string innerFolder = "")
        {
            try
            {
                var fileExt = Path.GetExtension(image.FileName);
                var fileName = Path.GetRandomFileName() + fileExt;
                var dir = Path.Combine(ImagesConstants.ImagesFolder, innerFolder);
                var fullName = Path.Combine(dir, fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), fullName);

                await using (var stream = File.Create(path))
                {
                    await image.CopyToAsync(stream);
                }

                return new ProductImage
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
    }
}