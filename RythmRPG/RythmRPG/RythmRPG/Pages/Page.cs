using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class Page {
        public Page() {

        }
        public virtual void Initialize() {
        }

        public virtual void LoadContent(ContentManager content) {
        }

        public virtual void UnloadContent() {
        }
  
        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
        }

        public virtual void Update(GameTime gametime) {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
        }
        public bool isOver(Rectangle mouse, Sprite sprite) {
            if (mouse.Intersects(sprite.Rectangle)) {
                return true;
            }
            return false;
        }
    }
}
