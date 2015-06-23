using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// The playable character class, inherits from Character
    /// </summary>
    [Serializable]
    public class PlayableCharacter : AbstractCharacter
    {
        /// <summary>
        /// The maximum level of the character
        /// </summary>
        private const int LEVEL_MAX = 25;

        /// <summary>
        /// The amount of gold needed to respec
        /// </summary>
        private const int GOLD_TO_RESPEC = 100;

        /// <summary>
        /// The stats that the character gains when leveling up
        /// </summary>
        private int[,] levelUpStats;

        /// <summary>
        /// The unique skill of the player
        /// </summary>
        public UniqueSkill uniqueSkill { get; set; }

        /// <summary>
        /// The number of hits needed to perform a combo, depends on the difficulty
        /// </summary>
        public int Combo { get; set; }

        /// <summary>
        /// The number of successful hits
        /// </summary>
        public int hitCombo { get; set; }

        /// <summary>
        /// The number of restart
        /// </summary>
        public int NbRestart { get; set; }

        /// <summary>
        /// The amount of gold
        /// </summary>
        public int gold { get; set; }

        /// <summary>
        /// The amount of statPoint to spend
        /// </summary>
        public int statPoints { get; set; }

        /// <summary>
        /// The amount of experience the character got
        /// </summary>
        public int xp { get; set; }

        /// <summary>
        /// The amount of experience the player needs to level up
        /// </summary>
        private int[] xpLevels = { 1000, 3000, 6000, 10000, 15000, 21000, 28000, 36000, 45000,
                                   55000, 66000, 78000, 91000, 105000, 120000, 136000, 153000,
                                   171000, 190000, 210000, 231000, 253000, 276000, 300000 };


        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="level">The level</param>
        /// <param name="vitality">The vitality</param>
        /// <param name="attack">The attack</param>
        /// <param name="defense">The defense</param>
        /// <param name="skill">The list of skills the player got</param>
        /// <param name="levelUpStats">The array of experience needed to level up</param>
        /// <param name="combo">Number of hits needed to perform a combo</param>
        /// <param name="xp">Amount of experience the player got</param>
        /// <param name="statPoints">Amount of statPoints the player got</param>
        /// <param name="nbRestart">The number of restart</param>
        /// <param name="position">The position of the player on screen</param>
        /// <param name="size">The size of the player</param>
        /// <param name="name">The name of the player</param>
        public PlayableCharacter(int level, int vitality, int attack, int defense,
            UniqueSkill skill, int[,] levelUpStats, int combo, int xp, int statPoints, int nbRestart, int gold,
            Vector2 position, float scale, string name)
            : base(level, vitality, attack, defense, position, scale, name)
        {
            this.levelUpStats = levelUpStats;
            this.uniqueSkill = skill;

            this.hitCombo = 0;
            this.Combo = combo;

            this.NbRestart = nbRestart;
            this.xp = xp;
            this.statPoints = statPoints;
            this.gold = gold;

            this.Health = this.Level * this.Vitality + 100;

            this.skills = new List<Skills>(this.NbRestart);
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
            this.hitCombo++;

            if (this.uniqueSkill == UniqueSkill.Templar)
            {
                damageDealt = this.Defense;
            }

            if (this.skills.Contains(Skills.CriticalDamage) && this.hitCombo == this.Combo)
            {
                damageDealt += 9;
                this.hitCombo = 0;
            }

            if (this.skills.Contains(Skills.AttackBoost))
            {
                damageDealt += 3;
            }

            try
            {
                Boss boss = (Boss)character;

                if (this.skills.Contains(Skills.AttackMegaBoost))
                {
                    damageDealt += 3;
                }
            }
            catch (InvalidCastException e)
            {
                // It's not a boss, nothing more to do
            }

            if (character.skills.Contains(Skills.DefenseBoost))
            {
                resistance += 3;
            }

            damageDealt -= resistance;

            if (damageDealt <= 0)
            {
                damageDealt = 1;
            }

            if (this.uniqueSkill == UniqueSkill.FatalBlow)
            {
                Random rand = new Random();
                if (rand.Next(100) <= 5)
                {
                    damageDealt = character.Health;
                }
            }

            character.takeDamage(damageDealt);
        }

        /// <summary>
        /// Upgrade stats
        /// </summary>
        public override void levelUp()
        {
            if (this.Level < LEVEL_MAX)  // Prevents going higher than permitted
            {
                base.levelUp();

                // Upgrade the stats depending on the level
                this.Vitality += this.levelUpStats[this.Level % 3, 0];
                this.Attack += this.levelUpStats[this.Level % 3, 1];
                this.Defense += this.levelUpStats[this.Level % 3, 2];

                if (this.uniqueSkill == UniqueSkill.GoldDigger)  // i.e. the custom character
                {
                    this.statPoints += 3;
                }
            }
        }

        /// <summary>
        /// Override addVitality from Character
        /// Add the given value to the vitality
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public override void addVitality(int nbPoints)
        {
            if (this.statPoints >= nbPoints)
            {
                this.Vitality += nbPoints;
                this.statPoints -= nbPoints;
            }
        }

        /// <summary>
        /// Override addVitality from Character
        /// Add the given value to the attack
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public override void addAttack(int nbPoints)
        {
            if (this.statPoints >= nbPoints)
            {
                this.Attack += nbPoints;
                this.statPoints -= nbPoints;
            }
        }

        /// <summary>
        /// Override addVitality from Character
        /// Add the given value to the defense
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public override void addDefense(int nbPoints)
        {
            if (this.statPoints >= nbPoints)
            {
                this.Defense += nbPoints;
                this.statPoints -= nbPoints;
            }
        }

        /// <summary>
        /// Add experience to the player
        /// </summary>
        /// <param name="xp">Amount of experience the player earned</param>
        public void gainXP(int xp)
        {
            if (this.Level < LEVEL_MAX)
            {
                this.xp += xp;

                if (this.xp > xpLevels[LEVEL_MAX - 2])   // Prevents from going higher than the limit (360 000)
                {
                    this.xp = xpLevels[LEVEL_MAX - 2];
                }

                if (this.xp >= xpLevels[this.Level - 1])    // Gained enough xp to level up
                {
                    this.levelUp();
                }
            }
        }

        /// <summary>
        /// Restart the character, need to be at level max
        /// </summary>
        /// <returns>True if the character restarted, false if not permitted</returns>
        public bool restart()
        {
            if (this.Level == LEVEL_MAX)
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

        /// <summary>
        /// Respec, i.e. restart the stats
        /// Need a certain amount of gold
        /// </summary>
        /// <returns>True if respec successful, false if not enough gold</returns>
        public bool respec()
        {
            if (this.gold >= GOLD_TO_RESPEC) // Check if enough gold
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

        /// <summary>
        /// Replace a skill by another
        /// </summary>
        /// <param name="skillToRemove">The skill to remove</param>
        /// <param name="skillToActivate">The skill to add</param>
        public bool manageSkill(Skills skillToRemove, Skills skillToActivate)
        {
            if (skillToRemove != Skills.None)
            {
                this.skills.Remove(skillToRemove);
                return true;
            }
            if (skillToActivate != Skills.None)
            {
                if (this.skills.Count <= this.NbRestart)
                {
                    if (!this.skills.Contains(skillToActivate))
                    {
                        this.skills.Add(skillToActivate);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gives the amount of experience earned by killing the character
        /// </summary>
        /// <param name="nbInput">The number of input of the music</param>
        /// <param name="difficulty">The difficulty of the game</param>
        /// <returns>The amount of experience earned</returns>
        public override int giveXP(int nbInput, Difficulty difficulty)
        {
            return 0;
        }

        /// <summary>
        /// Initialize the character for the music
        /// </summary>
        public override void prepareForMusic()
        {
            base.prepareForMusic();
            this.Health = this.Level * this.Vitality + 100;
        }

        /// <summary>
        /// Returns the amount of experience needed to pass the level
        /// </summary>
        /// <returns>The amount of experience needed</returns>
        public int xpToNextLevel()
        {
            if (this.Level < 25)
                return this.xpLevels[this.Level - 1] - this.xp;
            else
                return 0;
        }

        /// <summary>
        /// Returns the total health points of the character
        /// </summary>
        /// <returns>The total of health points</returns>
        public override int getMaxHealth()
        {
            return base.getMaxHealth() + 100;
        }
    }
}
