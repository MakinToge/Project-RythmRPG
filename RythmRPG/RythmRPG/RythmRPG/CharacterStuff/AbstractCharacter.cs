using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    public class AbstractCharacter
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Vitality { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public List<Skills> skills { get; set; }

        public CharacterSprites sprites { get; set; }

        public AbstractCharacter(int level, int vitality, int attack, int defense, string idleSpriteName, string attackingSpriteName, Vector2 position, float scale, bool mirror, string name)
        {
            this.Level = level;
            this.Vitality = vitality;
            this.Attack = attack;
            this.Defense = defense;
            this.Name = name;
            this.Health = this.Level * this.Vitality;

            this.skills = new List<Skills>();
            this.sprites = new CharacterSprites(position, 0, scale, 0, mirror);
        }

        public void Load(ContentManager content, string idle, string attacking, int frameCount, int framesPerSec)
        {
            this.sprites.Load(content, idle, attacking, frameCount, framesPerSec);
        }

        public void Draw(SpriteBatch batch)
        {
            this.sprites.DrawFrame(batch);
        }

        public virtual void levelUp()
        {
            this.Level++;
        }

        public void addVitality(int nbPoints)
        {
            this.Vitality += nbPoints;
        }

        public void addAttack(int nbPoints)
        {
            this.Attack += nbPoints;
        }

        public void addDefense(int nbPoints)
        {
            this.Defense += nbPoints;
        }

        public bool isDead()
        {
            return this.Health <= 0;
        }

        public void takeDamage(int damage)
        {
            this.Health -= damage;
        }

        public virtual void attackCharacter(AbstractCharacter character)
        {
            this.sprites.isAttacking = true;
        }
    }
}
