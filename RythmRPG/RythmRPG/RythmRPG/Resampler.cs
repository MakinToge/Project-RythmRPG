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
        static public void Resampling(string inFilePath, string wavDirectory, int outRate)
        {
            var outFile = wavDirectory + Path.GetFileNameWithoutExtension(inFilePath) + ".wav";

            using (var reader = new Mp3FileReader(inFilePath))
            {
                var outFormat = new WaveFormat(outRate, reader.WaveFormat.Channels);
                using (var resampler = new MediaFoundationResampler(reader, outFormat))
                {
                    resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(outFile, resampler);
                }
            }
        }
    }
}
