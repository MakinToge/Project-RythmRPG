﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class StartMenu : Page{
        public Sprite MainImage { get; set; }
        public Sprite Start { get; set; }
        public Sprite Options { get; set; }
        public Sprite Achievements { get; set; }
        public Sprite Scores { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Start = new Sprite(3 * Game1.UnitX, 4 * Game1.UnitY, Game1.ButtonWidth, 2 * Game1.ButtonHeight);
            this.Options = new Sprite(3 * Game1.UnitX, 8 * Game1.UnitY, Game1.ButtonWidth, 2* Game1.ButtonHeight);
            this.Achievements = new Sprite(3 * Game1.UnitX, 13 * Game1.UnitY, Game1.ButtonWidth, Game1.ButtonHeight);
            this.Scores = new Sprite(3 * Game1.UnitX, 15 * Game1.UnitY, Game1.ButtonWidth, Game1.ButtonHeight);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "StartMenu/StartMenu");
            this.Start.LoadContent(content, "StartMenu/Start");
            this.Options.LoadContent(content, "StartMenu/Options");
            this.Achievements.LoadContent(content, "StartMenu/Achievements");
            this.Scores.LoadContent(content, "StartMenu/Scores");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X,currentMouseState.Y ,10,10);

                if (isOver(mouse, Options)) {
                    Game1.GameState = GameState.Options;
                }
                else if (isOver(mouse, Scores)) {

                }
                else if (isOver(mouse, Start)) {
                    Game1.GameState = GameState.GameMenu;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Start.Draw(spriteBatch, gameTime);
            this.Options.Draw(spriteBatch, gameTime);
            this.Achievements.Draw(spriteBatch, gameTime);
            this.Scores.Draw(spriteBatch, gameTime);
        }
    }
}
