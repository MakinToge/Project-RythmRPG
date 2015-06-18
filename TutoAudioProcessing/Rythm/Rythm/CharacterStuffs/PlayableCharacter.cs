using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm
{
    class PlayableCharacter : SkilledCharacter
    {
        /// <summary>
        /// Level of the character
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// Value to add when level up
        /// </summary>
        public int healthToAdd { get; set; }
        /// <summary>
        /// Value to add when level up
        /// </summary>
        public int strengthToAdd { get; set; }
        /// <summary>
        /// Value to add when level up
        /// </summary>
        public int enduranceToAdd { get; set; }


        /// <summary>
        /// Default constructor
        /// Will create a character with the default values
        /// </summary>
        public PlayableCharacter()
            : this(START_HEALTH, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT)
        {

        }

        /// <summary>
        /// Construct the character with the given values
        /// </summary>
        public PlayableCharacter(int health, int strength, int endurance, int level, int healthToAdd, int strengthToAdd, int enduranceToAdd)
            : base(health, strength, endurance)
        {
            this.level = level;

            this.healthToAdd = healthToAdd;
            this.strengthToAdd = strengthToAdd;
            this.enduranceToAdd = enduranceToAdd;
        }


        /// <summary>
        /// Construct the character with the given values
        /// </summary>
        public void levelUp()
        {
            this.level++;

            this.healthCpacity += this.healthToAdd;
            this.strength += this.strengthToAdd;
            this.endurance += this.enduranceToAdd;
        }
    }
}
