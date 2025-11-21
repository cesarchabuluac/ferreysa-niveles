using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vales
{
    public partial class FormImagen : Form
    {

        public int art_id;
        public Image imagen;

        public FormImagen()
        {
            InitializeComponent();
        }

        private void FormImagen_Load(object sender, EventArgs e)
        {

            // Assuming you have a PictureBox control named pbx_imagen in your form
            // And assuming targetSize is the desired size to fit the image within

            // Get the original image from the PictureBox
            Image originalImage = imagen;

            // Calculate the target dimensions while preserving the aspect ratio
            int targetWidth = 420;
            int targetHeight = 420;

            float aspectRatio = (float)originalImage.Width / originalImage.Height;

            if (originalImage.Width > originalImage.Height)
            {
                targetHeight = (int)(targetWidth / aspectRatio);
            }
            else if (originalImage.Width < originalImage.Height)
            {
                targetWidth = (int)(targetHeight * aspectRatio);
            }

            // Create a new bitmap with the target size
            Bitmap resizedImage = new Bitmap(targetWidth, targetHeight);

            // Create a Graphics object from the new bitmap
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                // Set the interpolation mode to achieve a smoother result
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Draw the original image onto the new bitmap with the target size
                graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
            }

            // Set the resized image as the source for the PictureBox control
            pbx_imagen.Image = resizedImage;

            Size imageSize = pbx_imagen.Image.Size;
            int border = 15;
            Size formSize = new Size(imageSize.Width + 2 * border, imageSize.Height + 2 * border);


            this.ClientSize = formSize;


        }
    }
}
