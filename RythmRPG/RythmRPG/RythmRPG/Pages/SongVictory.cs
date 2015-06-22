using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class SongVictory : Page
    {
        /// <summary>
        /// Gets or sets the main image.
        /// </summary>
        /// <value>
        /// The main image.
        /// </value>
        public Sprite MainImage { get; set; }

        /// <summary>
        /// Gets or sets the game menu.
        /// </summary>
        /// <value>
        /// The game menu.
        /// </value>
        public Sprite GameMenu { get; set; }

        /// <summary>
        /// Gets or sets the restart.
        /// </summary>
        /// <value>
        /// The restart.
        /// </value>
        public Sprite Restart { get; set; }

        /// <summary>
        /// Gets or sets the next music.
        /// </summary>
        /// <value>
        /// The next music.
        /// </value>
        public Sprite NextMusic { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.GameMenu = new Sprite(10 * Game1.UnitX, 12 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
            this.Restart = new Sprite(10 * Game1.UnitX, 9 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
            this.NextMusic = new Sprite(10 * Game1.UnitX, 6 * Game1.UnitY, 12 * Game1.UnitX, 2 * Game1.UnitY);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.MainImage.LoadContent(content, "AfterGame/VictorySong");
            this.GameMenu.LoadContent(content, "Pause/GameMenu2");
            this.Restart.LoadContent(content, "Pause/Restart");
            this.NextMusic.LoadContent(content, "AfterGame/NextSong");
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
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                Game1.characters.getSelectedCharacter().prepareForMusic();
                if (isOver(mouse, NextMusic))
                {// Clique sur Next Song
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.MusicPlaying;
                }
                else if (isOver(mouse, GameMenu))
                {// Clique sur GameMenu
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Play();
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, Restart))
                {// Clique sur Restart
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.MusicPlaying;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Restart.Draw(spriteBatch, gameTime);
            this.NextMusic.Draw(spriteBatch, gameTime);
        }
    }
}
