using CoreCooking.Models.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

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
                    // Act
                    ImageHelper.Resize(image, 480, 320);

                    // Assert
                    Assert.AreEqual(480, image.Width);
                    Assert.AreEqual(320, image.Height);

                    image.Save(destinationFilePathName);
                }
            }
        }
    }
}
