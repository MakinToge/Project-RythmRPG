using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    abstract class AbstractCharacter
    {
        public int level { get; set; }

        public int health { get; set; }
        public int vitality { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }

        public List<Skills> skills { get; set; }

        public CharacterSprites sprites { get; set; }

        public AbstractCharacter(int level, int vitality, int attack, int defense, string idleSpriteName, string attackingSpriteName, Vector2 position, float scale, bool mirror)
        {
            this.level = level;
            this.vitality = vitality;
            this.attack = attack;
            this.defense = defense;

            this.health = this.level * this.vitality;

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
            this.level++;
        }

        public void addVitality(int nbPoints)
        {
            this.vitality += nbPoints;
        }

        public void addAttack(int nbPoints)
        {
            this.attack += nbPoints;
        }

        public void addDefense(int nbPoints)
        {
            this.defense += nbPoints;
        }

        public bool isDead()
        {
            return this.health <= 0;
        }

        public void takeDamage(int damage)
        {
            this.health -= damage;
        }

        public virtual void attackCharacter(AbstractCharacter character)
        {
            this.sprites.isAttacking = true;
        }
    }
}
