using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class CharacterManagement : Page{
        //re
        public Sprite MainImage { get; set; }
        public Sprite Back { get; set; }
        public Sprite Modify { get; set; }
        public Characters Characters { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite XPNextLevel { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite HP { get; set; }
        public TextSprite Strength { get; set; }
        public TextSprite[] Skills { get; set; }
        public int SelectedCharacter { get; set; }
        public Sprite[] TabMedium { get; set; }
        public Sprite[] TabTank { get; set; }
        public Sprite[] TabDPS { get; set; }
        public Sprite[] TabCustom { get; set; }
        private int tabSelected;
	    public int TabSelected
	    {
		    get { return tabSelected;}
		    set { 
                tabSelected = value;
                this.SelectedCharacter = value;
            }
	    }
        public ModifyCharacter ModifyCharacter { get; set; }
	

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            this.TabMedium = new Sprite[2] {
                new Sprite(2 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(2 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabTank = new Sprite[2] {
                new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabDPS = new Sprite[2] {
                new Sprite(16 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(16 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabCustom = new Sprite[2] {
                new Sprite(23 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(23 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            

            this.Characters = Game1.Save.CharactersArray[Game1.Save.SelectedSave];
            this.SelectedCharacter = 0;
            //Character Data
            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new Sprite(12 * Game1.UnitX, 5 * Game1.UnitY, 9 * Game1.UnitX, 9 * Game1.UnitY);
            }
            this.Name = new TextSprite(15 * Game1.UnitX, 3.3f * Game1.UnitY, "Florizarre", Color.Black);
            this.Level = new TextSprite(6 * Game1.UnitX, 4.2f * Game1.UnitY, "1", Color.Black);
            this.Endurance = new TextSprite(7 * Game1.UnitX, 9.2f * Game1.UnitY, "10", Color.Black);
            this.HP = new TextSprite(5 * Game1.UnitX, 7.2f * Game1.UnitY, "50", Color.Black);
            this.Strength = new TextSprite(7 * Game1.UnitX, 8.2f * Game1.UnitY, "25", Color.Black);

            this.Modify = new Sprite(2 * Game1.UnitX, 12 * Game1.UnitY, 7 * Game1.UnitX, 2*Game1.UnitY);

            this.TabSelected = 0;
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "CharacterManagement/CharacterManagement");
            this.Back.LoadContent(content, "Options/Back");
            this.Modify.LoadContent(content, "CharacterManagement/ModifyCharacter");

            this.TabMedium[0].LoadContent(content, "CharacterManagement/Medium");
            this.TabMedium[1].LoadContent(content, "CharacterManagement/Selected/Medium");
            this.TabTank[0].LoadContent(content, "CharacterManagement/Tank");
            this.TabTank[1].LoadContent(content, "CharacterManagement/Selected/Tank");
            this.TabDPS[0].LoadContent(content, "CharacterManagement/DPS");
            this.TabDPS[1].LoadContent(content, "CharacterManagement/Selected/DPS");
            this.TabCustom[0].LoadContent(content, "CharacterManagement/Custom");
            this.TabCustom[1].LoadContent(content, "CharacterManagement/Selected/Custom");

            //Character Data
            for (int i = 0; i < this.Characters.CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + this.Characters.CharacterArray[i].Type.ToString().ToLower());
            }

            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                if (isOver(mouse, Back)) {
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, TabMedium[0])) {
                    this.TabSelected = 0;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.SelectedCharacter]);
                }
                else if (isOver(mouse, TabTank[0])) {
                    this.TabSelected = 1;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.SelectedCharacter]);
                }
                else if (isOver(mouse, TabDPS[0])) {
                    this.TabSelected = 2;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.SelectedCharacter]);
                }
                else if (isOver(mouse, TabCustom[0])) {
                    this.TabSelected = 3;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.SelectedCharacter]);
                }
                else if (isOver(mouse, Modify)) {
                    Game1.GameState = GameState.ModifyCharacter;
                    this.ModifyCharacter.LoadDataCharacter(this.Characters.CharacterArray[this.SelectedCharacter]);
                }
            }
        }

        

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);
            

            this.TabMedium[0].Draw(spriteBatch, gameTime);
            this.TabTank[0].Draw(spriteBatch, gameTime);
            this.TabDPS[0].Draw(spriteBatch, gameTime);
            this.TabCustom[0].Draw(spriteBatch, gameTime);

            int tab = this.TabSelected;
            if (tab == 0) {
                this.TabMedium[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 1) {
                this.TabTank[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 2) {
                this.TabDPS[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 3) {
                this.TabCustom[1].Draw(spriteBatch, gameTime);
                this.Modify.Draw(spriteBatch, gameTime);
            }

            //Character Data
            this.SpriteCharacters[this.SelectedCharacter].Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
        }

        public void LoadDataCharacter(Character character) {
            this.Name.Text = character.Name;
            this.Level.Text = character.Level.ToString();
            this.Endurance.Text = character.EndurancePoints.ToString();
            this.HP.Text = character.HealthPoints.ToString();
            this.Strength.Text = character.StrengthPoints.ToString();
        }
    }
}
