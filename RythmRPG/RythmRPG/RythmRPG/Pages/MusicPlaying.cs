using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class MusicPlaying : Page{
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public Sprite MainImage { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite HP { get; set; }
        public int HPStart { get; set; }
        public TextSprite Strength { get; set; }
        public Difficulty Difficulty { get; set; }
        public TextSprite[] Skills { get; set; }
        public TextSprite Ability { get; set; }
        

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);

            //Character Data
            
            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new Sprite(17 * Game1.UnitX, 2 * Game1.UnitY, 5 * Game1.UnitX, 5 * Game1.UnitY);
            }
            this.Name = new TextSprite(17 * Game1.UnitX, 1f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(26 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(26 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(25 * Game1.UnitX, 3.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(26 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);

            this.Ability = new TextSprite(28 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "MusicPlaying/MusicPlaying");

            //Character Data
            
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].Type.ToString().ToLower());
            }

            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Ability.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            /*
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
            }
            */
            if (currentKeyboardState.IsKeyDown(Keys.Escape)) {
                Game1.GameState = GameState.Pause;
            }
        }

        public override void Update(GameTime gametime) {
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Ability.Draw(spriteBatch, gameTime);
        }

        public void LoadDataCharacter(Character character) {
            this.HPStart = character.HealthPoints;
            this.Name.Text = character.Name;
            if (character.ReachLevelMax == 0) {
                this.Level.Text = character.Level.ToString();
            }
            else {
                this.Level.Text = string.Format("{0} ({1})", character.Level, character.ReachLevelMax);
            }
            
            this.Endurance.Text = character.EndurancePoints.ToString();
            this.HP.Text = character.HealthPoints.ToString() + " / " + this.HPStart.ToString();
            this.Strength.Text = character.StrengthPoints.ToString();
            this.Ability.Text = character.Abilility;
        }

        public void LoadGame() {
            this.isLoaded = true;
        }
    }
}
