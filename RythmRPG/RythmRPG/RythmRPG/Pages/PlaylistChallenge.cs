using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RythmRPG.Pages
{
    public class PlaylistChallenge : Page
    {
        /// <summary>
        /// Gets or sets the main image.
        /// </summary>
        /// <value>
        /// The main image.
        /// </value>
        public Sprite MainImage { get; set; }
        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public Sprite Back { get; set; }

        /// <summary>
        /// Gets or sets the casual.
        /// </summary>
        /// <value>
        /// The casual.
        /// </value>
        public Sprite[] Casual { get; set; }
        /// <summary>
        /// Gets or sets the veteran.
        /// </summary>
        /// <value>
        /// The veteran.
        /// </value>
        public Sprite[] Veteran { get; set; }
        /// <summary>
        /// Gets or sets the god like.
        /// </summary>
        /// <value>
        /// The god like.
        /// </value>
        public Sprite[] GodLike { get; set; }
        /// <summary>
        /// Gets or sets the selected difficulty.
        /// </summary>
        /// <value>
        /// The selected difficulty.
        /// </value>
        public int SelectedDifficulty { get; set; }
        /// <summary>
        /// Gets or sets the play.
        /// </summary>
        /// <value>
        /// The play.
        /// </value>
        public Sprite Play { get; set; }
        /// <summary>
        /// Gets or sets the not play.
        /// </summary>
        /// <value>
        /// The not play.
        /// </value>
        public Sprite NotPlay { get; set; }
        /// <summary>
        /// Gets or sets the choose music.
        /// </summary>
        /// <value>
        /// The choose music.
        /// </value>
        public Sprite ChooseMusic { get; set; }

        /// <summary>
        /// Gets or sets the classic.
        /// </summary>
        /// <value>
        /// The classic.
        /// </value>
        public Sprite[] Classic { get; set; }
        /// <summary>
        /// Gets or sets the hardcore.
        /// </summary>
        /// <value>
        /// The hardcore.
        /// </value>
        public Sprite[] Hardcore { get; set; }
        /// <summary>
        /// Gets or sets the selected mode.
        /// </summary>
        /// <value>
        /// The selected mode.
        /// </value>
        public int SelectedMode { get; set; }
        /// <summary>
        /// Gets or sets the music playing.
        /// </summary>
        /// <value>
        /// The music playing.
        /// </value>
        public MusicPlaying MusicPlaying { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);


            this.Casual = new Sprite[2] {
                new Sprite(Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.Veteran = new Sprite[2] {
                new Sprite(Game1.UnitX, 6 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 6 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.GodLike = new Sprite[2] {
                new Sprite(Game1.UnitX, 7 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 7 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };

            this.SelectedDifficulty = 0;

            this.Play = new Sprite(11 * Game1.UnitX, 14 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.NotPlay = new Sprite(11 * Game1.UnitX, 14 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.ChooseMusic = new Sprite(11 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.Classic = new Sprite[2] {
                new Sprite(Game1.UnitX, 12 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 12 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.Hardcore = new Sprite[2] {
                new Sprite(Game1.UnitX, 13 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 13 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.MainImage.LoadContent(content, "PlaylistChallenge/PlaylistChallenge");
            this.Back.LoadContent(content, "Options/Back");

            this.Casual[0].LoadContent(content, "SingleMusic/Casual");
            this.Casual[1].LoadContent(content, "SingleMusic/Selected/Casual");
            this.Veteran[0].LoadContent(content, "SingleMusic/Veteran");
            this.Veteran[1].LoadContent(content, "SingleMusic/Selected/Veteran");
            this.GodLike[0].LoadContent(content, "SingleMusic/GodLike");
            this.GodLike[1].LoadContent(content, "SingleMusic/Selected/GodLike");

            this.Play.LoadContent(content, "SingleMusic/Play!");
            this.NotPlay.LoadContent(content, "SingleMusic/Play!2");
            this.ChooseMusic.LoadContent(content, "SingleMusic/ChooseMusic");

            this.Classic[0].LoadContent(content, "PlaylistChallenge/Classic");
            this.Classic[1].LoadContent(content, "PlaylistChallenge/Selected/Classic");
            this.Hardcore[0].LoadContent(content, "PlaylistChallenge/Hardcore");
            this.Hardcore[1].LoadContent(content, "PlaylistChallenge/Selected/Hardcore");

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
            if (currentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && previousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                if (isOver(mouse, Back))
                {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, Casual[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 0;
                    Game1.Difficulty = Difficulty.Casual;
                }
                else if (isOver(mouse, Veteran[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 1;
                    Game1.Difficulty = Difficulty.Veteran;
                }
                else if (isOver(mouse, GodLike[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 2;
                    Game1.Difficulty = Difficulty.GodLike;
                }
                else if (isOver(mouse, Classic[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.SelectedMode = 0;
                }
                else if (isOver(mouse, Hardcore[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.SelectedMode = 1;
                }
                else if (isOver(mouse, ChooseMusic))
                {//Clique sur Choose Music
                    StartMenu.EffectClick.Play();

                    OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
                    open.Filter = "MP3 File (*.mp3)|*.mp3;";
                    if (open.ShowDialog() != DialogResult.OK) return;

                }
                else if (isOver(mouse, Play))
                {// Clique sur Play!
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Stop();
                    Game1.GameState = GameState.MusicPlaying;

                    //Charge le jeu
                    this.MusicPlaying.LoadGame();
                    this.MusicPlaying.LoadDataCharacter(Game1.characters.getSelectedCharacter());
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
            this.Back.Draw(spriteBatch, gameTime);

            this.Casual[0].Draw(spriteBatch, gameTime);
            this.Veteran[0].Draw(spriteBatch, gameTime);
            this.GodLike[0].Draw(spriteBatch, gameTime);

            if (this.SelectedDifficulty == 0)
            {
                this.Casual[1].Draw(spriteBatch, gameTime);
            }
            else if (this.SelectedDifficulty == 1)
            {
                this.Veteran[1].Draw(spriteBatch, gameTime);
            }
            else if (this.SelectedDifficulty == 2)
            {
                this.GodLike[1].Draw(spriteBatch, gameTime);
            }

            this.Classic[0].Draw(spriteBatch, gameTime);
            this.Hardcore[0].Draw(spriteBatch, gameTime);

            if (this.SelectedMode == 0)
            {
                this.Classic[1].Draw(spriteBatch, gameTime);
            }
            else if (SelectedMode == 1)
            {
                this.Hardcore[1].Draw(spriteBatch, gameTime);
            }

            this.ChooseMusic.Draw(spriteBatch, gameTime);
            this.Play.Draw(spriteBatch, gameTime);
            this.NotPlay.Draw(spriteBatch, gameTime);

            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            tmp.setPosition(new Vector2(23.5f * Game1.UnitX, 4 * Game1.UnitY));
            tmp.setScale(2);
            tmp.Draw(spriteBatch);
        }
    }
}
