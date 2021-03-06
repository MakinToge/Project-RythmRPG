﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm.CharacterStuffs
{
    class Boss : Character
    {
        public Boss(int level, int vitality, int attack, int defense,
            string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
            : base(level, vitality, attack, defense, idleSpriteName, attackingSpriteName, position, size)
        {
        }

        public override void attackCharacter(Character character)
        {
            int damageDealt = this.attack;
            int resistance = character.defense;

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
