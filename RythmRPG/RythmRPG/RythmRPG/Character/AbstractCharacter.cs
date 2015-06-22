using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// The base of all characters in game, hero mobs and bosses
    /// </summary>
    [Serializable]
    public class AbstractCharacter
    {
        /// <summary>
        /// The level of the character
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The name of the character
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The health of the character, compute as follow : vitality * health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// The vitality of the character, impact the max health
        /// </summary>
        private int vitality;

        public int Vitality {
            get { return vitality; }
            set { vitality = value;
            this.Health = this.Level * this.Vitality;
            }
        }
        
        /// <summary>
        /// The attack of the character, impact the amount of damage the character deals
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// The defense of the character, impact the amount of damage the character receives
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// All the skills that the character has
        /// </summary>
        public List<Skills> skills { get; set; }

        /// <summary>
        /// The animations of the character
        /// </summary>
        public CharacterSprites sprites { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level">The level</param>
        /// <param name="vitality">The vitality</param>
        /// <param name="attack">The attack</param>
        /// <param name="defense">The defense</param>
        /// <param name="position">The position of the character on screen</param>
        /// <param name="scale">The scale of the character</param>
        /// <param name="name">The name of the character</param>
        public AbstractCharacter(int level, int vitality, int attack, int defense, Vector2 position, float scale, string name)
        {
            this.Level = level;
            this.Vitality = vitality;
            this.Attack = attack;
            this.Defense = defense;
            this.Name = name;
            this.Health = this.Level * this.Vitality;

            this.skills = new List<Skills>();
            this.sprites = new CharacterSprites(position, 0, scale, 0);
        }

        /// <summary>
        /// Loads the animations of the character
        /// </summary>
        /// <param name="content">The content manager</param>
        /// <param name="idle">The name of the idle animation</param>
        /// <param name="attacking">The name of the attacking animation</param>
        /// <param name="frameLineCount">Number of line in the spritesheet</param>
        /// <param name="frameColCount">Number of frame per line in the spritesheet</param>
        /// <param name="framesPerSec">Number of frames per seconde</param>
        public void Load(ContentManager content, string idle, string attacking, int frameLineCount, int frameColCount, int framesPerSec)
        {
            this.sprites.Load(content, idle, attacking, frameLineCount, frameColCount, framesPerSec);
        }

        /// <summary>
        /// Draw the animation
        /// </summary>
        /// <param name="batch">The spritebatch</param>
        public void Draw(SpriteBatch batch)
        {
            this.sprites.DrawFrame(batch);
        }

        /// <summary>
        /// Add 1 to the current level
        /// </summary>
        public virtual void levelUp()
        {
            this.Level++;
        }

        /// <summary>
        /// Add the given value to the vitality
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public virtual void addVitality(int nbPoints)
        {
            this.Vitality += nbPoints;
        }

        /// <summary>
        /// Add the given value to the attack
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public virtual void addAttack(int nbPoints)
        {
            this.Attack += nbPoints;
        }

        /// <summary>
        /// Add the given value to the defense
        /// </summary>
        /// <param name="nbPoints">Number of points to add</param>
        public virtual void addDefense(int nbPoints)
        {
            this.Defense += nbPoints;
        }

        /// <summary>
        /// Return true if the character is dead, false otherwise
        /// </summary>
        /// <returns>Return true if the character is dead, false otherwise</returns>
        public bool isDead()
        {
            return this.Health <= 0;
        }
        
        /// <summary>
        /// Remove health from the character
        /// </summary>
        /// <param name="damage">Number of health points to remove</param>
        public void takeDamage(int damage)
        {
            this.Health -= damage;
        }

        /// <summary>
        /// Attack another character
        /// </summary>
        /// <param name="character">The character to attack</param>
        public virtual void attackCharacter(AbstractCharacter character)
        {
            this.sprites.IsAttacking = true;
        }

        public void setPosition(Vector2 position)
        {
            this.sprites.position = position;
        }

        public void setScale(float scale)
        {
            this.sprites.scale = scale;
        }

        public void UpdateFrame(float elapsed)
        {
            this.sprites.UpdateFrame(elapsed);
        }
    }
}
