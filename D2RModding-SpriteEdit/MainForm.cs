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

namespace D2RModding_SpriteEdit
{
    public partial class MainForm : Form
    {
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
                var width = BitConverter.ToInt32(bytes, 8);
                var height = BitConverter.ToInt32(bytes, 0xC);
                var bmp = new Bitmap(width, height);
                for(x = 0; x < height; x++)
                {
                    for(y = 0; y < width; y++)
                    {
                        var baseVal = 0x28 + x * 4 * width + y * 4;
                        bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                    }
                }

                imagePreview.Image = bmp;
                toolbarText.Text = string.Format("{0}x{1}", width, height);
                Text = "SpriteEdit - " + fileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Diablo II Resurrected Sprites (*.sprite)|*.sprite|All Files (*.*)|*.*";
            dlg.DefaultExt = ".sprite";

            if(imagePreview.Image == null)
            {
                MessageBox.Show("You haven't loaded an image yet.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                var f = File.Open(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);

                f.Seek(8, SeekOrigin.Current);
                f.Write(BitConverter.GetBytes((Int32)imagePreview.Image.Width), 0, 4);
                f.Write(BitConverter.GetBytes((Int32)imagePreview.Image.Height), 0, 4);
                int x, y;
                Bitmap bmp = new Bitmap(imagePreview.Image);
                f.Seek(0x28, SeekOrigin.Begin);
                for(x = 0; x < imagePreview.Image.Height; x++)
                {
                    for(y = 0; y < imagePreview.Image.Width; y++)
                    {
                        var pos = 0x28 + x * 4 * imagePreview.Image.Width + y * 4;
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
                imagePreview.Image = Image.FromFile(fileName);
                toolbarText.Text = string.Format("{0}x{1}", imagePreview.Image.Width, imagePreview.Image.Height);
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

            if(imagePreview.Image == null)
            {
                MessageBox.Show("You need to open or import an image first.");
                return;
            }

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                imagePreview.Image.Save(fileName);
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
    }
}
