using System.Drawing;
using System.Drawing.Imaging;

namespace Aspose.Core.Utils
{
    public static class ImageUtils
    {
        public static bool TryToGetJpgImage(Stream stream, out byte[] jpg)
        {
            try
            {
                var img = Image.FromStream(stream);
                using (var mStream = new MemoryStream())
                {
                    img.Save(mStream, ImageFormat.Png);
                    jpg = mStream.ToArray();
                }

                return true;
            }
            catch
            {
                jpg = null;
                return false;
            }
        }
    }
}
