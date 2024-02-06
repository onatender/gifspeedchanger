using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string inputPath = "gif.gif";

            string outputFolder = "output_folder";

            ConvertGifToImages(inputPath, outputFolder);

            timer1.Start();
            label1.Text = "FPS:"+60;
        }
        static int framecount = 0;
        static void ConvertGifToImages(string inputPath, string outputFolder)
        {
            using (Image gif = Image.FromFile(inputPath))
            {
                Directory.CreateDirectory(outputFolder);
                framecount = gif.GetFrameCount(FrameDimension.Time);
                for (int frameNumber = 0; frameNumber < gif.GetFrameCount(FrameDimension.Time); frameNumber++)
                {
                    gif.SelectActiveFrame(FrameDimension.Time, frameNumber);

                    Bitmap frameBitmap = new Bitmap(gif);

                    string frameFilename = Path.Combine(outputFolder, $"frame_{frameNumber + 1:000}.png");
                    frameBitmap.Save(frameFilename, ImageFormat.Png);

                    frameBitmap.Dispose();
                }
            }
        }
        int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            pictureBox1.ImageLocation = $"output_folder\\frame_{i.ToString("000")}.png";
            i++;
            if (i>framecount) i = 1;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        int FpsToInterval(int fps)
        {
            return 1000/fps;
        }
        int fps = 60;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue== 38)
            {
                if (fps==0) timer1.Start();
                fps++;
                timer1.Interval = FpsToInterval(fps);
            }
            else if (e.KeyValue==40)
            {
                if (fps >= 1)
                {
                    fps--;
                    if (fps==0) timer1.Stop();
                    else timer1.Interval = FpsToInterval(fps);
                }
            }
            label1.Text = "FPS:"+fps;
        }
    }
}
