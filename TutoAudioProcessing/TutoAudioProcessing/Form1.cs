using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutoAudioProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private NAudio.Wave.WaveFileReader wave = null;  //to read a WAV File directly
        private NAudio.Wave.BlockAlignReductionStream stream = null; //to read MP3 properly


        private NAudio.Wave.DirectSoundOut output = null;

        //button1 = openFileButton
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Wave File (*.wav)|*.wav;";
            //open.Filter = "MP3 File (*.mp3)|*.mp3;";
            open.Filter = "Audio File (*.mp3;*.wav)|*.mp3;*.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;

            DisposeWave();

            //wave = new NAudio.Wave.WaveFileReader(open.FileName);
            if (open.FileName.EndsWith(".mp3"))
            {
                NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(open.FileName)); //contain the uncompress audio data
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else if (open.FileName.EndsWith(".wav"))
            {
                NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(open.FileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else throw new InvalidOperationException("Not a correct audio file type");

            output = new NAudio.Wave.DirectSoundOut();
            //output.Init(new NAudio.Wave.WaveChannel32(wave));
            output.Init(stream);
            output.Play();

            pauseButton.Enabled = true;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if(output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
                else if (output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }

        private void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            //if (wave != null)
            if (stream != null)
            {
                /*wave.Dispose();
                wave = null;*/
                stream.Dispose();
                stream = null;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeWave();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
