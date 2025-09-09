using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Menu
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap original;
        string ruta;  

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();

            od.Filter = "Imagenes|*.jpg;*.png;*.bmp";
            od.AddExtension = true;
            DialogResult res = od.ShowDialog();


            if (res == DialogResult.OK)
            {
                ruta = od.FileName;

                using (var temp = new Bitmap(od.FileName))
                {
                    original = new Bitmap(temp);
                }

                bmp = (Bitmap)original.Clone();

                this.Invalidate();

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           

            if (bmp != null)
            {

                int x = (this.ClientSize.Width - bmp.Width) / 2;
                int y = (this.ClientSize.Height - bmp.Height) / 2;

                e.Graphics.DrawImage(bmp, x, y, bmp.Width, bmp.Height);
            }
        }

        private void grisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {

                        Color c = bmp.GetPixel(x, y);
                        int promedio = (c.R + c.G + c.B) / 3;
                        Color nc = Color.FromArgb(promedio, promedio, promedio);

                        bmp.SetPixel(x, y, nc);
                    }
                }

                this.Invalidate();
            }
        }

        private void negativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {

                        Color c = bmp.GetPixel(x, y);
                        Color nc = Color.FromArgb(255-c.R,255-c.G,255-c.B );

                        bmp.SetPixel(x, y, nc);
                    }
                }

                this.Invalidate();
            }
        }

        private void blancoYNegroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {

                        Color c = bmp.GetPixel(x, y);
                        int promedio = (c.R + c.G + c.B) / 3;
                        Color nc = promedio >= 128 ? Color.FromArgb(255, 255, 255)
                                                   : Color.FromArgb(0, 0, 0);

                        bmp.SetPixel(x, y, nc);
                    }
                }

                this.Invalidate();
            }
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (bmp != null)
            {

                bmp = (Bitmap)original.Clone();
                this.Invalidate();
            }
           
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {

                        Color c = bmp.GetPixel(x, y);

                        int tr = (int)(0.393 * c.R + 0.769 * c.G + 0.189 * c.B);
                        int tg = (int)(0.349 * c.R + 0.686 * c.G + 0.168 * c.B);
                        int tb = (int)(0.272 * c.R + 0.534 * c.G + 0.131 * c.B);

                        tr = Math.Min(255, tr);
                        tg = Math.Min(255, tg);
                        tb = Math.Min(255, tb);

                        Color nc = Color.FromArgb(tr, tg, tb);

                        bmp.SetPixel(x, y, nc);
                    }
                }

                this.Invalidate();
            }

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null && !string.IsNullOrEmpty(ruta))
            {
                bmp.Save(ruta); // sobrescribe la imagen
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "JPEG|*.jpg|PNG|*.png|Bitmap|*.bmp";
                sd.AddExtension = true;
                sd.FileName = "imagen"; 

                if (sd.ShowDialog() == DialogResult.OK)
                {            
                    bmp.Save(sd.FileName);
                    ruta = sd.FileName;
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}
