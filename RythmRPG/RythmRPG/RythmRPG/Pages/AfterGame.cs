﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class AfterGame : Page{
        public Sprite MainImage { get; set; }
        public string AssetNameMainImage { get; set; }
        public string Message { get; set; }
        public TextSprite Xp { get; set; }
        public Sprite PlayAgain { get; set; }
        public Sprite GameMenu { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
        public TextSprite Type { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite Vitality { get; set; }
        public TextSprite HP { get; set; }
        public TextSprite Strength { get; set; }

        public AfterGame(string assetNameMainImage, string message) {
            this.AssetNameMainImage = assetNameMainImage;
            this.Message = message;
        }
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.PlayAgain = new Sprite(22 * Game1.UnitX, 6 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.GameMenu = new Sprite(22 * Game1.UnitX, 9 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.Xp = new TextSprite(22 * Game1.UnitX, 3.9f * Game1.UnitY, this.Message, Color.Black);

            //Character Data
            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new Sprite(3 * Game1.UnitX, 5 * Game1.UnitY, 9 * Game1.UnitX, 9 * Game1.UnitY);
            }
            this.Type = new TextSprite(17 * Game1.UnitX, 3.9f * Game1.UnitY, "", Color.Black);
            this.Name = new TextSprite(17 * Game1.UnitX, 4.9f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(17 * Game1.UnitX, 5.9f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(16 * Game1.UnitX, 6.9f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(18 * Game1.UnitX, 7.9f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(18 * Game1.UnitX, 8.9f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(19 * Game1.UnitX, 9.9f * Game1.UnitY, "", Color.Black);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "AfterGame/" + this.AssetNameMainImage);
            this.PlayAgain.LoadContent(content, "AfterGame/PlayAgain");
            this.GameMenu.LoadContent(content, "AfterGame/GameMenu");

            this.Xp.LoadContent(content, "Arial16");
            //Character Data
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].Type.ToString().ToLower());
            }

            this.Type.LoadContent(content, "Arial16");
            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Vitality.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                if (isOver(mouse, PlayAgain)) { //Clique sur Play Again

                }
                else if (isOver(mouse, GameMenu)) {
                    Game1.GameState = GameState.GameMenu;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.PlayAgain.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Xp.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].Draw(spriteBatch, gameTime);
            this.Type.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);
        }

        public void LoadDataCharacter(Character character) {
            this.Type.Text = character.Type.ToString();
            this.Name.Text = character.Name;
            if (character.ReachLevelMax == 0) {
                this.Level.Text = character.Level.ToString();
            }
            else {
                this.Level.Text = string.Format("{0} ({1})", character.Level, character.ReachLevelMax);
            }
            this.Endurance.Text = character.EndurancePoints.ToString();
            this.HP.Text = character.HealthPoints.ToString();
            this.Strength.Text = character.StrengthPoints.ToString();
            this.Vitality.Text = character.Vitality.ToString();
        }
    }
}