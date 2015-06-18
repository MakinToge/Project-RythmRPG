using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm
{
    /// <summary>
    /// This base class is mainly for the mobs
    /// </summary>
    class Character
    {
        protected const int MINIMUM_STAT = 1;
        protected const int START_HEALTH = 50;

        /// <summary>
        /// Health maximum capacity
        /// </summary>
        public int healthCpacity { get; set; }
        /// <summary>
        /// Health level
        /// </summary>
        public int health { get; set; }
        /// <summary>
        /// Strength level
        /// </summary>
        public int strength { get; set; }
        /// <summary>
        /// Endurance level
        /// </summary>
        public int endurance { get; set; }
        
        /// <summary>
        /// Default constructor
        /// Will create a character with the default values
        /// </summary>
        public Character()
            : this(START_HEALTH, MINIMUM_STAT, MINIMUM_STAT)
        {
            
        }

        /// <summary>
        /// Will create a character with the given values
        /// </summary>
        public Character(int health, int strength, int endurance)
        {
            this.health = health;
            this.healthCpacity = health;
            this.strength = strength;
            this.endurance = endurance;
        }

        /// <summary>
        /// Add health
        /// </summary>
        public void addHealth(int valueToAdd)
        {
            this.health += valueToAdd;
        }

        /// <summary>
        /// Remove health
        /// </summary>
        public void removeHealth(int valueToRemove)
        {
            this.health -= valueToRemove;
        }

        /// <summary>
        /// Add strength
        /// </summary>
        public void addStrength(int valueToAdd)
        {
            this.strength += valueToAdd;
        }

        /// <summary>
        /// Remove strength but prevent it from going under the minimum value
        /// </summary>
        public void removeStrength(int valueToRemove)
        {
            this.strength -= valueToRemove;

            if(this.strength < MINIMUM_STAT)
            {
                this.strength = MINIMUM_STAT;
            }
        }

        /// <summary>
        /// Add endurance
        /// </summary>
        public void addEndurance(int valueToAdd)
        {
            this.endurance += valueToAdd;
        }

        /// <summary>
        /// Remove endurance but prevent it from going under the minimum value
        /// </summary>
        public void removeEndurance(int valueToRemove)
        {
            this.endurance -= valueToRemove;

            if (this.endurance < MINIMUM_STAT)
            {
                this.endurance = MINIMUM_STAT;
            }
        }

        /// <summary>
        /// Return true if the character is dead, false otherwise
        /// </summary>
        public bool isDead()
        {
            return this.health < 0;
        }

        /// <summary>
        /// Reset all stats to default values
        /// </summary>
        public void reset()
        {
            this.health = START_HEALTH;
            this.strength = MINIMUM_STAT;
            this.endurance = MINIMUM_STAT;
        }

        /// <summary>
        /// Deals damage to the given character
        /// Returns true if the attacked character is dead, false otherwise
        /// </summary>
        public virtual bool attack(Character character)
        {
            int damageDealt = this.strength;
            int endurance = character.endurance;

            try
            {
                SkilledCharacter tmpCharacter = (SkilledCharacter)character;

                if (tmpCharacter.skills.Contains(Skills.EnduranceBoost))
                {
                    double tmp = character.endurance * 1.5;
                    endurance = (int)Math.Floor(tmp);
                }
            }
            catch (InvalidCastException e) // ie it's a mob, theoretically impossible
            {
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
