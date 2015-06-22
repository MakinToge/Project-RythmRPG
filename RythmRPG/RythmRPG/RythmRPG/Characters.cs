using Microsoft.Xna.Framework;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    [Serializable]
    public class Characters
    {
        
        private const int NB_MAX_CHARACTERS = 4;
        public int NbCharacter
        {
            get
            {
                return NB_MAX_CHARACTERS;
            }
        }
        private PlayableCharacter[] characterArray;
        public int selectedCharacter { get; set; }

        public Characters()
        {
            this.selectedCharacter = 0;
            this.characterArray = new PlayableCharacter[NB_MAX_CHARACTERS];
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            for (int i = 0; i < NB_MAX_CHARACTERS; i++)
            {
                string name = this.characterArray[i].Name;
                this.characterArray[i].Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2, 4, 10);
            }
        }

        public void CreateDataCharacters()
        {
            characterArray[0] = new PlayableCharacter(1, 1, 1, 1, UniqueSkill.Survivor, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, 12, 0, 0, 0, 0, Vector2.Zero, 1, "Barbarian");
            characterArray[1] = new PlayableCharacter(1, 1, 1, 1, UniqueSkill.Templar, new int[,] { { 2, 0, 1 }, { 1, 0, 2 }, { 1, 1, 1 } }, 12, 0, 0, 0, 0, Vector2.Zero, 1, "Knight");
            characterArray[2] = new PlayableCharacter(1, 1, 1, 1, UniqueSkill.FatalBlow, new int[,] { { 0, 3, 0 }, { 1, 2, 0 }, { 0, 2, 1 } }, 12, 0, 0, 0, 0, Vector2.Zero, 1, "Ninja");
            characterArray[3] = new PlayableCharacter(1, 1, 1, 1, UniqueSkill.GoldDigger, new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }, 12, 0, 0, 0, 0, Vector2.Zero, 1, "Magus");
        }

        public void LoadCharacters(PlayableCharacter[] characters)
        {
            this.characterArray = characters;
        }

        public PlayableCharacter getSelectedCharacter()
        {
            return this.characterArray[this.selectedCharacter];
        }
    }
}
