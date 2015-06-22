using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class AudioAnalyser
    {
        private static bool log = false;
        public const int SubbandsNumber = 16;
        private const int InstantWidth = 1024;//must be a power of 2 and a multiple of SubBandNumber
        private const int LocalWidth = 44032;//must be a multiple of InstantWidth(best result if ~1sec of music (== SampleRate))
        private const int LocalInstantNumber = LocalWidth / InstantWidth;
        private static int LocalLowerBound = getLocalLowerBound();
        private const string MusicDirectory = "musicDir";//Resampling output directory

        static private double[] ComputeSubbandsLocalEnergyAverage(double[][] instantSubbandsEnergyArray, int historyIndex)//<Ei>, LocalLowBound <= historyIndex < subbandsInstantEnergyArray.Length - LocalInstantNumber/2
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

        static private double[] ComputeSubbandsInstantEnergy(double[] instantBuffer)//(Es)
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

        static private double[][] ComputeSubbandsInstantEnergyArray(double[] left, double[] right)
        {
            Lomont.LomontFFT fftTool = new Lomont.LomontFFT();
            int totalInstant = left.Length / InstantWidth;
            double[][] complexData = new double[totalInstant][];//[InstantWidth * 2];
            double[][] subbandsInstantEnergyArray = new double[totalInstant][];//[SubbandsNumber]

            for (int i = 0; i < totalInstant; i++)
            {
                //Console.WriteLine(string.Format("instant {0} out of {1}", i, totalInstant));
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
        }

        static public List<double>[] DetectBeat(string wavFilePath)
        {
            List<double>[] beats = new List<double>[16];
            for (int i = 0; i < SubbandsNumber; i++)
            {
                beats[i] = new List<double>();
            }

            double[] left;
            double[] right;
            wavFileReader.ReadWavFile(wavFilePath, out left, out right);

            if (right == null)//if mono
            {
                right = left;
            }

            int totalInstant = left.Length / InstantWidth;
            //if (totalInstant < LocalWidth) ;//music way too short !!

            double[][] subbandsInstantEnergyArray = ComputeSubbandsInstantEnergyArray(left, right);//[totInst][subb]
            double[][] subbandsLocalEnergyAverageArray = new double[totalInstant][];//[subb]
            double localVariance = 0;
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
                    localVariance = sum / LocalInstantNumber;
                    c = 3 - (Math.Exp(0.3) - 1) * 2;//(Math.Exp(0.3 * i / SubbandsNumber) - 1) * 2;
                    if (log)
                    {
                        Console.WriteLine(string.Format("instant {0}, subb {1} : instant energy {2}, Local Average {3}", k, i, subbandsInstantEnergyArray[k][i], subbandsLocalEnergyAverageArray[k][i]));
                        Console.WriteLine(string.Format("var {0}, C {1}", localVariance, c));
                    }
                    if (subbandsInstantEnergyArray[k][i] > c * subbandsLocalEnergyAverageArray[k][i] && localVariance > v0)
                    {
                        beats[i].Add((double)k / LocalInstantNumber);
                    }
                }
            }
            return beats;
        }
        static private int getLocalLowerBound()
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
    }
}

