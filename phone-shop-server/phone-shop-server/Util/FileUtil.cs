using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using Microsoft.Extensions.Hosting;

namespace phone_shop_server.Util
{
    public interface IFileUtil
    {
        Task<string> UploadAsync(IFormFile fileUpload);
        Task<IEnumerable<string>> MultiUploadAsync(IFormFile[] files);
    }
    public class FileUtil : IFileUtil
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public FileUtil
            (
            IConfiguration configuration,
            IWebHostEnvironment environment
            )
        {
            _environment = environment;
            _configuration = configuration;
        }
        public async Task<string> UploadAsync(IFormFile fileUpload)
        {
            try
            {
                var cloudinary = new Cloudinary(_configuration["CLOUDINARY_URL"].ToString());
                cloudinary.Api.Secure = true;
                var basePath = Path.Combine(_environment.WebRootPath, "upload");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var filepath = basePath + @$"/fileupload-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg";
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }
                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(filepath),
                    UseFilename = true,
                    Folder = "hotel_management",
                    UniqueFilename = true,
                    Overwrite = true
                };
                var result = await cloudinary.UploadAsync(uploadParam);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                return result.Url.ToString();
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<string>> MultiUploadAsync(IFormFile[] files)
        {
            try
            {
                var cloudinary = new Cloudinary(_configuration["CLOUDINARY_URL"].ToString());
                cloudinary.Api.Secure = true;
                var basePath = Path.Combine(_environment.WebRootPath, "upload");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var result = new List<string>();
                foreach (var file in files)
                {
                    var filepath = basePath + @$"/image-upload-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg";
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var uploadParam = new ImageUploadParams()
                    {
                        File = new FileDescription(filepath),
                        UseFilename = true,
                        Folder = "hotel_management",
                        UniqueFilename = true,
                        Overwrite = true
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParam);
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    result.Add(uploadResult.Url.ToString());
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
