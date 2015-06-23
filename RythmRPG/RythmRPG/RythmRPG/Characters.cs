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
        /// <summary>
        /// The number max characters
        /// </summary>
        private const int NB_MAX_CHARACTERS = 4;

        /// <summary>
        /// Gets the number of  character.
        /// </summary>
        /// <value>
        /// The number of character.
        /// </value>
        public int NbCharacter
        {
            get
            {
                return NB_MAX_CHARACTERS;
            }
        }

        /// <summary>
        /// The character array
        /// </summary>
        public PlayableCharacter[] characterArray;

        /// <summary>
        /// Gets or sets the selected character.
        /// </summary>
        /// <value>
        /// The selected character.
        /// </value>
        public int selectedCharacter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Characters"/> class.
        /// </summary>
        public Characters()
        {
            this.selectedCharacter = 0;
            this.characterArray = new PlayableCharacter[NB_MAX_CHARACTERS];
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            for (int i = 0; i < NB_MAX_CHARACTERS; i++)
            {
                string name = this.characterArray[i].Name;
                this.characterArray[i].Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2, 4, 10);
            }
        }

        /// <summary>
        /// Creates the data characters.
        /// </summary>
        public void CreateDataCharacters()
        {
            characterArray[0] = new PlayableCharacter(25, 26,26,26, UniqueSkill.Survivor, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, 12, 300000, 0, 4, 0, Vector2.Zero, 1, "Barbarian");
            characterArray[1] = new PlayableCharacter(14, 19, 5, 18, UniqueSkill.Templar, new int[,] { { 2, 0, 1 }, { 1, 0, 2 }, { 1, 1, 1 } }, 12, 91547, 0, 2, 0, Vector2.Zero, 1, "Knight");
            characterArray[2] = new PlayableCharacter(1, 1, 1, 1, UniqueSkill.FatalBlow, new int[,] { { 0, 3, 0 }, { 1, 2, 0 }, { 0, 2, 1 } }, 12, 0, 0, 0, 0, Vector2.Zero, 1, "Ninja");
            characterArray[3] = new PlayableCharacter(19, 1, 1, 1, UniqueSkill.GoldDigger, new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }, 12, 190397, 54, 0, 1337, Vector2.Zero, 1, "Magus");
        }

        /// <summary>
        /// Loads the characters.
        /// </summary>
        /// <param name="characters">The characters.</param>
        public void LoadCharacters(PlayableCharacter[] characters)
        {
            this.characterArray = characters;
        }

        /// <summary>
        /// Gets the selected character.
        /// </summary>
        /// <returns></returns>
        public PlayableCharacter getSelectedCharacter()
        {
            return this.characterArray[this.selectedCharacter];
        }
    }
}
