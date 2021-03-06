﻿using Lomont;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beatDetectionAlgo
{
    class Program
    {
        public static bool log = false;
        public const int SubbandsNumber = 16;
        public const int InstantWidth = 1024;//must be a power of 2 and a multiple of SubBandNumber
        public const int LocalWidth = 44032;//must be a multiple of InstantWidth(best result if ~1sec of music (== SampleRate))
        public const int LocalInstantNumber = LocalWidth / InstantWidth;
        public static int LocalLowerBound = getLocalLowerBound();
        public const string MusicDirectory = "musicDir";//Resampling output directory
        public const int outRate = 44032;//SampleRate for resampling

        static void Main(string[] args)
        {
            string file = "HarrisLilliburleroShort";//HarrisLilliburleroShort
            Resampling(file + ".mp3");
            List<Beat> beats = DetectBeat(file + ".wav");
            foreach (Beat beat in beats)
            {
                Console.WriteLine(beat);
            }
            Console.Read();//HarrisLilliburleroShort.wav
        }
        static public double[] ComputeSubbandsLocalEnergyAverage(double[][] instantSubbandsEnergyArray, int historyIndex)//<Ei>, LocalLowBound <= historyIndex < subbandsInstantEnergyArray.Length - LocalInstantNumber/2
        {
            double[] subbandsLocalEnergyAverage = new double[SubbandsNumber];
            for (int i = 0; i < SubbandsNumber; i++)
            {
                double subbandLocalEnergy = 0;
                for (int k = LocalLowerBound; k < LocalInstantNumber / 2; k++)
                {
                    subbandLocalEnergy += instantSubbandsEnergyArray[historyIndex + k][i];
                }
                subbandsLocalEnergyAverage[i] = subbandLocalEnergy / (double)LocalInstantNumber;
            }
            return subbandsLocalEnergyAverage;
        }
        static public double[] ComputeSubbandsInstantEnergy(Complex[] instantBuffer)//(Es)
        {

            double[] InstantEnergybySubband = new double[SubbandsNumber];
            double[] Buffer = new double[InstantWidth];

            for (int i = 0; i < InstantWidth; i++)//(B)
            {
                Buffer[i] = Math.Pow(instantBuffer[i].X, 2) + Math.Pow(instantBuffer[i].Y, 2);//square module
            }

            for (int i = 0; i < SubbandsNumber; i++)//(Es)
            {
                double SubbandSum = 0;
                for (int k = 0; k < SubbandsNumber; k++)
                {
                    SubbandSum += Buffer[k + i * SubbandsNumber];
                }
                InstantEnergybySubband[i] = (double)SubbandsNumber / (double)InstantWidth * SubbandSum;
            }

            return InstantEnergybySubband;
        }
        static public double[] ComputeSubbandsInstantEnergy(double[] instantBuffer)//(Es)
        {

            double[] InstantEnergybySubband = new double[SubbandsNumber];
            double[] Buffer = new double[InstantWidth];

            for (int i = 0; i < InstantWidth; i += 2)//(B)
            {
                Buffer[i / 2] = Math.Pow(instantBuffer[i], 2) + Math.Pow(instantBuffer[i + 1], 2);//square module
            }

            for (int i = 0; i < SubbandsNumber; i++)//(Es)
            {
                double SubbandSum = 0;
                for (int k = 0; k < InstantWidth / SubbandsNumber; k++)
                {
                    SubbandSum += Buffer[k + i * SubbandsNumber];
                }
                InstantEnergybySubband[i] = (double)SubbandsNumber / InstantWidth * SubbandSum;
            }

            return InstantEnergybySubband;
        }

        static public double[][] ComputeSubbandsInstantEnergyArray(double[] left, double[] right)
        {
            LomontFFT fftTool = new LomontFFT();
            int totalInstant = left.Length / InstantWidth;
            double[][] complexData = new double[totalInstant][];//[InstantWidth * 2];
            double[][] subbandsInstantEnergyArray = new double[totalInstant][];//[SubbandsNumber]

            for (int i = 0; i < totalInstant; i++)
            {
                Console.WriteLine(string.Format("instant {0} out of {1}", i, totalInstant));
                complexData[i] = new double[InstantWidth * 2];
                for (int k = 0; k < InstantWidth * 2; k += 2)
                {

                    complexData[i][k] = (float)left[i * InstantWidth + k / 2];
                    complexData[i][k + 1] = (float)right[i * InstantWidth + k / 2];
                }
                //FastFourierTransform.FFT(true, InstantWidth, complexData[i]);//complexData is an input and ouput parameter
                fftTool.FFT(complexData[i], true);
                subbandsInstantEnergyArray[i] = ComputeSubbandsInstantEnergy(complexData[i]);
            }
            return subbandsInstantEnergyArray;
            /*
            for (int i = 0; i < totalInstant; i++)
            {
                Console.WriteLine(string.Format("instant {0} out of {1}",i,totalInstant));
                complexData[i] = new Complex[InstantWidth];
                for (int k = 0; k < InstantWidth; k++)
                {

                    complexData[i][k].X = (float)left[i * InstantWidth + k];
                    complexData[i][k].Y = (float)right[i * InstantWidth + k];
                }
                FastFourierTransform.FFT(true, InstantWidth, complexData[i]);//complexData is an input and ouput parameter
                subbandsInstantEnergyArray[i] = ComputeSubbandsInstantEnergy(complexData[i]);
            }
             */
        }

        static public List<Beat> DetectBeat(string wavFilePath)
        {
            List<Beat> beats = new List<Beat>();

            double[] left;
            double[] right;
            openWav(string.Format("{0}/{1}", MusicDirectory, wavFilePath), out left, out right);

            if (right == null)//if mono
            {
                right = left;
            }

            int totalInstant = left.Length / InstantWidth;
            if (totalInstant < LocalWidth) ;//music way too short !!

            double[][] subbandsInstantEnergyArray = ComputeSubbandsInstantEnergyArray(left, right);//[totInst][subb]
            double[][] subbandsLocalEnergyAverageArray = new double[totalInstant][];//[subb]
            double variance = 0;
            double c = 2.5;
            double v0 = 1.5;

            for (int k = -LocalLowerBound; k < totalInstant - LocalInstantNumber / 2; k++)
            {
                subbandsLocalEnergyAverageArray[k] = ComputeSubbandsLocalEnergyAverage(subbandsInstantEnergyArray, k);
                for (int i = 0; i < SubbandsNumber; i++)
                {
                    double sum = 0;
                    for (int j = LocalLowerBound; j < LocalInstantNumber / 2; j++)
                    {
                        sum += Math.Pow(subbandsInstantEnergyArray[k + j][i] - subbandsLocalEnergyAverageArray[k][i], 2);
                    }
                    variance = sum / LocalInstantNumber;
                    c = 4 - (Math.Exp(0.3 * i / SubbandsNumber) - 1) * 2;
                    if (log)
                    {
                        Console.WriteLine(string.Format("instant {0}, subb {1} : instant energy {2}, Local Average {3}", k, i, subbandsInstantEnergyArray[k][i], subbandsLocalEnergyAverageArray[k][i]));
                        Console.WriteLine(string.Format("var {0}, C {1}", variance, c));
                    }
                    if (subbandsInstantEnergyArray[k][i] > c * subbandsLocalEnergyAverageArray[k][i] && variance > v0)
                    {
                        beats.Add(new Beat((double)k / LocalInstantNumber, i));
                    }
                }
            }
            return beats;
        }
        static public int getLocalLowerBound()
        {
            if (LocalInstantNumber % 2 == 0)
            {
                return -LocalInstantNumber / 2;
            }
            else
            {
                return -LocalInstantNumber / 2 - 1;
            }
        }
        static public void Resampling(string inFilePath)
        {
            var outFile = MusicDirectory + Path.GetFileNameWithoutExtension(inFilePath) + ".wav";

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

        // convert two bytes to one double in the range 0 to 64
        static double bytesToDouble(byte firstByte, byte secondByte)
        {
            // convert two bytes to one short (little endian)
            short s = (short)((secondByte << 8) | firstByte);

            // convert to range from -1 to 1 (just below)
            //return (double)s / 32768.0;
            return ((s + 32768) / (double)1024);
        }

        // Returns left and right double arrays. 'right' will be null if sound is mono.
        static public void openWav(string filename, out double[] left, out double[] right)
        {
            byte[] wav = File.ReadAllBytes(filename);

            // Determine if mono or complexData
            int channels = wav[22];     // Forget byte 23 as 99.999% of WAVs are 1 or 2 channels

            // Get past all the other sub chunks to get to the data subchunk:
            int pos = 12;   // First Subchunk ID from 12 to 16

            // Keep iterating until we find the data chunk (i.e. 64 61 74 61 ...... (i.e. 100 97 116 97 in decimal))
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
            {
                pos += 4;
                int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
                pos += 4 + chunkSize;
            }
            pos += 8;

            // Pos is now positioned to start of actual sound data.
            int samples = (wav.Length - pos) / 2;     // 2 bytes per sample (16 bit sound mono)
            if (channels == 2) samples /= 2;        // 4 bytes per sample (16 bit complexData)

            // Allocate memory (right will be null if only mono sound)
            left = new double[samples];
            if (channels == 2) right = new double[samples];
            else right = null;

            // Write to double array/s:
            int i = 0;
            while (pos < wav.Length - 1)
            {
                left[i] = bytesToDouble(wav[pos], wav[pos + 1]);
                pos += 2;
                if (channels == 2)
                {
                    right[i] = bytesToDouble(wav[pos], wav[pos + 1]);
                    pos += 2;
                }
                i++;
            }
        }

    }
}
