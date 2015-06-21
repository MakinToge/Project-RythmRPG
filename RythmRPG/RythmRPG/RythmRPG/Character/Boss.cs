using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// The boss class, inherits from Character
    /// </summary>
    public class Boss : AbstractCharacter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level">The level</param>
        /// <param name="vitality">The vitality</param>
        /// <param name="attack">The attack</param>
        /// <param name="defense">The defense</param>
        /// <param name="position">The position of the boss on screen</param>
        /// <param name="scale">The size of the boss</param>
        public Boss(int level, int vitality, int attack, int defense, Vector2 position, Vector2 size, float scale)
            : base(level, vitality, attack, defense, position, size, scale, "")
        {
        }

        /// <summary>
        /// Override the attack method from Character
        /// Attack another character
        /// </summary>
        /// <param name="character">The character to attack</param>
        public override void attackCharacter(AbstractCharacter character)
        {
            base.attackCharacter(character);

            int damageDealt = this.Attack;
            int resistance = character.Defense;

            if (this.skills.Contains(Skills.AttackBoost))
            {
                damageDealt += 3;
            }

            if (character.skills.Contains(Skills.EnduranceMegaBoost))
            {
                resistance += 6;
            }
            else if (character.skills.Contains(Skills.DefenseBoost))
            {
                resistance += 3;
            }

            damageDealt -= resistance;

            if (damageDealt < 0)
            {
                damageDealt = 0;
            }

            character.takeDamage(damageDealt);
        }
    }
}
