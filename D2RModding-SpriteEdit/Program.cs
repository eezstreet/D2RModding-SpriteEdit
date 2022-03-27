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
using D2RImageManipulation;

namespace D2RModding_SpriteEdit
{
    static class Program
    {
        static readonly ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 10
        };

        static void ForAllFiles(string[] args, Action<string> fileAction)
        {
            var filesSkipped = new System.Collections.Concurrent.BlockingCollection<String>();
            var filesFailed = new System.Collections.Concurrent.ConcurrentDictionary<String, String>();
            if (args.Length <= 1)
            {
                Console.WriteLine("Please specify file(s)");
                return;
            }

            var filesArr = new string[args.Length - 1];
            Array.Copy(args, 1, filesArr, 0, args.Length - 1);
            Parallel.ForEach(filesArr, parallelOptions, fileName =>
            {
                try
                {
                    if (File.Exists(fileName))
                    {

                    }
                    else
                    {
                        filesSkipped.Add(fileName);
                    }
                }
                catch (Exception e)
                {
                    filesFailed.TryAdd(fileName, e.Message);
                }
            });

            if (filesSkipped.Count > 0)
            {
                Console.WriteLine(String.Format("Skipped {0} Files:", filesSkipped.Count));
                foreach (var file in filesSkipped)
                {
                    Console.WriteLine(String.Format(" * {0}", file));
                }

            }

            if (filesFailed.Count > 0)
            {
                Console.WriteLine(String.Format("Failed to convert {0} Files:", filesFailed.Count));
                foreach (var file in filesFailed)
                {
                    Console.WriteLine(String.Format(" * {0}: with error \n      {1}", file.Key, file.Value));
                }
            }
        }

        static void ConvertSpriteToImage(string[] args, string format)
        {
            ForAllFiles(args, (fileName) =>
            {
                var newPath = Path.ChangeExtension(fileName, format);
                var sprite = new Sprite(fileName);

                // now save
                Image image = sprite.asBitmap;
                image.Save(newPath);
                Console.WriteLine(String.Format("Successfully converted {0}", newPath));
            });
        }

        static void ConvertImageToSprite(string[] args)
        {
            ForAllFiles(args, (fileName) =>
            {
                Image image = Image.FromFile(fileName);
                if (image != null)
                {
                    var newPath = Path.ChangeExtension(fileName, ".sprite");
                    using (var f = File.Open(newPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        if (f != null)
                        {
                            var sprite = new Sprite(image);
                            var bytes = sprite.asBytes;
                            f.Write(bytes, 0, bytes.Length);
                            Console.WriteLine(String.Format("Successfully converted {0}", newPath));
                        }
                        else
                        {
                            throw new IOException(String.Format("Could not write to {0}", newPath));
                        }
                    }
                }
                else
                {
                    throw new IOException(String.Format("Could not read {0} as image", fileName));
                }
            });
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
