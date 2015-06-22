using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Chart
    {
        private const int EASY_LANE0_SUBBAND_UPPERLIMIT = 4;
        private const int EASY_LANE1_SUBBAND_UPPERLIMIT = 9;

        private const int NORMAL_LANE0_SUBBAND_UPPERLIMIT = 3;
        private const int NORMAL_LANE1_SUBBAND_UPPERLIMIT = 7;
        private const int NORMAL_LANE2_SUBBAND_UPPERLIMIT = 11;

        private const int HARD_LANE0_SUBBAND_UPPERLIMIT = 2;
        private const int HARD_LANE1_SUBBAND_UPPERLIMIT = 5;
        private const int HARD_LANE2_SUBBAND_UPPERLIMIT = 8;
        private const int HARD_LANE3_SUBBAND_UPPERLIMIT = 12;
        static private double minimumTimeInterval = 0.5;
        static public int LaneNumber = 3;


        static private void AddBeatsToLane(SortedSet<double>[] sSBeats, int laneIndex, List<double> beats)
        {
            double lastTime = 0;
            foreach (var beat in beats)
            {
                if (lastTime + minimumTimeInterval <= beat)
                {
                    if (sSBeats[laneIndex].Add(beat))
                    {
                        lastTime = beat;
                    }
                }
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
                for (int i = 0; i < EASY_LANE0_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 0, beats[i]);
                }
                for (int i = EASY_LANE0_SUBBAND_UPPERLIMIT; i < EASY_LANE1_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 1, beats[i]);
                }
                for (int i = EASY_LANE1_SUBBAND_UPPERLIMIT; i < AudioAnalyser.SubbandsNumber; i++)
                {
                    AddBeatsToLane(sSBeats, 2, beats[i]);
                }
            }
            else if (difficulty == Difficulty.Veteran)
            {
                for (int i = 0; i < NORMAL_LANE0_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 0, beats[i]);
                }
                for (int i = NORMAL_LANE0_SUBBAND_UPPERLIMIT; i < NORMAL_LANE1_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 1, beats[i]);
                }
                for (int i = NORMAL_LANE1_SUBBAND_UPPERLIMIT; i < NORMAL_LANE2_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 2, beats[i]);
                }
                for (int i = NORMAL_LANE2_SUBBAND_UPPERLIMIT; i < AudioAnalyser.SubbandsNumber; i++)
                {
                    AddBeatsToLane(sSBeats, 3, beats[i]);
                }
            }
            else if (difficulty == Difficulty.GodLike)
            {
                for (int i = 0; i < HARD_LANE0_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 0, beats[i]);
                }
                for (int i = HARD_LANE0_SUBBAND_UPPERLIMIT; i < HARD_LANE1_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 1, beats[i]);
                }
                for (int i = HARD_LANE1_SUBBAND_UPPERLIMIT; i < HARD_LANE2_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 2, beats[i]);
                }
                for (int i = HARD_LANE2_SUBBAND_UPPERLIMIT; i < HARD_LANE3_SUBBAND_UPPERLIMIT; i++)
                {
                    AddBeatsToLane(sSBeats, 3, beats[i]);
                }
                for (int i = HARD_LANE3_SUBBAND_UPPERLIMIT; i < AudioAnalyser.SubbandsNumber; i++)
                {
                    AddBeatsToLane(sSBeats, 4, beats[i]);
                }
            }
            return sSBeats;
        }
    }
}
