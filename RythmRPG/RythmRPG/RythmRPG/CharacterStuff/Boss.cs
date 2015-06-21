using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    public class Boss : AbstractCharacter
    {
        public Boss(int level, int vitality, int attack, int defense,
            string idleSpriteName, string attackingSpriteName, Vector2 position, float size)
            : base(level, vitality, attack, defense, position, size, "")
        {
        }

        public override void attackCharacter(AbstractCharacter character)
        {
            base.attackCharacter(character);

            int damageDealt = this.Attack;
            int resistance = character.Defense;

            if (this.skills.Contains(Skills.StrengthBoost))
            {
                damageDealt += 3;
            }

            if (character.skills.Contains(Skills.EnduranceMegaBoost))
            {
                resistance += 6;
            }
            else if (character.skills.Contains(Skills.EnduranceBoost))
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
