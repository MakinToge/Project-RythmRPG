﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class GameMenu : Page{
        
        public Sprite MainImage { get; set; }
        public Sprite Back { get; set; }
        public Sprite  CharacterManagement { get; set; }
        public Sprite PlaylistChallenge { get; set; }
        public Sprite SingleMusic { get; set; }
        public Sprite LeftCharacter { get; set; }
        public Sprite RightCharacter { get; set; }
        public Characters Characters { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
        public TextSprite Type { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite HP { get; set; }
        public TextSprite Strength { get; set; }
        public TextSprite[] Skills { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);
            this.SingleMusic = new Sprite(3 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.PlaylistChallenge = new Sprite(3 * Game1.UnitX, 7 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.CharacterManagement = new Sprite(3 * Game1.UnitX, 10 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.LeftCharacter = new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightCharacter = new Sprite(22 * Game1.UnitX, 2 * Game1.UnitY, Game1.UnitX, Game1.UnitY);

            this.Characters = Game1.Save.CharactersArray[Game1.Save.SelectedSave];
            this.Characters.SelectCharacter = 0;
            //Character Data
            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters [i] = new Sprite(13 * Game1.UnitX, 5 * Game1.UnitY, 10 * Game1.UnitX, 10 * Game1.UnitY);
			}
            this.Type = new TextSprite(27 * Game1.UnitX, 3.2f * Game1.UnitY,"Medium", Color.Black);
            this.Name = new TextSprite(27 * Game1.UnitX, 4.2f * Game1.UnitY, "Florizarre", Color.Black);
            this.Level = new TextSprite(28 * Game1.UnitX, 5.2f * Game1.UnitY, "1", Color.Black);
            this.Endurance = new TextSprite(28 * Game1.UnitX, 6.2f * Game1.UnitY, "10", Color.Black);
            this.HP = new TextSprite(26 * Game1.UnitX, 7.2f * Game1.UnitY, "50", Color.Black);
            this.Strength = new TextSprite(28 * Game1.UnitX, 8.2f * Game1.UnitY, "25", Color.Black);

        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "GameMenu/GameMenu");
            this.Back.LoadContent(content, "Options/Back");
            this.SingleMusic.LoadContent(content, "GameMenu/SingleMusic");
            this.PlaylistChallenge.LoadContent(content, "GameMenu/PlaylistChallenge");
            this.CharacterManagement.LoadContent(content, "GameMenu/CharactersManagement");

            this.LeftCharacter.LoadContent(content, "Options/ArrowLeft");
            this.RightCharacter.LoadContent(content, "Options/ArrowRight");

            //Character Data
            for (int i = 0; i < this.Characters.CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + this.Characters.CharacterArray[i].Type.ToString().ToLower());
            }
            
            this.Type.LoadContent(content, "Arial16");
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
                    Game1.GameState = GameState.StartMenu;
                }
                else if (isOver(mouse, CharacterManagement)) {
                    Game1.GameState = GameState.CharacterManagement;
                }
                else if (isOver(mouse, LeftCharacter) && this.Characters.SelectCharacter > 0) {
                    this.Characters.SelectCharacter -= 1;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.Characters.SelectCharacter]);
                }
                else if (isOver(mouse, RightCharacter) && this.Characters.SelectCharacter < Characters.NB_MAX_CHARACTERS - 1) {
                    this.Characters.SelectCharacter += 1;
                    this.LoadDataCharacter(this.Characters.CharacterArray[this.Characters.SelectCharacter]);
                }
                else if (isOver(mouse, SingleMusic)) {
                    Game1.GameState = GameState.SingleMusic;
                }
                else if (isOver(mouse, PlaylistChallenge)) {
                    Game1.GameState = GameState.PlaylistChallenge;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);
            this.SingleMusic.Draw(spriteBatch, gameTime);
            this.PlaylistChallenge.Draw(spriteBatch, gameTime);
            this.CharacterManagement.Draw(spriteBatch, gameTime);

            this.LeftCharacter.Draw(spriteBatch, gameTime);
            this.RightCharacter.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[this.Characters.SelectCharacter].Draw(spriteBatch, gameTime);
            this.Type.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
        }

        public void LoadDataCharacter(Character character){
            this.Type.Text = character.Type.ToString();
            this.Name.Text = character.Name;
            this.Level.Text = character.Level.ToString();
            this.Endurance.Text = character.EndurancePoints.ToString();
            this.HP.Text = character.HealthPoints.ToString();
            this.Strength.Text = character.StrengthPoints.ToString();
        }
    }
}
