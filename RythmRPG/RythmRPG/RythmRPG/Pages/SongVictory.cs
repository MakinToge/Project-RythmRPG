using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class SongVictory : Page{
        public Sprite MainImage { get; set; }
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "SingleMusic/SingleMusic");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
                /*
                if (isOver(mouse, Back)) {
                    Game1.GameState = GameState.GameMenu;
                }*/
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
        }
    }
}
