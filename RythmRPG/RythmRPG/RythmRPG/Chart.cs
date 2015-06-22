using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Chart
    {
        private const int EASY_LANE0_SUBBAND_UPPERLIMIT = 5;//4
        private const int EASY_LANE1_SUBBAND_UPPERLIMIT = 10;//5//7

        private const int NORMAL_LANE0_SUBBAND_UPPERLIMIT = 4;//3
        private const int NORMAL_LANE1_SUBBAND_UPPERLIMIT = 8;//4
        private const int NORMAL_LANE2_SUBBAND_UPPERLIMIT = 12;//4//5

        private const int HARD_LANE0_SUBBAND_UPPERLIMIT = 3;//2
        private const int HARD_LANE1_SUBBAND_UPPERLIMIT = 6;//3
        private const int HARD_LANE2_SUBBAND_UPPERLIMIT = 9;//3
        private const int HARD_LANE3_SUBBAND_UPPERLIMIT = 12;//4//4
        static private double minimumTimeInterval = 0.5;
        static public int LaneNumber = 3;


        static private void AddBeatsToLane(SortedSet<double>[] sSBeats, int laneIndex, List<double>[] beats, int subbandIndexStart, int subbandIndexStop)
        {

            int i = subbandIndexStart;
            double lastTime = 1;
            while (i < subbandIndexStop)
            {
                foreach (double beat in beats[i])
                {
                    if (lastTime + minimumTimeInterval <= beat)
                    {
                        if (sSBeats[laneIndex].Add(beat))
                        {
                            lastTime = beat;
                        }
                    }
                }
                i++;
            }
        }
        static public SortedSet<double>[] getSetArray(List<double>[] beats, Difficulty difficulty)
        {
            LaneNumber = (int)Game1.Difficulty + 3;
            minimumTimeInterval = (5 - (int)difficulty) / 8.0;
            SortedSet<double>[] sSBeats = new SortedSet<double>[LaneNumber];
            for (int i = 0; i < LaneNumber; i++)
            {
                sSBeats[i] = new SortedSet<double>();
            }

            if (difficulty == Difficulty.Casual)
            {
                AddBeatsToLane(sSBeats, 0, beats, 0, EASY_LANE0_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 1, beats, EASY_LANE0_SUBBAND_UPPERLIMIT, EASY_LANE1_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 2, beats, EASY_LANE1_SUBBAND_UPPERLIMIT, AudioAnalyser.SubbandsNumber);
                
            }
            else if (difficulty == Difficulty.Veteran)
            {
                AddBeatsToLane(sSBeats, 0, beats, 0, NORMAL_LANE0_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 1, beats, NORMAL_LANE0_SUBBAND_UPPERLIMIT, NORMAL_LANE1_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 2, beats, NORMAL_LANE1_SUBBAND_UPPERLIMIT, NORMAL_LANE2_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 3, beats, NORMAL_LANE2_SUBBAND_UPPERLIMIT, AudioAnalyser.SubbandsNumber);
            }
            else if (difficulty == Difficulty.GodLike)
            {
                AddBeatsToLane(sSBeats, 0, beats, 0, HARD_LANE0_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 1, beats, HARD_LANE0_SUBBAND_UPPERLIMIT, HARD_LANE1_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 2, beats, HARD_LANE1_SUBBAND_UPPERLIMIT, HARD_LANE2_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 3, beats, HARD_LANE2_SUBBAND_UPPERLIMIT, HARD_LANE3_SUBBAND_UPPERLIMIT);
                AddBeatsToLane(sSBeats, 4, beats, HARD_LANE3_SUBBAND_UPPERLIMIT, AudioAnalyser.SubbandsNumber);
            }
            return sSBeats;
        }
    }
}
