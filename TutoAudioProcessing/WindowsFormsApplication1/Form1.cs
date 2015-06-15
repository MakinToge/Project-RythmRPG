using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //easy conversion with NAudio
        private void button1_Click(object sender, EventArgs e)
        {
            //open mp3 file
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 File (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK) return;

            //set a place to save the wav file, don't forget to set a name to the new wav file
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "WAV File (*.wav)|*.wav;";
            if (save.ShowDialog() != DialogResult.OK) return;

            using (Mp3FileReader mp3 = new Mp3FileReader(open.FileName))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(save.FileName, pcm);
                }
            }
        }
    }
}
