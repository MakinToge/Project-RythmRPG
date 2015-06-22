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
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page() {

        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize() {
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public virtual void LoadContent(ContentManager content) {
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent() {
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
        }

        /// <summary>
        /// Updates the specified gametime.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        public virtual void Update(GameTime gametime) {
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
        }
        /// <summary>
        /// Determines whether the specified mouse is over.
        /// </summary>
        /// <param name="mouse">The mouse.</param>
        /// <param name="sprite">The sprite.</param>
        /// <returns></returns>
        public bool isOver(Rectangle mouse, Sprite sprite) {
            if (mouse.Intersects(sprite.Rectangle)) {
                return true;
            }
            return false;
        }
    }
}
