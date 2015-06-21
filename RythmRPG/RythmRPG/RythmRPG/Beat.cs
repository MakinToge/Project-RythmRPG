using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Beat
    {
        public double Time { get; set; }
        public int Subband { get; set; }

        public Beat(double time, int subband)
        {
            this.Time = time;
            this.Subband = subband;
        }
        public override string ToString()
        {
            return string.Format("beat at {0} in subb {1}", this.Time, this.Subband);
        }
    }
}
