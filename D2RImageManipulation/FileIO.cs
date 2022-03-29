using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using TGASharpLib;

namespace D2RImageManipulation
{
    public class FileIO
    {
        public enum Format
        {
            Sprite,
            BMP,
            GIF,
            JPG,
            PNG,
            TIFF,
            TGA
        }

        public static IReadOnlyDictionary<Format, String[]> extensions = new Dictionary<Format, String[]>()
        {
            { Format.Sprite, new String[] { ".sprite" } },
            { Format.BMP, new String[] { ".bmp" } },
            { Format.GIF, new String[] { ".gif" } },
            { Format.JPG, new String[] { ".jpg", ".jpeg" } },
            { Format.PNG, new String[] { ".png" } },
            { Format.TIFF, new String[] { ".tif", ".tiff" } },
            { Format.TGA, new String[] { ".tga" } }
        };

        public static IReadOnlyDictionary<String, Format> formats = new Dictionary<String, Format>()
        {
            { ".sprite", Format.Sprite },
            { ".bmp", Format.BMP },
            { ".gif", Format.GIF},
            { ".jpg", Format.JPG },
            { ".jpeg", Format.JPG },
            { ".png", Format.PNG },
            { ".tif", Format.TIFF },
            { ".tiff", Format.TIFF },
            { ".tga", Format.TGA }
        };

        public static Image loadAsImage(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var format = formats[ext];
            Image img;
            switch (format)
            {
                case Format.Sprite:
                    img = Converters.SpriteToBitmap.Invoke(new Sprite(fileName));
                    break;
                case Format.TGA:
                    img = TGA.FromFile(fileName).ToBitmap();
                    break;
                default:
                    img = Image.FromFile(fileName);
                    break;
            }
            return img;
        }

        public static void saveAs(Image image, string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var format = formats[ext];
            saveAs(image, format, fileName);
        }

        public static void saveAs(Sprite sprite, string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var format = formats[ext];
            saveAs(sprite, format, fileName);
        }

        public static void saveAs(Image image, FileIO.Format format, String fileName)
        {
            switch (format)
            {
                case Format.Sprite:
                    saveSprite(Converters.ImageToSprite.Invoke(image), fileName);
                    break;
                case Format.TGA:
                    saveTGA(new Bitmap(image), fileName);
                    break;
                default:
                    saveImage(image, format, fileName);
                    break;
            }
        }

        public static void saveAs(Sprite sprite, FileIO.Format format, String fileName)
        {
            switch (format)
            {
                case Format.Sprite:
                    saveSprite(sprite, fileName);
                    break;
                case Format.TGA:
                    saveTGA(Converters.SpriteToBitmap.Invoke(sprite), fileName);
                    break;
                default:
                    saveImage(Converters.SpriteToBitmap.Invoke(sprite), format, fileName);
                    break;
            }
        }

        private static void saveImage(Image image, FileIO.Format format, String fileName)
        {
            var newPath = Path.ChangeExtension(fileName, extensions[format][0]);
            image.Save(newPath);
        }

        private static void saveTGA(Bitmap image, String fileName)
        {
            var newPath = Path.ChangeExtension(fileName, extensions[Format.TGA][0]);
            var tga = TGA.FromBitmap(image);
            tga.Save(newPath);
        }

        private static void saveSprite(Sprite sprite, String fileName)
        {
            var newPath = Path.ChangeExtension(fileName, extensions[Format.Sprite][0]);
            using (var f = File.Open(newPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                if (f != null)
                {
                    var bytes = sprite.GetBytes();
                    f.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    throw new IOException(String.Format("Could not write to {0}", newPath));
                }
            }
        }
    }
}
