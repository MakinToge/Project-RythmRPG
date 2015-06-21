using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    public class PlayableCharacter : AbstractCharacter
    {
        private const int LEVEL_MAX = 25;
        private const int GOLD_TO_RESPEC = 100;

        private int[,] levelUpStats;

        public UniqueSkill uniqueSkill { get; set; }

        public int Combo { get; set; }
        public int hitCombo { get; set; }

        public int NbRestart { get; set; }
        public int gold { get; set; }
        public int statPoints { get; set; }
        public string IdleSpriteName { get; set; }
        public int xp { get; set; }
        private int[] xpLevels = { 1000, 3000, 6000, 10000, 15000, 21000, 28000, 36000, 45000,
                                   55000, 66000, 78000, 91000, 105000, 120000, 136000, 153000,
                                   171000, 190000, 210000, 231000, 253000, 276000, 300000 };


        public PlayableCharacter(int level, int vitality, int attack, int defense,
            UniqueSkill skill, int[,] levelUpStats, int combo, int xp, int statPoints, int nbRestart,
            string idleSpriteName, string attackingSpriteName, Vector2 position, float size, string name)
            : base(level, vitality, attack, defense, position, size, name)
        {
            this.levelUpStats = levelUpStats;
            this.uniqueSkill = skill;

            this.hitCombo = 0;
            this.Combo = combo;

            this.NbRestart = nbRestart;
            this.xp = xp;
            this.statPoints = statPoints;

            this.Health = this.Level * this.Vitality + 10;

            this.skills = new List<Skills>(this.NbRestart);

            this.IdleSpriteName = idleSpriteName;
        }

        public override void attackCharacter(AbstractCharacter character)
        {
            base.attackCharacter(character);

            int damageDealt = this.Attack;
            int resistance = character.Defense;
            this.hitCombo++;
            
            if(this.uniqueSkill == UniqueSkill.Berserker)
            {
                damageDealt = this.Defense;
            }

            if (this.skills.Contains(Skills.CriticalDamage) && this.hitCombo == this.Combo)
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
                    damageDealt = character.Health;
                }
            }

            character.takeDamage(damageDealt);
        }

        public override void levelUp()
        {
            if(this.Level < LEVEL_MAX)
            {
                base.levelUp();

                this.Vitality += this.levelUpStats[this.Level % 3, 0];
                this.Attack += this.levelUpStats[this.Level % 3, 1];
                this.Defense += this.levelUpStats[this.Level % 3, 2];

                if(this.uniqueSkill == UniqueSkill.GoldDigger)
                {
                    this.statPoints += 3;
                }
            }
        }

        public void increaseVitality()
        {
            if (this.statPoints > 0)
            {
                this.Vitality++;
                this.statPoints--;
            }
        }

        public void increaseAttack()
        {
            if (this.statPoints > 0)
            {
                this.Attack++;
                this.statPoints--;
            }
        }

        public void increaseDefense()
        {
            if (this.statPoints > 0)
            {
                this.Defense++;
                this.statPoints--;
            }
        }

        public void gainXP(int xp)
        {
            if(this.Level < LEVEL_MAX)
            {
                this.xp += xp;

                if(this.xp > xpLevels[LEVEL_MAX - 2])
                {
                    this.xp = xpLevels[LEVEL_MAX - 2];
                }

                if (this.xp >= xpLevels[this.Level - 1])
                {
                    this.levelUp();
                }
            }
        }

        public bool restart()
        {
            if(this.Level == LEVEL_MAX)
            {
                this.NbRestart++;

                this.Vitality = 1;
                this.Attack = 1;
                this.Defense = 1;

                this.xp = 0;
                this.Level = 1;
                this.statPoints = 0;

                this.skills = new List<Skills>(this.NbRestart);

                return true;
            }

            return false;
        }

        public bool respec()
        {
            if(this.gold >= GOLD_TO_RESPEC)
            {
                this.gold -= GOLD_TO_RESPEC;

                this.Vitality = 1;
                this.Attack = 1;
                this.Defense = 1;

                this.statPoints = 3 * (this.Level - 1);

                return true;
            }

            return false;
        }

        public void manageSkill(Skills skillToRemove, Skills skillToActivate)
        {
            if (skillToRemove != null)
            {
                this.skills.Remove(skillToRemove);
            }
            if (skillToActivate != null)
            {
                this.skills.Add(skillToActivate);
            }
        }
    }
}
