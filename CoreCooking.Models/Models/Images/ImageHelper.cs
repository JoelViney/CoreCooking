using ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Images
{
    public static class ImageHelper
    {
        public static Image<Rgba32> Resize(Image<Rgba32> image, int maxWidth, int maxHeight)
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
                return image;
            }

            var newImage = image.Resize(width, height);

            return newImage;
        }

    }
}
