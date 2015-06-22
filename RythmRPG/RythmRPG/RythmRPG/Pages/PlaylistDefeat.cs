using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class PlaylistDefeat : AfterGame
    {
        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        public Sprite Skip { get; set; }
        /// <summary>
        /// Gets or sets the retry.
        /// </summary>
        /// <value>
        /// The retry.
        /// </value>
        public Sprite Retry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistDefeat"/> class.
        /// </summary>
        /// <param name="assetNameMainImage">The asset name main image.</param>
        /// <param name="message">The message.</param>
        public PlaylistDefeat(string assetNameMainImage, string message)
            : base(assetNameMainImage, message)
        {
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            this.Skip = new Sprite(22 * Game1.UnitX, 12 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.Retry = new Sprite(22 * Game1.UnitX, 6 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            this.Skip.LoadContent(content, "AfterGame/SkipMusic");
            this.Retry.LoadContent(content, "AfterGame/RetryMusic");
        }
        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState)
        {
            base.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
        }

        /// <summary>
        /// Draws the specified sprite.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Xp.Draw(spriteBatch, gameTime);
            this.Skip.Draw(spriteBatch, gameTime);
            this.Retry.Draw(spriteBatch, gameTime);

            //Character Data
            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            tmp.setPosition(new Vector2(4.5f * Game1.UnitX, 5.5f * Game1.UnitY));
            tmp.setScale(2);
            tmp.Draw(spriteBatch);

            this.Type.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);
        }
    }
}
