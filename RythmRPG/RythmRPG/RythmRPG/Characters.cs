using Microsoft.Xna.Framework;
using RythmRPG.CharacterStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Characters {
        public const int NB_MAX_CHARACTERS = 4;
        public PlayableCharacter[] CharacterArray { get; set; }
        public int SelectCharacter { get; set; }

        public Characters() {
            // Data Characters
            this.CharacterArray = new PlayableCharacter[NB_MAX_CHARACTERS];
            for (int i = 0; i < this.CharacterArray.Length; i++) {
                this.LoadDataCharacters(i);
            }
            /*{
                new PlayableCharacter()
                new Character("Florizarre", CharacterType.Medium,"medium", 1,50,25,10,0,0,0,0),
                new Character("Squirtle", CharacterType.Tank,"tank", 4,75,15,20,0,0,0,0),
                new Character("DPS", CharacterType.DPS,"dps", 4,75,15,20,0,0,0,0),
                new Character("Truc", CharacterType.Custom,"custom", 4,15,5,10,0,0,0,0)
            };*/
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Sprite[] spriteCharacters) {
            for (int i = 0; i < this.CharacterArray.Length; i++) {
                spriteCharacters[i].LoadContent(content, "Characters/" + this.CharacterArray[i].IdleSpriteName);
            }
        }

        public void LoadDataCharacters(int nbCharacter) {
            //Charger les données Données au hasard pour l'instant
            int level, defense, vitality, attack, combo, xp, statPoints, nbRestart, size;
            string idleSpriteName, attackingSpriteName, name;
            int[,] levelUpStats = new int[1, 2];
            Vector2 position;
            UniqueSkill ability;

            name = "Flori";
            level = 5;
            defense = 15;
            vitality = 20;
            attack = 15;
            ability = UniqueSkill.Berserker;
            levelUpStats = new int[1, 2];
            combo = 0;
            xp = 0;
            statPoints = 0;
            nbRestart = 0;
            idleSpriteName = "medium";
            attackingSpriteName = "";
            position = new Vector2(0, 0);
            size = 0;

            if(nbCharacter == 1) {
                name = "Carapuce";
                level = 7;
                defense = 35;
                vitality = 30;
                attack = 10;
                ability = UniqueSkill.GoldDigger;
                levelUpStats = new int[1, 2];
                combo = 0;
                xp = 0;
                statPoints = 0;
                nbRestart = 0;
                idleSpriteName = "tank";
                attackingSpriteName = "";
                position = new Vector2(0, 0);
                size = 0;
            }
            else if(nbCharacter == 2) {
                name = "Pika Pika";
                level = 15;
                defense = 25;
                vitality = 40;
                attack = 25;
                ability = UniqueSkill.FatalBlow;
                levelUpStats = new int[1, 2];
                combo = 0;
                xp = 0;
                statPoints = 0;
                nbRestart = 0;
                idleSpriteName = "dps";
                attackingSpriteName = "";
                position = new Vector2(0, 0);
                size = 0;
            }
            else if (nbCharacter == 3) {
                name = "Truc";
                level = 24;
                defense = 35;
                vitality = 30;
                attack = 35;
                ability = UniqueSkill.Experimentation;
                levelUpStats = new int[1, 2];
                combo = 0;
                xp = 0;
                statPoints = 0;
                nbRestart = 2;
                idleSpriteName = "custom";
                attackingSpriteName = "";
                position = new Vector2(0, 0);
                size = 0;
            }

            this.CharacterArray[nbCharacter] = new PlayableCharacter(level,vitality,attack,defense, ability, levelUpStats, combo, xp, statPoints, nbRestart, idleSpriteName,attackingSpriteName,position,size, name);
            this.CharacterArray[nbCharacter].gold = 500;
        }
    }
}
