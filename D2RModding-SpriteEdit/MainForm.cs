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
        private uint _currentFrameCount;
        private uint currentFrameCount
        {
            get
            {
                return _currentFrameCount;
            }
            set
            {
                if(value > 0 && !hasFrames)
                {
                    hasFrames = true;
                }
                _currentFrameCount = value;
                ResetPreview();
            }
        }

        private bool _hasFrames;
        private bool hasFrames
        {
            get
            {
                return _hasFrames;
            }
            set
            {
                _hasFrames = value;
                ResetPreview();
            }
        }

        private uint _currentlyViewedFrame;
        private uint currentlyViewedFrame
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
        private Point currentPan
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
            if(currentImage == null)
            {
                return;
            }
            int sourceWidth = currentImage.Width;
            int sourceHeight = currentImage.Height;
            int newWidth, newHeight, frameWidth = 0;
            if (hasFrames && currentFrameCount > 0)
            {
                frameWidth = (int)(sourceWidth / (float)currentFrameCount);
                newWidth = (int)(currentZoom * frameWidth);
            }
            else
            {
                newWidth = (int)(currentZoom * sourceWidth);
            }
            newHeight = (int)(currentZoom * sourceHeight);


            Bitmap temp = new Bitmap(newWidth, newHeight);
            Graphics bmGraphics = Graphics.FromImage(temp);
            bmGraphics.Clear(imagePreview.BackColor);
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if(hasFrames && currentFrameCount > 0)
            {
                bmGraphics.DrawImage(currentImage,
                    new Rectangle((int)(currentPan.X * currentZoom), (int)(currentPan.Y * currentZoom), newWidth, newHeight),
                    new Rectangle(0, 0, frameWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            else
            {
                bmGraphics.DrawImage(currentImage,
                    new Rectangle((int)(currentPan.X * currentZoom), (int)(currentPan.Y * currentZoom), newWidth, newHeight),
                    new Rectangle(0, 0, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            
            bmGraphics.Dispose();
            imagePreview.Image = temp;
        }

        private float _currentZoom = 1.0f;
        private float currentZoom
        {
            get
            {
                return _currentZoom;
            }
            set
            {
                zoomAmountLabel.Text = string.Format("{0}%", currentZoom * 100.0f);
                _currentZoom = value;
                ResetPreview();
            }
        }

        private Image _currentImage;
        private Image currentImage
        {
            get
            {
                return _currentImage;
            }
            set
            {
                _currentImage = value;
                ResetPreview();
            }
        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*";
            dlg.DefaultExt = ".sprite";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                // open up the image
                var fileName = dlg.FileName;
                var bytes = File.ReadAllBytes(fileName);
                int x, y;
                var version = BitConverter.ToUInt16(bytes, 4);
                var width = BitConverter.ToInt32(bytes, 8);
                var height = BitConverter.ToInt32(bytes, 0xC);
                var bmp = new Bitmap(width, height);
                currentFrameCount = BitConverter.ToUInt32(bytes, 0x14);

                if(version == 31)
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
                else if(version == 61)
                {   // DXT
                    var tempBytes = new byte[width * height * 4];
                    Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                    for(y = 0; y < height; y++)
                    {
                        for(x = 0; x < width; x++)
                        {
                            var baseVal = (y * width) + (x * 4);
                            bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                        }
                    }
                }

                currentImage = bmp;
                toolbarText.Text = string.Format("{0}x{1}", width, height);
                Text = "SpriteEdit - " + fileName;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*";
            dlg.DefaultExt = ".sprite";

            if(currentImage == null)
            {
                MessageBox.Show("You haven't loaded an image yet.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var f = File.Open(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);

                f.Write(new byte[] { (byte)'S', (byte)'P', (byte)'a', (byte)'1' }, 0, 4);
                f.Write(BitConverter.GetBytes((ushort)31), 0, 2);
                f.Write(BitConverter.GetBytes((ushort)currentImage.Width / currentFrameCount), 0, 2);
                f.Write(BitConverter.GetBytes((Int32)currentImage.Width), 0, 4);
                f.Write(BitConverter.GetBytes((Int32)currentImage.Height), 0, 4);
                f.Seek(0x14, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes((UInt32)currentFrameCount), 0, 4);
                int x, y;
                Bitmap bmp = new Bitmap(currentImage);
                f.Seek(0x28, SeekOrigin.Begin);
                for(x = 0; x < currentImage.Height; x++)
                {
                    for(y = 0; y < currentImage.Width; y++)
                    {
                        var pos = 0x28 + x * 4 * currentImage.Width + y * 4;
                        var pixel = bmp.GetPixel(y, x);
                        f.Write(new byte[] { pixel.R, pixel.G, pixel.B, pixel.A }, 0, 4);
                    }
                }
                f.Close();
            }
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
                + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                + "All Files (*.*)|*.*";
            dlg.DefaultExt = "*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                currentImage = Image.FromFile(fileName);
                toolbarText.Text = string.Format("{0}x{1}", currentImage.Width, currentImage.Height);
                Text = "SpriteEdit - " + dlg.FileName;
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
                + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                + "All Files (*.*)|*.*";
            dlg.DefaultExt = "*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";

            if(currentImage == null)
            {
                MessageBox.Show("You need to open or import an image first.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                currentImage.Save(fileName);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void massExportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void d2RModdingDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/gvsJBRd4KZ");
        }
        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            var value = tb.Value;

            currentZoom = value / 100.0f;
        }

        private bool isPanning = false;
        private Point mouseDownLocation;

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            isPanning = true;
            mouseDownLocation = e.Location;
        }
        private void onMouseUp(object sender, MouseEventArgs e)
        {
            isPanning = false;
        }
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if(isPanning)
            {
                currentPan = new Point(e.Location.X - mouseDownLocation.X, e.Location.Y - mouseDownLocation.Y);
            }
        }
        private void onMouseLeave(object sender, EventArgs e)
        {
            isPanning = false;
        }
        private void onResetPan(object sender, EventArgs e)
        {
            currentPan = new Point(0, 0);
        }
        private void rotate_180_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(currentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            currentImage = temp;
        }
        private void rotate_90_clockwise_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(currentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            currentImage = temp;
        }
        private void rotate_90_counterclockwise_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(currentImage);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            currentImage = temp;
        }
        private void flip_horizontal_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(currentImage);
            temp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            currentImage = temp;
        }
        private void flip_vertical_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(currentImage);
            temp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            currentImage = temp;
        }

        private float pendingHue;
        private float pendingSaturation;
        private float pendingValue;
        private Image unmodifiedImage;
        private ImageAttributes imageAttributes = new ImageAttributes();
        private ColorMatrix colorMatrix = new ColorMatrix();
        private Graphics gfx;

        // Luminance vector for linear RGB
        const float rwgt = 0.3086f;
        const float gwgt = 0.6094f;
        const float bwgt = 0.0820f;

        private void AdjustForHSV()
        {
            Bitmap temp = new Bitmap(unmodifiedImage);
            currentImage.Dispose();
            currentImage = new Bitmap(temp);
            gfx.Dispose();
            gfx = Graphics.FromImage(currentImage);

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

            gfx.DrawImage(currentImage,
                new Rectangle(0, 0, currentImage.Width, currentImage.Height),
                0, 0, currentImage.Width, currentImage.Height,
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
        }
        private void HSV_CancelClicked(object sender, EventArgs e)
        {
            // reset current image to unmodified
            currentImage = unmodifiedImage;
        }

        private void filter_hsv_Click(object sender, EventArgs e)
        {
            pendingHue = pendingSaturation = pendingValue = 0.5f;
            unmodifiedImage = new Bitmap(currentImage);
            currentImage = new Bitmap(currentImage);
            gfx = Graphics.FromImage(currentImage);
            HSVEditForm form = new HSVEditForm(pendingHue, pendingSaturation, pendingValue);
            form.HueChanged += HSV_HueChanged;
            form.SaturationChanged += HSV_SaturationChanged;
            form.ValueChanged += HSV_ValueChanged;
            form.OKClicked += HSV_OkClicked;
            form.CancelClicked += HSV_CancelClicked;
            form.ShowDialog();
        }
    }
}
