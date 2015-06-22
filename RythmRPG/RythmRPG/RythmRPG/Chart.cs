using RythmRPG.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Chart
    {
        /// <summary>
        /// The easy lane0 subband upperlimit
        /// </summary>
        private const int EASY_LANE0_SUBBAND_UPPERLIMIT = 5;

        /// <summary>
        /// The easy lane1 subband upperlimit
        /// </summary>
        private const int EASY_LANE1_SUBBAND_UPPERLIMIT = 10;

        /// <summary>
        /// The normal lane0 subband upperlimit
        /// </summary>
        private const int NORMAL_LANE0_SUBBAND_UPPERLIMIT = 4;

        /// <summary>
        /// The normal lane1 subband upperlimit
        /// </summary>
        private const int NORMAL_LANE1_SUBBAND_UPPERLIMIT = 8;

        /// <summary>
        /// The normal lane2 subband upperlimit
        /// </summary>
        private const int NORMAL_LANE2_SUBBAND_UPPERLIMIT = 12;

        /// <summary>
        /// The hard lane0 subband upperlimit
        /// </summary>
        private const int HARD_LANE0_SUBBAND_UPPERLIMIT = 3;

        /// <summary>
        /// The hard lane1 subband upperlimit
        /// </summary>
        private const int HARD_LANE1_SUBBAND_UPPERLIMIT = 6;

        /// <summary>
        /// The hard lane2 subband upperlimit
        /// </summary>
        private const int HARD_LANE2_SUBBAND_UPPERLIMIT = 9;

        /// <summary>
        /// The hard lane3 subband upperlimit
        /// </summary>
        private const int HARD_LANE3_SUBBAND_UPPERLIMIT = 12;

        /// <summary>
        /// The minimum time interval
        /// </summary>
        private static double minimumTimeInterval = 0.5;

        /// <summary>
        /// The lane number
        /// </summary>
        private static int laneNumber;

        /// <summary>
        /// Gets the lane number.
        /// </summary>
        /// <value>
        /// The lane number.
        /// </value>
        public static int LaneNumber
        {
            get { return 3 + (int)Game1.Difficulty; }
        }


        /// <summary>
        /// Adds the beats to lane.
        /// </summary>
        /// <param name="sSBeats">The s s beats.</param>
        /// <param name="laneIndex">Index of the lane.</param>
        /// <param name="beats">The beats.</param>
        /// <param name="subbandIndexStart">The subband index start.</param>
        /// <param name="subbandIndexStop">The subband index stop.</param>
        static private void AddBeatsToLane(SortedSet<double>[] sSBeats, int laneIndex, List<double>[] beats, int subbandIndexStart, int subbandIndexStop)
        {

            int i = subbandIndexStart;
            double lastTime = 1 + MusicPlaying.LengthSpeedUnit;
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

        /// <summary>
        /// Gets the set array.
        /// </summary>
        /// <param name="beats">The beats.</param>
        /// <param name="difficulty">The difficulty.</param>
        /// <returns></returns>
        static public SortedSet<double>[] getSetArray(List<double>[] beats, Difficulty difficulty)
        {
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
