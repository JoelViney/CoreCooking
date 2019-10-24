using CoreCooking.Models.Images;
using CoreCooking.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreCooking.Website.Controllers
{
    public class ImagesController : Controller
    {
        private readonly Settings _settings;

        public ImagesController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Json(new { success = false, message = "The file was empty." });

                ImageRepository repository = new ImageRepository(_settings.AzureStorageConnectionString);

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    stream.Position = 0;
                    string url = await repository.SaveAsync(stream);

                    return Json(new { success = true, url = url });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}