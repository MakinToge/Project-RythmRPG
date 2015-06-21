using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// The mob class, inherits from Character
    /// </summary>
    public class Mob : AbstractCharacter
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="level">The level</param>
        /// <param name="vitality">The vitality</param>
        /// <param name="attack">The attack</param>
        /// <param name="defense">The defense</param>
        /// <param name="position">The position of the mob on screen</param>
        /// <param name="size">The size of the mob</param>
        public Mob(int level, int vitality, int attack, int defense, Vector2 position, float size)
            : base(level, vitality, attack, defense, position, size, "")
        {
        }

        /// <summary>
        /// Override the attack method from Character
        /// Attack another character
        /// </summary>
        /// <param name="character"></param>
        public override void attackCharacter(AbstractCharacter character)
        {
            base.attackCharacter(character);

            int damageDealt = this.Attack;
            int resistance = character.Defense;

            if(character.skills.Contains(Skills.DefenseBoost))
            {
                resistance += 3;
            }

            damageDealt -= resistance;
            damageDealt += (int)Math.Floor(1.1 * this.Attack);

            if(damageDealt < 0)
            {
                damageDealt = 0;
            }

            character.takeDamage(damageDealt);
        }
    }
}
