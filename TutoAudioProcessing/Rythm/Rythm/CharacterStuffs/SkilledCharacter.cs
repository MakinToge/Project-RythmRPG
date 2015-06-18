using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm
{
    class SkilledCharacter : Character
    {
        /// <summary>
        /// List of skills
        /// </summary>
        public List<Skills> skills { get; set; }

        /// <summary>
        /// Default constructor
        /// Will create a character with the default values
        /// </summary>
        public SkilledCharacter() : this(START_HEALTH, MINIMUM_STAT, MINIMUM_STAT)
        {

        }

        /// <summary>
        /// Construct the character with the given values
        /// </summary>
        public SkilledCharacter(int health, int strength, int endurance) : base(health, strength, endurance)
        {
            this.skills = new List<Skills>();
        }

        /// <summary>
        /// Deals damage to the given character
        /// Returns true if the attacked character is dead, false otherwise
        /// </summary>
        public override bool attack(Character character)
        {
            int damageDealt = this.strength;
            int endurance;

            try
            {
                SkilledCharacter tmpCharacter = (SkilledCharacter)character;

                if (this.skills.Contains(Skills.StrengthMegaBoost))
                {
                    double tmp = this.strength * 2;
                    damageDealt = (int)Math.Floor(tmp);
                }

                if (tmpCharacter.skills.Contains(Skills.EnduranceBoost))
                {
                    double tmp = character.endurance * 1.5;
                    endurance = (int)Math.Floor(tmp);
                }
                else
                {
                    endurance = character.endurance;
                }
            }
            catch (InvalidCastException e) // ie it's a mob
            {
                if (this.skills.Contains(Skills.StrengthBoost))
                {
                    double tmp = this.strength * 1.5;
                    damageDealt = (int)Math.Floor(tmp);
                }

                endurance = character.endurance;
            }

            damageDealt -= endurance;

            // TODO : Find a better way to deal damage

            // In case the attacked character endurance is bigger than the strength of the attacking one
            if (damageDealt < 0)
            {
                damageDealt = 0;
            }

            character.removeHealth(damageDealt);

            return character.isDead();
        }
    }
}
