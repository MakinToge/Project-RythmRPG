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
    public class Sprite
    {
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public Vector2 Direction { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float Speed { get; set; }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <value>
        /// The rectangle.
        /// </value>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Convert.ToInt32(this.Size.X), Convert.ToInt32(this.Size.Y));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        public Sprite()
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Sprite(Vector2 position)
            : this(position, Vector2.Zero, Vector2.Zero, 0)
        {
            this.Position = position;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Sprite(float positionX, float positionY, int width, int height)
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0)
        {
            this.Position = new Vector2(positionX, positionY);
            this.Size = new Vector2(width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        public Sprite(Vector2 position, Vector2 size, Vector2 direction, float speed)
        {
            this.Position = position;
            this.Size = size;
            this.Direction = direction;
            this.Speed = speed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="directionX">The direction x.</param>
        /// <param name="directionY">The direction y.</param>
        /// <param name="speed">The speed.</param>
        public Sprite(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed)
        {
            this.Position = new Vector2(positionX, positionY);
            this.Size = new Vector2(width, height);
            this.Direction = Vector2.Normalize(new Vector2(directionX, directionY));
            this.Speed = speed;
        }

        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        public void SetPosition(float X, float Y)
        {
            this.Position = new Vector2(X, Y);
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetSize(float width, float height)
        {
            this.Size = new Vector2(width, height);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            this.Position = Vector2.Zero;
            this.Direction = Vector2.Zero;
            this.Speed = 0;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="assetName">Name of the asset.</param>
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            this.Texture = content.Load<Texture2D>(assetName);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            this.Position += this.Direction * this.Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState)
        {

        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Begin();

            Vector2 spriteScale = new Vector2(this.Size.X / this.Texture.Width, this.Size.Y / this.Texture.Height);
            spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
