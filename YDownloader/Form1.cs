using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
//dodatkowe biblioteki
using YoutubeExtractor;
using System.IO;
using System.Net;

namespace YDownloader
{
    public partial class Form1 : Form
    {
        public static string miejsce = "";
        public Form1()
        {
            InitializeComponent();
            
        }

        private void mp3_Click(object sender, EventArgs e)
        {
            label1.Text = "Trwa pobieranie...";
            // Link do youtube
            string link = textBox1.Text;
            //string miejsce = @"C:\Users\aleksander\Desktop\";  //praca
            //string miejsce = @"C:\Users\quaker\Desktop\"; //dom
            if (textBox1.Text.StartsWith("https://www.youtube.com/watch?v=") || textBox1.Text.StartsWith("https://www.youtube.com/watch?v="))
            {

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
            VideoInfo video = videoInfos
                .OrderByDescending(info => info.Resolution)
                .Where(info => info.AudioBitrate == 128)
    .First(info => info.VideoType == VideoType.Mp4);
            HttpWebRequest httpRequest = (HttpWebRequest)
    WebRequest.Create(video.DownloadUrl);
            httpRequest.Method = WebRequestMethods.Http.Get;
            HttpWebResponse httpResponse
        = (HttpWebResponse)httpRequest.GetResponse();
            Stream httpResponseStream = httpResponse.GetResponseStream();
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            // Read from response and write to file
            FileStream fileStream = File.Create(miejsce + video.Title + ".mp3");
            while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
            {
                fileStream.Write(buffer, 0, bytesRead);

            } // end while
        }
             else{
                 label1.Visible = false;
                MessageBox.Show("Nieprawidlowy link");
        }
            label1.Visible = false;
            label2.Text = "Pobrano";
        }
       

        private void mp4_Click(object sender, EventArgs e)
        {
            label1.Text = "Pobieranie...";
            // Link do youtube
            string link = textBox1.Text;

            //string miejsce = @"C:\Users\aleksander\Desktop\";  //praca
            //string miejsce = @"C:\Users\quaker\Desktop\"; //dom
            if (textBox1.Text.StartsWith("https://www.youtube.com/watch?v=") || textBox1.Text.StartsWith("https://www.youtube.com/watch?v="))
            {
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
                VideoInfo video = videoInfos
        .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 720 || info.Resolution == 480 || info.Resolution == 360);
                HttpWebRequest httpRequest = (HttpWebRequest)
                WebRequest.Create(video.DownloadUrl);
                httpRequest.Method = WebRequestMethods.Http.Get;
                HttpWebResponse httpResponse
            = (HttpWebResponse)httpRequest.GetResponse();
                Stream httpResponseStream = httpResponse.GetResponseStream();
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;
                // Read from response and write to file
                FileStream fileStream = File.Create(miejsce + video.Title + ".mp4");
                while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);

                } // end while
            }
            else
            {
                label1.Visible = false;
                MessageBox.Show("Nieprawidlowy link");
            }
            label1.Visible = false;
            label2.Text = "Pobrano";
        }

        private void Folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                miejsce = folderBrowserDialog1.SelectedPath;
                
                mp3.Enabled = true;
                mp4.Enabled = true;
            }
        }
    }
}
