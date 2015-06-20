using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    abstract class Character
    {
        public int level { get; set; }

        public int health { get; set; }
        public int vitality { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }

        public List<Skills> skills { get; set; }

        public CharacterSprites sprites { get; set; }

        public Character(int level, int vitality, int attack, int defense,
            string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
        {
            this.level = level;
            this.vitality = vitality;
            this.attack = attack;
            this.defense = defense;

            this.health = this.level * this.vitality;

            this.skills = new List<Skills>();
            this.sprites = new CharacterSprites(idleSpriteName, attackingSpriteName, position, size);
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

        public abstract void attackCharacter(Character character);
    }
}
