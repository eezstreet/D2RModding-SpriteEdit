using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace D2RImageManipulation
{
    public static class Converters
    {
        public static readonly Converter<Image, Sprite> ImageToSprite
            = new Converter<Image, Sprite>((image) => {
                const int headerSize = 40;
                using (var stream = new MemoryStream(headerSize + (image.Height * image.Width * 4)))
                {
                    stream.Write(new byte[] { (byte)'S', (byte)'p', (byte)'A', (byte)'1' }, 0, 4);
                    stream.Write(BitConverter.GetBytes((ushort)31), 0, 2);
                    stream.Write(BitConverter.GetBytes((ushort)image.Width), 0, 2);
                    stream.Write(BitConverter.GetBytes((Int32)image.Width), 0, 4);
                    stream.Write(BitConverter.GetBytes((Int32)image.Height), 0, 4);
                    stream.Seek(0x14, SeekOrigin.Begin);
                    stream.Write(BitConverter.GetBytes((UInt32)1), 0, 4);
                    int x, y;
                    Bitmap bmp = new Bitmap(image);
                    stream.Seek(0x28, SeekOrigin.Begin);
                    for (x = 0; x < image.Height; x++)
                    {
                        for (y = 0; y < image.Width; y++)
                        {
                            var pixel = bmp.GetPixel(y, x);
                            stream.Write(new byte[] { pixel.R, pixel.G, pixel.B, pixel.A }, 0, 4);
                        }
                    }
                    return new Sprite(stream.ToArray());
                }
            });

        public static readonly Converter<Sprite, Bitmap> SpriteToBitmap
            = new Converter<Sprite, Bitmap>((sprite) =>
            {
                int x, y;
                var bytes = sprite.getBytes();
                var version = BitConverter.ToUInt16(bytes, 4);
                var width = BitConverter.ToInt32(bytes, 8);
                var height = BitConverter.ToInt32(bytes, 0xC);
                var bmp = new Bitmap(width, height);

                if (version == 31)
                {   // regular RGBA
                    for (x = 0; x < height; x++)
                    {
                        for (y = 0; y < width; y++)
                        {
                            var baseVal = 0x28 + x * 4 * width + y * 4;
                            bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                        }
                    }
                }
                else if (version == 61)
                {   // DXT
                    var tempBytes = new byte[width * height * 4];
                    DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            var baseVal = (y * width) + (x * 4);
                            bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                        }
                    }
                }

                return bmp;
            });
    }
}
