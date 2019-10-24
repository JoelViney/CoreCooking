using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;

namespace CoreCooking.Models.Images
{
    public static class ImageHelper
    {
        public static void Resize(Image<Rgba32> image, int maxWidth, int maxHeight)
        {
            double coefHeight = ((double)maxHeight / (double)image.Height);
            double coefWidth = ((double)maxWidth / (double)image.Width);
            double coef;

            if (coefHeight < coefWidth)
                coef = coefHeight;
            else
                coef = coefWidth;

            int height = System.Convert.ToInt32(image.Height * coef);
            int width = System.Convert.ToInt32(image.Width * coef);

            if (image.Height <= height && image.Width <= width)
            {
                return;
            }
            
            image.Mutate(x => x.Resize(width, height)); // resize the image in place and return it for chaining
        }
    }
}
