using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public Boss(Difficulty diff, Vector2 position, float scale)
            : base(0, 0, 0, 0, position, scale, "")
        {
            int difficulty = 1;
            Random rand = new Random();

            switch (diff)
            {
                case Difficulty.Casual:
                    difficulty = 1;
                    this.Level = rand.Next(7, 9);
                    break;
                case Difficulty.Veteran:
                    difficulty = 3;
                    this.Level = rand.Next(17, 19);
                    this.skills.Add(Skills.AttackBoost);
                    break;
                case Difficulty.GodLike:
                    difficulty = 6;
                    this.Level = 25;
                    this.skills.Add(Skills.AttackBoost);
                    this.skills.Add(Skills.DefenseBoost);
                    break;
            }

            this.Attack = rand.Next(7, 9) * difficulty;
            this.Defense = rand.Next(7, 9) * difficulty;

            this.Vitality = (this.Attack + this.Defense + 50) * difficulty;
            this.Health = this.Vitality * this.Level;
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

        public void Load(ContentManager content)
        {
            Random rand = new Random();

            switch (rand.Next(2))
            {
                case 0:
                    this.sprites.Load(content, "Spritesheet/Boss/BOSS_Bull_Idle", "Spritesheet/Boss/BOSS_Bull_Attack", 2, 4, 10);
                    break;
                case 1:
                    this.sprites.Load(content, "Spritesheet/Boss/BOSS_King_Idle", "Spritesheet/Boss/BOSS_King_Attack", 2, 4, 10);
                    break;
            }
        }

        public int giveXP()
        {
            return this.Vitality * this.Level;
        }
    }
}
