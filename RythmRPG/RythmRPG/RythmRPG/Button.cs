using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Button {
        public Sprite Basic { get; set; }
        public Sprite Pressed { get; set; }
        public Sprite Over { get; set; }

        public bool isPressed { get; set; }
        public bool isHover { get; set; }

        public Button(float positionX, float positionY, int width, int height) {
            this.Basic = new Sprite(positionX, positionY, width, height);
        }

        public virtual void LoadContent(ContentManager content, string assetNameBasic, string assetNamePressed, string assetNameHover) {
            this.Basic.Texture = content.Load<Texture2D>(assetNameBasic);
            this.Pressed.Texture = content.Load<Texture2D>(assetNamePressed);
            this.Over.Texture = content.Load<Texture2D>(assetNameHover);
        }

        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

            spriteBatch.Begin();

            Vector2 spriteScale = new Vector2(this.Basic.Size.X / this.Basic.Texture.Width, this.Basic.Size.Y / this.Basic.Texture.Height);

            if (isPressed) {
                spriteBatch.Draw(this.Pressed.Texture, this.Pressed.Position, null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
            }
            else if (isHover) {
                spriteBatch.Draw(this.Pressed.Texture, this.Pressed.Position, null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
            }
            else {
                spriteBatch.Draw(this.Basic.Texture, this.Basic.Position, null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
            }
            
            spriteBatch.End();
        }

        public bool isClicked(Rectangle mouse, Sprite sprite) {
            if (mouse.Intersects(sprite.Rectangle)) {
                return true;
            }
            return false;
        }
    }
}
