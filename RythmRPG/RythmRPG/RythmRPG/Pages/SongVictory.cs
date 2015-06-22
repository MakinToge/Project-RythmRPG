using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class SongVictory : Page{
        public Sprite MainImage { get; set; }
        public Sprite GameMenu { get; set; }
        public Sprite Restart { get; set; }
        public Sprite NextMusic { get; set; }
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.GameMenu = new Sprite(10 * Game1.UnitX, 12 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
            this.Restart = new Sprite(10 * Game1.UnitX, 9 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
            this.NextMusic = new Sprite(10 * Game1.UnitX, 6 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "AfterGame/VictorySong");
            this.GameMenu.LoadContent(content, "Pause/GameMenu2");
            this.Restart.LoadContent(content, "Pause/Restart");
            this.NextMusic.LoadContent(content, "AfterGame/NextSong");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                Game1.characters.getSelectedCharacter().prepareForMusic();
                if (isOver(mouse, NextMusic)) {// Clique sur Next Song
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.MusicPlaying;
                }
                else if (isOver(mouse, GameMenu)) {// Clique sur GameMenu
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Play();
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, Restart)) {// Clique sur Restart
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.MusicPlaying;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Restart.Draw(spriteBatch, gameTime);
            this.NextMusic.Draw(spriteBatch, gameTime);
        }
    }
}
