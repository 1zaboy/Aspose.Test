using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Core.Utils;
using Aspose.Data.Models;

namespace Aspose.Core.Cropper;

public class CropperHandler
{
    private byte[] m_Bytes;

    public CropperHandler(byte[] bytes)
    {
        m_Bytes = bytes;
    }

    public async Task<byte[]> Crop(CropOptions options)
    {
        using var image = ByteToImage();
        var cropImage = CropImage(image, options);
        return ByteToImage(cropImage);
    }

    private Image ByteToImage()
    {
        using var mStream = new MemoryStream(m_Bytes);
        var image = Image.FromStream(mStream);
        return image;
    }

    private int OutOfRange(int point, int startPoint, int endPoint, int shift)
    {
        var value = shift;
        var calcPoint = point + shift;

        if (calcPoint < startPoint)
        {
            value = point;
        }

        if (endPoint < calcPoint)
        {
            value = endPoint - point;
        }

        return value;
    }

    private Image CropImage(Image image, CropOptions options)
    {
        var pointX = RangeUtils.OutOfRange(options.StartPoint.X, 0, image.Width);
        var pointY = RangeUtils.OutOfRange(options.StartPoint.Y, 0, image.Height);
        var width = OutOfRange(pointX, 0, image.Width, options.Width);
        var height = OutOfRange(pointY, 0, image.Height, options.Height);

        if (width == 0 || height == 0)
        {
            throw new Exception("Image not selected");
        }

        var oldImage = new Bitmap(image);
        var newImage = new Bitmap(Math.Abs(width), Math.Abs(height));
        var startPoint = options.StartPoint;

        if (width < 0)
        {
            startPoint.X -= width;
        }

        if (height < 0)
        {
            startPoint.Y -= height;
        }

        for (var i = 0; i < Math.Abs(width); i++)
        {
            for (var ii = 0; ii < Math.Abs(height); ii++)
            {
                var oldPixel = oldImage.GetPixel(i + startPoint.X, ii + startPoint.Y);
                newImage.SetPixel(i, ii, oldPixel);
            }
        }

        return newImage;
    }

    private byte[] ByteToImage(Image image)
    {
        using var mStream = new MemoryStream();
        image.Save(mStream, ImageFormat.Png);
        return mStream.ToArray();
    }
}