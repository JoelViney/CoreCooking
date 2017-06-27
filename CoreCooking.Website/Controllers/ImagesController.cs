using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCooking.Models.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;

namespace CoreCooking.Website.Controllers
{
    public class ImagesController : Controller
    {
        private Settings _settings;

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