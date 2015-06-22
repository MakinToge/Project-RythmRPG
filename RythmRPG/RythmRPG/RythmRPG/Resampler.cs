using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Resampler
    {

        public const int RESAMPLING_SAMPLE_RATE = 44032;//SampleRate
        static public void Resampling(string inFilePath, string wavDirectory, int outRate, out string outFilePath)
        {
            if (!Directory.Exists(wavDirectory))
            {
                Directory.CreateDirectory(wavDirectory);
            }
            outFilePath = wavDirectory + Path.GetFileNameWithoutExtension(inFilePath) + ".wav";

            using (var reader = new Mp3FileReader(inFilePath))
            {
                var outFormat = new WaveFormat(outRate, reader.WaveFormat.Channels);
                using (var resampler = new MediaFoundationResampler(reader, outFormat))
                {
                    resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(outFilePath, resampler);
                }
            }
        }
    }
}
