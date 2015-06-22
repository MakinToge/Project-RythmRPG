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
        /// <summary>
        /// The default base speed
        /// </summary>
        public const float DEFAULT_BASE_SPEED = 0.3f;
        /// <summary>
        /// The default speed coefficient
        /// </summary>
        public const float DEFAULT_SPEED_COEFFICIENT = 0.1f;

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public double Time { get; private set; }
        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public int Line { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="line">The line.</param>
        public Note(double time, int line)
            : base()
        {

            this.Time = time;
            this.Line = line;

            this.Direction = new Vector2(1, 0);//x = 1 (left)
            this.Position = new Vector2(0.5f * Game1.UnitX, (12 + line) * Game1.UnitY);
            this.Size = new Vector2(Game1.UnitX, Game1.UnitY);
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
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("beat at {0} on line {1}", this.Time, this.Line);
        }

    }
}
