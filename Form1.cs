using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace RTSPViewer
{
    public partial class Form1 : Form
    {
        string rtspUrl = "rtsp://210.99.70.120:1935/live/cctv001.stream";
        VideoCapture video = new VideoCapture();

        public Form1()
        {
            InitializeComponent();
            picVideo.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            video.Open(rtspUrl);

            using (Mat image = new Mat())
            {
                while (true)
                {
                    if (!video.Read(image))
                    {
                        Cv2.WaitKey();
                    }
                    if (!image.Empty())
                    {
                        Bitmap bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
                        picVideo.Image = bmp;
                    }
                    if (Cv2.WaitKey(1) >= 0)
                        break;
                }
            }
            video = null;
        }
    }
}
