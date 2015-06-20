using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    class Mob : Character
    {
        public Mob(int level, int vitality, int attack, int defense,
            string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
            : base(level, vitality, attack, defense, idleSpriteName, attackingSpriteName, position, size)
        {
        }

        public override void attackCharacter(Character character)
        {
            int damageDealt = this.attack;
            int resistance = character.defense;

            if(character.skills.Contains(Skills.EnduranceBoost))
            {
                resistance += 3;
            }

            damageDealt -= resistance;
            damageDealt += (int)Math.Floor(1.1 * this.attack);

            if(damageDealt < 0)
            {
                damageDealt = 0;
            }

            character.takeDamage(damageDealt);
        }
    }
}
