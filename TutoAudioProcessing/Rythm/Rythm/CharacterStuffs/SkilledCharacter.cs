using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm
{
    /// <summary>
    /// This class is for the players and bosses
    /// </summary>
    class SkilledCharacter : Character
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
        /// List of skills
        /// </summary>
        public List<Skills> skills { get; set; }

        /// <summary>
        /// Default constructor
        /// Will create a character with the default values
        /// </summary>
        public SkilledCharacter()
            : this(START_HEALTH, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT, MINIMUM_STAT)
        {

        }

        /// <summary>
        /// Construct the character with the given values
        /// </summary>
        public SkilledCharacter(int health, int strength, int endurance, int level, int healthToAdd, int strengthToAdd, int enduranceToAdd)
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

        /// <summary>
        /// Deals damage to the given character
        /// Returns true if the attacked character is dead, false otherwise
        /// </summary>
        public override bool attack(Character character)
        {
            int damageDealt = this.strength;
            int endurance = character.endurance;

            try
            {
                SkilledCharacter tmpCharacter = (SkilledCharacter)character;

                if (this.skills.Contains(Skills.StrengthMegaBoost))
                {
                    double tmp = this.strength * 2;
                    damageDealt = (int)Math.Floor(tmp);
                }
                else if(this.skills.Contains(Skills.StrengthBoost))
                {
                    double tmp = this.strength * 1.5;
                    damageDealt = (int)Math.Floor(tmp);
                }

                if (tmpCharacter.skills.Contains(Skills.EnduranceBoost))
                {
                    double tmp = character.endurance * 1.5;
                    endurance = (int)Math.Floor(tmp);
                }
                else if (tmpCharacter.skills.Contains(Skills.EnduranceMegaBoost))
                {
                    double tmp = character.endurance * 2;
                    endurance = (int)Math.Floor(tmp);
                }
            }
            catch (InvalidCastException e) // ie it's a mob
            {
                if (this.skills.Contains(Skills.StrengthBoost))
                {
                    double tmp = this.strength * 1.5;
                    damageDealt = (int)Math.Floor(tmp);
                }
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
