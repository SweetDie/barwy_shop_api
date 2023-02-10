using Infrastructure.Constants;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImageAsync(IFormFile image, string innerFolder = "")
        {
            try
            {
                var fileExp = Path.GetExtension(image.FileName);
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Images", innerFolder);
                string fileName = Path.GetRandomFileName() + fileExp;
                string path = Path.Combine(dir, fileName);

                using (var stream = File.Create(path))
                {
                    await image.CopyToAsync(stream);
                }

                return Path.Combine(innerFolder, fileName);
            }
            catch (Exception)
            {

                return ImagesConstants.ProductWithoutImage;
            }
        }
    }
}
