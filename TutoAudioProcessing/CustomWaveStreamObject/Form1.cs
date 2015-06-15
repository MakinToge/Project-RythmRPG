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

namespace CustomWaveStreamObject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DirectSoundOut output = null;
        private BlockAlignReductionStream stream = null;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startTone_Click(object sender, EventArgs e)
        {
            WaveTone tone = new WaveTone(3000, 0.1); // 1000 Hz and low amplitude (between 1 and -1)
            stream = new BlockAlignReductionStream(tone);

            output = new DirectSoundOut();
            output.Init(stream);
            output.Play();
        }

        private void stopTone_Click(object sender, EventArgs e)
        {
            if (output != null) output.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (output != null)
            {
                output.Dispose();
                output = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        public class WaveTone : WaveStream
        {
            private double frequency;
            private double amplitude;
            private double time;

            public WaveTone(double f, double a)
            {
                this.time = 0;
                this.frequency = f;
                this.amplitude = a;
            }

            //must override
            public override long Position
            {
                get;
                set;
            }

            //must override
            public override long Length
            {
                get { return long.MaxValue; }
            }

            //must override
            public override WaveFormat WaveFormat
            {
                get { return new WaveFormat(44100, 16, 1); } // (samples per sec , bits per sample, number of channels)
            }

            //must override
            public override int Read(byte[] buffer, int offset, int count)
            {
                int samples = count / 2; // divide by 2 (bytes) because we have 16 bits per sec
                for (int i = 0; i < samples; i++)
                {
                    double sine = amplitude * Math.Sin(Math.PI * 2 * frequency * time);
                    time += 1.0 / 44100;
                    short truncated = (short)Math.Round(sine * (Math.Pow(2, 15) - 1)); // - 1 because signed value, so - 1 bit
                    buffer[i * 2] = (byte)(truncated & 0x00ff); //first byte of the short value
                    buffer[i * 2 + 1] = (byte)((truncated & 0xff00) >> 8); //second byte of the short value
                }

                return count; //number of bytes to read
            }
        }
    }
}
