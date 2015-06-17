using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Sprite {

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Direction { get; set; }
 
        public Vector2 Size { get; set; }

        public float Speed { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Convert.ToInt32(this.Size.X), Convert.ToInt32(this.Size.Y));
            }
        }

        public Sprite()
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0) {
        }

        public Sprite(Vector2 position)
            : this(position, Vector2.Zero, Vector2.Zero, 0) {
            this.Position = position;
        }

        public Sprite(float positionX, float positionY, int width, int height)
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0) {
                this.Position = new Vector2(positionX, positionY);
                this.Size = new Vector2(width, height);
        }

        public Sprite(Vector2 position, Vector2 size, Vector2 direction, float speed) {
            this.Position = position;
            this.Size = size;
            this.Direction = direction;
            this.Speed = speed;
        }

        public Sprite(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed) {
            this.Position = new Vector2(positionX, positionY);
            this.Size = new Vector2(width, height);
            this.Direction = Vector2.Normalize(new Vector2(directionX, directionY));
            this.Speed = speed;
        }

        public void SetPosition(float X, float Y) {
            this.Position = new Vector2(X, Y);
        }

        public void SetSize(float width, float height) {
            this.Size = new Vector2(width, height);
        }

        public virtual void Initialize() {
            this.Position = Vector2.Zero;
            this.Direction = Vector2.Zero;
            this.Speed = 0;
        }
 
        public virtual void LoadContent(ContentManager content, string assetName) {
            this.Texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime) {
            this.Position += this.Direction * this.Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

            spriteBatch.Begin();

            Vector2 spriteScale = new Vector2(this.Size.X / this.Texture.Width, this.Size.Y / this.Texture.Height);
            spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
