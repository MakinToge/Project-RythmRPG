using Microsoft.Xna.Framework;
using RythmRPG.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    class Note : Sprite
    {
        public const float DEFAULT_BASE_SPEED = 0.3f;
        public const float DEFAULT_SPEED_COEFFICIENT = 0.1f;

        public double Time { get; private set; }
        public int Line { get; set; }

        public Note(double time, int line):base()
        {
            
            this.Time = time;
            this.Line = line;

            this.Direction = new Vector2(1, 0);//x = 1 (left)
            this.Position = new Vector2(0.5f*Game1.UnitX, (12 + line) * Game1.UnitY);
            this.Size = new Vector2(Game1.UnitX,Game1.UnitY);
            this.Speed = DEFAULT_BASE_SPEED + DEFAULT_SPEED_COEFFICIENT * (float)Game1.Difficulty;

            //Load png
            string assetName = "MusicPlaying/note";
            if (line == 0)
            {
                assetName = "MusicPlaying/noteGreen";
            }
            else if (line == 1)
            {
                assetName = "MusicPlaying/noteRed";
            }
            else if (line == 2)
            {
                assetName = "MusicPlaying/noteYellow";
            }
            else if (line == 3)
            {
                assetName = "MusicPlaying/noteBlue";
            }
            else
            {
                assetName = "MusicPlaying/noteOrange";
            }
            this.LoadContent(MusicPlaying.Content, assetName);
        }
        public override string ToString()
        {
            return string.Format("beat at {0} on line {1}", this.Time, this.Line);
        }

    }
}
