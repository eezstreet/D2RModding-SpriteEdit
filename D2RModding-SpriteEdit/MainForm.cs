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
    public partial class MainForm : Form
    {
        private bool needToSave = false;
        private uint _currentFrameCount;
        private readonly string _filterExtensions = "PNG|*.png|BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|TIFF|*.tif;*.tiff|"
                + "All Graphics Types|*.png;*.bmp;*.jpg;*.jpeg;*.tif;*.tiff|"
                + "All Files (*.*)|*.*";
        private readonly string _defaultExtensions = "*.png;*.bmp;*.jpg;*.jpeg;*.tif;*.tiff";

        private uint CurrentFrameCount
        {
            get
            {
                return _currentFrameCount;
            }
            set
            {
                if(value > 0 && !HasFrames)
                {
                    HasFrames = true;
                }
                _currentFrameCount = value;
                // reset current frame if above count
                if(CurrentlyViewedFrame >= value)
                {
                    CurrentlyViewedFrame = 0;
                }
                // set frame count textbox
                if(numFramesTextBox.Text != "" + value)
                {
                    numFramesTextBox.Text = "" + value;
                }

                // add items to the viewer
                int[] items = new int[value];
                for(var i = 0; i < value; i++)
                {
                    items[i] = i;
                }

                frameSelectionComboBox.ComboBox.DataSource = items;
                
                ResetPreview();
            }
        }

        private bool _hasFrames;
        private bool HasFrames
        {
            get
            {
                return _hasFrames;
            }
            set
            {
                _hasFrames = value;
                frameSelectionComboBox.Enabled = value;
                exportFramesToolStripMenuItem.Enabled = value;
                exportFrameToolStripMenuItem.Enabled = value;
                importFrameToolStripMenuItem.Enabled = value;

                ResetPreview();
            }
        }

        private uint _currentlyViewedFrame;
        private uint CurrentlyViewedFrame
        {
            get
            {
                return _currentlyViewedFrame;
            }
            set
            {
                _currentlyViewedFrame = value;
                ResetPreview();
            }
        }

        private Point _currentPan;
        private Point CurrentPan
        {
            get
            {
                return _currentPan;
            }
            set
            {
                _currentPan = value;
                ResetPreview();
            }
        }

        private void ResetPreview()
        {
            if(CurrentImage == null)
            {
                return;
            }
            int sourceWidth = CurrentImage.Width;
            int sourceHeight = CurrentImage.Height;
            int newWidth, newHeight, frameWidth = 0;
            if (HasFrames && CurrentFrameCount > 0)
            {
                frameWidth = (int)(sourceWidth / (float)CurrentFrameCount);
                newWidth = (int)(CurrentZoom * frameWidth);
            }
            else
            {
                newWidth = (int)(CurrentZoom * sourceWidth);
            }
            newHeight = (int)(CurrentZoom * sourceHeight);


            Bitmap temp = new Bitmap(newWidth, newHeight);
            Graphics bmGraphics = Graphics.FromImage(temp);
            bmGraphics.Clear(imagePreview.BackColor);
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if(HasFrames && CurrentFrameCount > 0)
            {
                bmGraphics.DrawImage(CurrentImage,
                    new Rectangle((int)(CurrentPan.X * CurrentZoom), (int)(CurrentPan.Y * CurrentZoom), newWidth, newHeight),
                    new Rectangle(frameWidth * (int)CurrentlyViewedFrame, 0, frameWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            else
            {
                bmGraphics.DrawImage(CurrentImage,
                    new Rectangle((int)(CurrentPan.X * CurrentZoom), (int)(CurrentPan.Y * CurrentZoom), newWidth, newHeight),
                    new Rectangle(0, 0, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            
            bmGraphics.Dispose();
            imagePreview.Image = temp;
        }

        private float _currentZoom = 1.0f;
        private float CurrentZoom
        {
            get
            {
                return _currentZoom;
            }
            set
            {
                zoomAmountLabel.Text = string.Format("{0}%", CurrentZoom * 100.0f);
                _currentZoom = value;
                ResetPreview();
            }
        }

        private Image _currentImage;
        private Image CurrentImage
        {
            get
            {
                return _currentImage;
            }
            set
            {
                exportToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                rotate90ToolStripMenuItem.Enabled = true;
                rotate_180.Enabled = true;
                rotate_90_clockwise.Enabled = true;
                rotate_90_counterclockwise.Enabled = true;
                filter_hsv.Enabled = true;
                flip_horizontal.Enabled = true;
                flip_vertical.Enabled = true;
                _currentImage = value;
                numFramesTextBox.Enabled = true;
                ResetPreview();
            }
        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void SaveAsSprite(Image img, uint fc, string fileName)
        {
            var f = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            if(fc == 0)
            {
                fc = 1;
            }

            f.Write(new byte[] { (byte)'S', (byte)'p', (byte)'A', (byte)'1' }, 0, 4);
            f.Write(BitConverter.GetBytes((ushort)31), 0, 2);
            f.Write(BitConverter.GetBytes((ushort)img.Width / fc), 0, 2);
            f.Write(BitConverter.GetBytes((Int32)img.Width), 0, 4);
            f.Write(BitConverter.GetBytes((Int32)img.Height), 0, 4);
            f.Seek(0x14, SeekOrigin.Begin);
            f.Write(BitConverter.GetBytes((UInt32)fc), 0, 4);
            int x, y;
            Bitmap bmp = new Bitmap(img);
            f.Seek(0x28, SeekOrigin.Begin);
            for (x = 0; x < img.Height; x++)
            {
                for (y = 0; y < img.Width; y++)
                {
                    var pixel = bmp.GetPixel(y, x);
                    f.Write(new byte[] { pixel.R, pixel.G, pixel.B, pixel.A }, 0, 4);
                }
            }
            f.Close();
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*",
                DefaultExt = ".sprite"
            };

            if (needToSave)
            {
                if (MessageBox.Show("Continue without saving?", "Notification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // open up the image
                var fileName = dlg.FileName;
                var bytes = File.ReadAllBytes(fileName);
                var width = BitConverter.ToInt32(bytes, 8);
                var height = BitConverter.ToInt32(bytes, 0xC);
                var bmp = CreateBitmapFromSpriteString(fileName);

                CurrentImage = bmp;
                toolbarText.Text = string.Format("{0}x{1}", width, height);
                Text = "SpriteEdit - " + fileName;
            }
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*",
                DefaultExt = ".sprite"
            };

            if (CurrentImage == null)
            {
                MessageBox.Show("You haven't loaded an image yet.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                SaveAsSprite(CurrentImage, CurrentFrameCount, dlg.FileName);
                var splitName = dlg.FileName.Split('.');
                var lowend = ".lowend.";
                SaveAsSprite(ResizeImage(CurrentImage, CurrentImage.Width / 2, CurrentImage.Height / 2), CurrentFrameCount, splitName[0] + lowend + splitName[1]);
                needToSave = false;
            }
        }

        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (needToSave)
            {
                if(MessageBox.Show("Continue without saving?", "Notification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                CurrentImage = Image.FromFile(fileName);
                toolbarText.Text = string.Format("{0}x{1}", CurrentImage.Width, CurrentImage.Height);
                Text = "SpriteEdit - " + dlg.FileName;
            }
        }
        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (CurrentImage == null)
            {
                MessageBox.Show("You need to open or import an image first.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                CurrentImage.Save(fileName);
                needToSave = false;
            }
        }

        private void MassExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*",
                DefaultExt = ".sprite",
                Multiselect = true
            };

            if (MessageBox.Show("First select the sprites you would like to convert", "Notification", MessageBoxButtons.OK) == DialogResult.OK)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show("Now select the directory you would like to export to.", "Notification", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        var folderBrowserDialog = new FolderBrowserDialog
                        {
                            Description = "Select the directory you would like to export to."
                        };

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var file in dlg.FileNames)
                            {
                                var bmp = CreateBitmapFromSpriteString(file);
                                var newPath = Path.ChangeExtension(file, "png");
                                var fileName = newPath.Split('\\');
                                Image image = bmp;
                                image.Save(folderBrowserDialog.SelectedPath + "/" + fileName[fileName.Length - 1]);
                            }
                            MessageBox.Show("Sprites successfully exported.");
                        }
                    }
                }
            }
        }

        private Bitmap CreateBitmapFromSpriteString(string file)
        {
            var bytes = File.ReadAllBytes(file);
            int x, y;
            var version = BitConverter.ToUInt16(bytes, 4);
            var width = BitConverter.ToInt32(bytes, 8);
            var height = BitConverter.ToInt32(bytes, 0xC);
            var bmp = new Bitmap(width, height);
            CurrentFrameCount = BitConverter.ToUInt32(bytes, 0x14);


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
            return bmp;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void D2RModdingDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/gvsJBRd4KZ");
        }
        private void ZoomTrackBar_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            var value = tb.Value;

            CurrentZoom = value / 100.0f;
        }

        private bool isPanning = false;
        private Point mouseDownLocation;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            isPanning = true;
            mouseDownLocation = e.Location;
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            isPanning = false;
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if(isPanning)
            {
                CurrentPan = new Point(e.Location.X - mouseDownLocation.X, e.Location.Y - mouseDownLocation.Y);
            }
        }
        private void OnMouseLeave(object sender, EventArgs e)
        {
            isPanning = false;
        }
        private void OnResetPan(object sender, EventArgs e)
        {
            CurrentPan = new Point(0, 0);
        }
        private void Rotate_180_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(CurrentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            CurrentImage = temp;
            needToSave = true;
        }
        private void Rotate_90_clockwise_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(CurrentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            CurrentImage = temp;
            needToSave = true;
        }
        private void Rotate_90_counterclockwise_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(CurrentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            CurrentImage = temp;
            needToSave = true;
        }
        private void Flip_horizontal_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(CurrentImage);
            temp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            CurrentImage = temp;
            needToSave = true;
        }
        private void Flip_vertical_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(CurrentImage);
            temp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            CurrentImage = temp;
            needToSave = true;
        }

        private float pendingHue;
        private float pendingSaturation;
        private float pendingValue;
        private Image unmodifiedImage;
        private ImageAttributes imageAttributes = new ImageAttributes();
        private ColorMatrix colorMatrix = new ColorMatrix();
        private Graphics gfx;

        private void AdjustForHSV()
        {
            Bitmap temp = new Bitmap(unmodifiedImage);
            CurrentImage.Dispose();
            CurrentImage = new Bitmap(temp);
            gfx.Dispose();
            gfx = Graphics.FromImage(CurrentImage);

            // hue
            float theta = (pendingHue - 0.5f) * 2 * 3.14159f;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            colorMatrix[0, 0] = 0.213f + 0.787f * c - 0.213f * s;
            colorMatrix[0, 1] = 0.213f - 0.213f * c + 0.413f * s;
            colorMatrix[0, 2] = 0.213f - 0.213f * c - 0.787f * s;
            colorMatrix[0, 3] = 0.0f;
            colorMatrix[0, 4] = 0.0f;
            colorMatrix[1, 0] = 0.715f - 0.715f * c - 0.715f * s;
            colorMatrix[1, 1] = 0.715f + 0.285f * c + 0.140f * s;
            colorMatrix[1, 2] = 0.715f - 0.715f * c + 0.715f * s;
            colorMatrix[1, 3] = 0.0f;
            colorMatrix[1, 4] = 0.0f;
            colorMatrix[2, 0] = 0.072f - 0.072f * c + 0.928f * s;
            colorMatrix[2, 1] = 0.072f - 0.072f * c - 0.283f * s;
            colorMatrix[2, 2] = 0.072f + 0.928f * c + 0.072f * s;
            colorMatrix[2, 3] = 0.0f;
            colorMatrix[2, 4] = 0.0f;
            colorMatrix[3, 0] = 0.0f;
            colorMatrix[3, 1] = 0.0f;
            colorMatrix[3, 2] = 0.0f;
            colorMatrix[3, 3] = 1.0f;
            colorMatrix[3, 4] = 0.0f;
            colorMatrix[4, 0] = pendingValue; // brightness goes here
            colorMatrix[4, 1] = pendingValue; // brightness
            colorMatrix[4, 2] = pendingValue; // even more brightness
            colorMatrix[4, 3] = 0.0f;
            colorMatrix[4, 4] = 1.0f;

            // saturation
            float s1 = pendingSaturation * 2;
            float sr = (1.0f - s) * 0.3086f;
            float sg = (1.0f - s) * 0.6094f;
            float sb = (1.0f - s) * 0.0820f;

            colorMatrix[0, 0] *= sr + s1;
            colorMatrix[0, 1] *= sr;
            colorMatrix[0, 2] *= sr;
            colorMatrix[1, 0] *= sg;
            colorMatrix[1, 1] *= sg + s1;
            colorMatrix[1, 2] *= sg;
            colorMatrix[2, 0] *= sb;
            colorMatrix[2, 1] *= sb;
            colorMatrix[2, 2] *= sb + s1;

            // brightness

            imageAttributes.SetColorMatrix(colorMatrix);

            gfx.DrawImage(CurrentImage,
                new Rectangle(0, 0, CurrentImage.Width, CurrentImage.Height),
                0, 0, CurrentImage.Width, CurrentImage.Height,
                GraphicsUnit.Pixel,
                imageAttributes);
            ResetPreview();
        }

        private void HSV_HueChanged(object sender, EventArgs e)
        {
            HSVEditForm.HSVHueChangeEvent e1 = e as HSVEditForm.HSVHueChangeEvent;
            pendingHue = e1.currentValue;

            AdjustForHSV();
        }
        private void HSV_SaturationChanged(object sneder, EventArgs e)
        {
            HSVEditForm.HSVSaturationChangeEvent e1 = e as HSVEditForm.HSVSaturationChangeEvent;
            pendingSaturation = e1.currentValue;

            AdjustForHSV();
        }
        private void HSV_ValueChanged(object sender, EventArgs e)
        {
            HSVEditForm.HSVValueChangeEvent e1 = e as HSVEditForm.HSVValueChangeEvent;
            pendingValue = (e1.currentValue * 2.0f) - 1.0f;

            AdjustForHSV();
        }
        private void HSV_OkClicked(object sender, EventArgs e)
        {
            // OK doesn't actually do anything here!
            // only thing we do is say "we need to save"
            needToSave = true;
        }
        private void HSV_CancelClicked(object sender, EventArgs e)
        {
            // reset current image to unmodified
            CurrentImage = unmodifiedImage;
        }

        private void filter_hsv_Click(object sender, EventArgs e)
        {
            pendingHue = pendingSaturation = pendingValue = 0.5f;
            unmodifiedImage = new Bitmap(CurrentImage);
            CurrentImage = new Bitmap(CurrentImage);
            gfx = Graphics.FromImage(CurrentImage);
            HSVEditForm form = new HSVEditForm(pendingHue, pendingSaturation, pendingValue);
            form.HueChanged += HSV_HueChanged;
            form.SaturationChanged += HSV_SaturationChanged;
            form.ValueChanged += HSV_ValueChanged;
            form.OKClicked += HSV_OkClicked;
            form.CancelClicked += HSV_CancelClicked;
            form.ShowDialog();
        }
        string AddSuffix(string filename, string suffix)
        {
            string fDir = Path.GetDirectoryName(filename);
            string fName = Path.GetFileNameWithoutExtension(filename);
            string fExt = Path.GetExtension(filename);
            return Path.Combine(fDir, String.Concat(fName, suffix, fExt));
        }

        /**
         * IMPORT SINGLE FRAME - MUST MATCH FRAME SIZE!
         */
        private void ImportFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // try and load the image. it should match the frame size.
                Image newImg = Image.FromFile(dlg.FileName);
                if (newImg.Width != CurrentImage.Width / CurrentFrameCount ||
                    newImg.Height != CurrentImage.Height)
                {
                    MessageBox.Show(string.Format("This image is not the right size. Please use a {0}x{1} image.", CurrentImage.Width / CurrentFrameCount, CurrentImage.Height));
                    return;
                }

                // blit!
                Bitmap bmp = new Bitmap(CurrentImage);
                Graphics g = Graphics.FromImage(bmp);
                g.CompositingMode = CompositingMode.SourceOver;
                g.DrawImage(newImg, new Point((int)CurrentlyViewedFrame * (int)(CurrentImage.Width / CurrentFrameCount), 0));
                CurrentImage = bmp;
            }
        }

        /**
         * IMPORT MULTIPLE IMAGES - ALL MUST BE THE SAME SIZE!
         */
        private void CombineFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (needToSave)
            {
                if (MessageBox.Show("Continue without saving?", "Notification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileNames = dlg.FileNames;
                Image[] files = new Image[fileNames.Length];
                for (var i = 0; i < fileNames.Length; i++)
                {
                    files[i] = Image.FromFile(fileNames[i]);
                    if (files[i].Width != files[0].Width ||
                        files[i].Height != files[0].Height)
                    {
                        MessageBox.Show("All selected images must have the same size.");
                        return;
                    }
                }

                // make a BIG BOY image
                Bitmap bmp = new Bitmap(files[0].Width * files.Length, files[0].Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CompositingMode = CompositingMode.SourceOver;
                for (var i = 0; i < files.Length; i++)
                {
                    g.DrawImage(files[i], new Point(files[0].Width * i, 0));
                }

                CurrentImage = bmp;
                CurrentFrameCount = (uint)files.Length;
            }
        }

        /**
         * MASS CONVERT IMAGES TO .SPRITE
         */
        private void MassTranslateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // grab each file, convert to .sprite
                string[] files = dlg.FileNames;
                for (var i = 0; i < files.Length; i++)
                {
                    Image img = Image.FromFile(files[i]);
                    string newPath = Path.ChangeExtension(files[i], ".sprite");
                    SaveAsSprite(img, 1, newPath);
                    string lowend = ".lowend";
                    var filepath = newPath;
                    var filepathsplit = String.Format("{0}{1}{2}",
                    Path.GetFileNameWithoutExtension(filepath), lowend, Path.GetExtension(filepath));
                    var filepathlow = Path.Combine(Path.GetDirectoryName(filepath), filepathsplit);
                    SaveAsSprite(img, 1, newPath);
                    SaveAsSprite(ResizeImage(img, img.Width / 2, img.Height / 2), CurrentFrameCount, filepathlow);
                }

                MessageBox.Show("Converted " + files.Length + " images!");
            }
        }
        /**
         * EXPORT JUST THIS FRAME
         */
        private void ExportFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!HasFrames || CurrentlyViewedFrame >= CurrentFrameCount)
            {
                ExportToolStripMenuItem_Click(sender, e);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (CurrentImage == null)
            {
                MessageBox.Show("You need to open or import an image first.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                int widthPerFrame = (int)(CurrentImage.Width / CurrentFrameCount);
                Bitmap subbmp = new Bitmap(CurrentImage).Clone(new Rectangle((int)CurrentlyViewedFrame * widthPerFrame, 0, widthPerFrame, CurrentImage.Height),
                    CurrentImage.PixelFormat);
                subbmp.Save(fileName);
            }
        }
        /**
         * EXPORT ALL FRAMES AS SEPARATE IMAGES
         */
        private void ExportFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HasFrames || CurrentlyViewedFrame >= CurrentFrameCount)
            {
                ExportToolStripMenuItem_Click(sender, e);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = _filterExtensions,
                DefaultExt = _defaultExtensions
            };

            if (CurrentImage == null)
            {
                MessageBox.Show("You need to open or import an image first.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                int widthPerFrame = (int)(CurrentImage.Width / CurrentFrameCount);

                for(var i = 0; i < CurrentFrameCount; i++)
                {
                    var thisFrameFileName = AddSuffix(fileName, "_" + i);
                    Bitmap subbmp = new Bitmap(CurrentImage).Clone(new Rectangle((int)i * widthPerFrame, 0, widthPerFrame, CurrentImage.Height),
                        CurrentImage.PixelFormat);
                    subbmp.Save(thisFrameFileName);
                }
            }
        }

        private void OnFrameComboBoxChanged(object sender, EventArgs e)
        {
            CurrentlyViewedFrame = UInt32.Parse(frameSelectionComboBox.Text);
        }

        private void OnFrameCountChanged(object sneder, EventArgs e)
        {
            CurrentFrameCount = UInt32.Parse(numFramesTextBox.Text);
        }
    }
}