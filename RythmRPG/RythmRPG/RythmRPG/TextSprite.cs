using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RythmRPG
{
    public class TextSprite
    {
        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        public SpriteFont Font { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSprite"/> class.
        /// </summary>
        public TextSprite()
            : this(Vector2.Zero, "", Color.White)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSprite"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        public TextSprite(Vector2 position, string text, Color color)
        {
            this.Position = position;
            this.Text = text;
            this.Color = color;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSprite"/> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        public TextSprite(float positionX, float positionY, string text, Color color)
        {
            this.Position = new Vector2(positionX, positionY);
            this.Text = text;
            this.Color = color;
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="assetName">Name of the asset.</param>
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            this.Font = content.Load<SpriteFont>(assetName);
        }
        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.DrawString(this.Font, this.Text, this.Position, this.Color);
            spriteBatch.End();
        }
        /// <summary>
        /// Determines whether the specified mouse is over.
        /// </summary>
        /// <param name="mouse">The mouse.</param>
        /// <returns></returns>
        public bool isOver(Microsoft.Xna.Framework.Input.MouseState mouse)
        {
            Vector2 size = this.Font.MeasureString(this.Text);
            if (mouse.X >= this.Position.X && mouse.Y >= this.Position.Y && mouse.X <= this.Position.X + size.X && mouse.Y <= this.Position.Y + size.Y)
            {
                return true;
            }
            return false;
        }
    }
}
