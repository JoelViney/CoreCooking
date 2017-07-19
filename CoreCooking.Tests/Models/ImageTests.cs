using CoreCooking.Models.Images;
using ImageSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreCooking.Models
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void ResizeImage()
        {
            // Arrange
            string filePathName = Path.Combine(Directory.GetCurrentDirectory(), @"Data\Images\600x400.jpg");
            string destinationFilePathName = Path.Combine(Directory.GetCurrentDirectory(), @"Data\Images\480x240.jpg");

            using (FileStream fileStream = new FileStream(filePathName, FileMode.Open))
            {
                using (var image = Image.Load<Rgba32>(fileStream))
                {
                    var image2 = ImageHelper.Resize(image, 480, 320);

                    Assert.AreEqual(480, image2.Width);
                    Assert.AreEqual(320, image2.Height);

                    image2.Save(destinationFilePathName);
                }
            }
        }
    }
}
