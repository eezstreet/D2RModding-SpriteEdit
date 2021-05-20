using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace D2RModding_SpriteEdit
{
    static class Program
    {
        static void ConvertSpriteToImage(string[] args, string format)
        {
            var totalFilesConverted = 0;
            if (args.Length <= 1)
            {
                Console.WriteLine("Please specify sprite(s)");
                return;
            }

            for(var i = 1; i < args.Length; i++)
            {
                var fileName = args[i];
                var newPath = Path.ChangeExtension(fileName, format);
                var bytes = File.ReadAllBytes(fileName);

                if(bytes == null)
                {
                    Console.WriteLine("Unable to read " + fileName);
                    continue;
                }
                int x, y;
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
                    Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            var baseVal = (y * width) + (x * 4);
                            bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                        }
                    }
                }

                // now save
                Image image = bmp;
                image.Save(newPath);

                totalFilesConverted++;
            }

            Console.WriteLine("Converted " + totalFilesConverted + " images");
        }
        static void ConvertImageToSprite(string[] args)
        {
            int numConverted = 0;
            if(args.Length <= 1)
            {
                Console.WriteLine("Please specify image(s)");
                return;
            }

            for(var i = 1; i < args.Length; i++)
            {
                string img = args[i];
                Image image = Image.FromFile(img);
                if(image == null)
                {
                    Console.WriteLine("Unable to open " + img);
                    continue;
                }
                string newPath = Path.ChangeExtension(img, ".sprite");
                var f = File.Open(newPath, FileMode.OpenOrCreate, FileAccess.Write);
                if(f == null)
                {
                    Console.WriteLine("Unable to write to " + newPath);
                    continue;
                }
                f.Write(new byte[] { (byte)'S', (byte)'p', (byte)'A', (byte)'1' }, 0, 4);
                f.Write(BitConverter.GetBytes((ushort)31), 0, 2);
                f.Write(BitConverter.GetBytes((ushort)image.Width), 0, 2);
                f.Write(BitConverter.GetBytes((Int32)image.Width), 0, 4);
                f.Write(BitConverter.GetBytes((Int32)image.Height), 0, 4);
                f.Seek(0x14, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes((UInt32)1), 0, 4);
                int x, y;
                Bitmap bmp = new Bitmap(image);
                f.Seek(0x28, SeekOrigin.Begin);
                for (x = 0; x < image.Height; x++)
                {
                    for (y = 0; y < image.Width; y++)
                    {
                        var pixel = bmp.GetPixel(y, x);
                        f.Write(new byte[] { pixel.R, pixel.G, pixel.B, pixel.A }, 0, 4);
                    }
                }
                f.Close();

                numConverted++;
            }

            Console.WriteLine("Converted " + numConverted + " images");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                return;
            }

            if(args[0] == "sprite2png")
            {
                ConvertSpriteToImage(args, "png");
            }
            else if(args[0] == "sprite2bmp")
            {
                ConvertSpriteToImage(args, "bmp");
            }
            else if(args[0] == "sprite2gif")
            {
                ConvertSpriteToImage(args, "gif");
            }
            else if(args[0] == "sprite2tiff")
            {
                ConvertSpriteToImage(args, "tiff");
            }
            else if(args[0] == "sprite2jpg")
            {
                ConvertSpriteToImage(args, "jpg");
            }
            /*else if(args[0] == "frames2png")
            {

            }
            else if(args[0] == "frames2bmp")
            {

            }
            else if(args[0] == "frames2gif")
            {

            }
            else if(args[0] == "frames2tiff")
            {

            }
            else if(args[0] == "frames2jpg")
            {

            }
            else if(args[0] == "frames2sprite")
            {

            }*/
            else if(args[0] == "img2sprite")
            {
                ConvertImageToSprite(args);
            }
            else
            {
                Console.WriteLine("Unknown arg " + args[0]);
            }
        }
    }
}
