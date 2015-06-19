using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rythm.CharacterStuffs
{
    class PlayableCharacter : Character
    {
        private int[,] levelUpStats;

        public UniqueSkill uniqueSkill { get; set; }

        public int combo { get; set; }
        public int hitCombo { get; set; }

        public PlayableCharacter(int level, int vitality, int attack, int defense,
            UniqueSkill skill, int[,] levelUpStats,
            string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
            : base(level, vitality, attack, defense, idleSpriteName, attackingSpriteName, position, size)
        {
            this.levelUpStats = levelUpStats;
            this.uniqueSkill = skill;
            this.hitCombo = 0;
        }

        public override void attackCharacter(Character character)
        {
            int damageDealt = this.attack;
            int resistance = character.defense;
            this.hitCombo++;
            
            if(this.uniqueSkill == UniqueSkill.Berserker)
            {
                damageDealt = this.defense;
            }

            if (this.skills.Contains(Skills.CriticalDamage) && this.hitCombo == this.combo)
            {
                damageDealt += 9;
                this.hitCombo = 0;
            }

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

            if(this.uniqueSkill == UniqueSkill.FatalBlow)
            {
                Random rand = new Random();
                if (rand.Next(100) <= 5)
                {
                    damageDealt = character.health;
                }
            }

            character.takeDamage(damageDealt);
        }

        public override void levelUp()
        {
            base.levelUp();

            this.vitality += this.levelUpStats[this.level % 3, 0];
            this.attack += this.levelUpStats[this.level % 3, 1];
            this.defense += this.levelUpStats[this.level % 3, 2];
        }
    }
}
