using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Characters {
        public const int NB_MAX_CHARACTERS = 4;
        public Character[] CharacterArray { get; set; }
        public int SelectCharacter { get; set; }

        public Characters() {
            // Data Characters
            this.CharacterArray = new Character[NB_MAX_CHARACTERS]{
                new Character("Florizarre", CharacterType.Medium,"medium", 1,50,25,10,0,0,0,0),
                new Character("Squirtle", CharacterType.Tank,"tank", 4,75,15,20,0,0,0,0),
                new Character("DPS", CharacterType.DPS,"dps", 4,75,15,20,0,0,0,0),
                new Character("Truc", CharacterType.Custom,"custom", 4,15,5,10,0,0,0,0)
            };
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Sprite[] spriteCharacters) {
            for (int i = 0; i < this.CharacterArray.Length; i++) {
                spriteCharacters[i].LoadContent(content, "Characters/" + this.CharacterArray[i].Type.ToString().ToLower());
            }
        }
    }
}
