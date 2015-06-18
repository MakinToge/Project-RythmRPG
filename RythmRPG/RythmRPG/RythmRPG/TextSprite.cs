using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class TextSprite {
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

        public TextSprite()
            : this(Vector2.Zero, "", Color.White) {
        }
        public TextSprite(Vector2 position, string text, Color color) {
            this.Position = position;
            this.Text = text;
            this.Color = color;
        }
        public TextSprite(float positionX, float positionY, string text, Color color) {
            this.Position = new Vector2(positionX, positionY);
            this.Text = text;
            this.Color = color;
        }
        public virtual void LoadContent(ContentManager content, string assetName) {
            this.Font = content.Load<SpriteFont>(assetName);
        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

            spriteBatch.Begin();
            spriteBatch.DrawString(this.Font, this.Text, this.Position, this.Color);
            spriteBatch.End();
        }
    }
}
