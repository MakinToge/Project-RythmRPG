using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        /// <param name="scale">The size of the mob</param>
        public Mob(Difficulty diff, Vector2 position, float scale)
            : base(0, 0, 0, 0, position, scale, "")
        {
            int difficulty = 1;
            Random rand = new Random();

            switch(diff)
            {
                case Difficulty.Casual:
                    difficulty = 1;
                    this.Level = rand.Next(1, 6);
                    break;
                case Difficulty.Veteran:
                    difficulty = 3;
                    this.Level = rand.Next(10, 16);
                    break;
                case Difficulty.GodLike:
                    difficulty = 6;
                    this.Level = rand.Next(20, 24);
                    break;
            }

            this.Attack = rand.Next(1, 6) * difficulty;
            this.Defense = rand.Next(1, 6) * difficulty;

            this.Vitality = (this.Attack + this.Defense + 10) * difficulty;
            this.Health = this.Vitality * this.Level;
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

        /// <summary>
        /// Load the animations
        /// </summary>
        /// <param name="content">The content manager</param>
        public void Load(ContentManager content)
        {
            Random rand = new Random();

            switch(rand.Next(4))
            {
                case 0:
                    this.sprites.Load(content, "Spritesheet/Mob/Mob_Witch_Idle", "Spritesheet/Mob/Mob_Witch_Attack", 2, 4, 10);
                    break;
                case 1:
                    this.sprites.Load(content, "Spritesheet/Mob/Mob_BigWarrior_Idle", "Spritesheet/Mob/Mob_BigWarrior_Attack", 2, 4, 10);
                    break;
                case 2:
                    this.sprites.Load(content, "Spritesheet/Mob/Mob_MediumWarrior_Idle", "Spritesheet/Mob/Mob_MediumWarrior_Attack", 2, 4, 10);
                    break;
                case 3:
                    this.sprites.Load(content, "Spritesheet/Mob/Mob_SmallWarrior_Idle", "Spritesheet/Mob/Mob_SmallWarrior_Attack", 2, 4, 10);
                    break;
            }
        }

        /// <summary>
        /// Gives the amount of experience earned by killing the character
        /// </summary>
        /// <param name="nbInput">The number of input of the music</param>
        /// <param name="difficulty">The difficulty of the game</param>
        /// <returns>The amount of experience earned</returns>
        public override int giveXP(int nbInput, Difficulty difficulty)
        {
            int diff;
            switch(difficulty)
            {
                case Difficulty.Casual:
                    diff = 6;
                    break;
                case Difficulty.Veteran:
                    diff = 3;
                    break;
                default:
                    diff = 1;
                    break;
            }
            return ((int)Math.Floor(this.Vitality * this.Level / 2.5) + (nbInput / diff));
        }
    }
}
