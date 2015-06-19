using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm.CharacterStuffs
{
    class PlayableCharacter : Character
    {
        public int combo { get; set; }
        public int hitCombo { get; set; }

        public PlayableCharacter(int level, int vitality, int attack, int defense,
            string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
            : base(level, vitality, attack, defense, idleSpriteName, attackingSpriteName, position, size)
        {
            this.hitCombo = 0;
        }

        public override void attackCharacter(Character character)
        {
            int damageDealt = this.attack;
            int resistance = character.defense;

            if (this.skills.Contains(Skills.StrengthBoost))
            {
                damageDealt += 3;
            }

            try
            {
                Boss boss = (Boss)character;

                if(this.skills.Contains(Skills.StrengthMegaBoost))
                {
                    damageDealt += 3;
                }
            }
            catch(InvalidCastException e)
            {
                // It's not a boss, nothing more to do
            }
            
            if(character.skills.Contains(Skills.EnduranceBoost))
            {
                resistance += 3;
            }

            damageDealt -= resistance;

            if (damageDealt < 0)
            {
                damageDealt = 0;
            }

            character.takeDamage(damageDealt);

            this.hitCombo++;
        }
    }
}
