using NAudio.Wave;
using RythmRPG.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Resampler
    {

        /// <summary>
        /// The resampling sample rate
        /// </summary>
        public const int RESAMPLING_SAMPLE_RATE = 44032;//SampleRate
        /// <summary>
        /// Resamplings the specified in file path.
        /// </summary>
        /// <param name="inFilePath">The in file path.</param>
        /// <param name="wavDirectory">The wav directory.</param>
        /// <param name="outRate">The out rate.</param>
        /// <param name="outFilePath">The out file path.</param>
        static public void Resampling(string inFilePath, string wavDirectory, int outRate, out string outFilePath)
        {
            if (MusicPlaying.output != null)
            {
                MusicPlaying.output.Stop();
                MusicPlaying.output.Dispose();
                MusicPlaying.output = null;
            }
            if (MusicPlaying.stream != null)
            {
                MusicPlaying.stream.Dispose();
                MusicPlaying.stream = null;
            }


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
                    if (!File.Exists(outFilePath))
                    {
                        WaveFileWriter.CreateWaveFile(outFilePath, resampler);
                    }
                }
            }
        }
    }
}
