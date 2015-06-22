using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class StartMenu : Page{
        /// <summary>
        /// Gets or sets the main image.
        /// </summary>
        /// <value>
        /// The main image.
        /// </value>
        public Sprite MainImage { get; set; }
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public Sprite Start { get; set; }
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public Sprite Options { get; set; }
        /// <summary>
        /// Gets or sets the save sprites.
        /// </summary>
        /// <value>
        /// The save sprites.
        /// </value>
        public Sprite[] SaveSprites { get; set; }
        /// <summary>
        /// Gets or sets the left save.
        /// </summary>
        /// <value>
        /// The left save.
        /// </value>
        public Sprite LeftSave { get; set; }
        /// <summary>
        /// Gets or sets the right save.
        /// </summary>
        /// <value>
        /// The right save.
        /// </value>
        public Sprite RightSave { get; set; }

        /// <summary>
        /// Gets or sets the exit button.
        /// </summary>
        /// <value>
        /// The exit button.
        /// </value>
        public Sprite ExitButton { get; set; }
        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public CharacterSprites Character { get; set; }

        /// <summary>
        /// The effect volume maximum
        /// </summary>
        public const float EFFECT_VOLUME_MAX = 1f;

        /// <summary>
        /// The effect volume minimum
        /// </summary>
        public const float EFFECT_VOLUME_MIN = 0f;

        /// <summary>
        /// The volume on
        /// </summary>
        public const float VOLUME_ON = 0.5f;

        /// <summary>
        /// The volume off
        /// </summary>
        public const float VOLUME_OFF = 0f;

        /// <summary>
        /// The click
        /// </summary>
        public SoundEffect Click;

        /// <summary>
        /// The effect click
        /// </summary>
        public static SoundEffectInstance EffectClick;

        /// <summary>
        /// The back sound effect
        /// </summary>
        public SoundEffect Back;

        /// <summary>
        /// The effect back
        /// </summary>
        public static SoundEffectInstance EffectBack;

        /// <summary>
        /// The victory
        /// </summary>
        public SoundEffect Victory;

        /// <summary>
        /// The effect victory
        /// </summary>
        public static SoundEffectInstance EffectVictory;

        /// <summary>
        /// The defeat
        /// </summary>
        public SoundEffect Defeat;

        /// <summary>
        /// The effect defeat
        /// </summary>
        public static SoundEffectInstance EffectDefeat;

        /// <summary>
        /// The song
        /// </summary>
        public SoundEffect Song;

        /// <summary>
        /// The main theme
        /// </summary>
        public static SoundEffectInstance MainTheme;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Character = new CharacterSprites(new Vector2(19 * Game1.UnitX, 5 * Game1.UnitY), 0, 2.5f, 0);
            this.Start = new Sprite(3 * Game1.UnitX, 3 * Game1.UnitY, Game1.ButtonWidth, 2 * Game1.ButtonHeight);
            this.ExitButton = new Sprite(3 * Game1.UnitX, 6 * Game1.UnitY, Game1.ButtonWidth, 2 * Game1.ButtonHeight);
            this.Options = new Sprite(3 * Game1.UnitX, 9 * Game1.UnitY, Game1.ButtonWidth, 2* Game1.ButtonHeight);
            this.SaveSprites = new Sprite[Save.NB_SAVE];
            for (int i = 0; i < Save.NB_SAVE; i++) {
                this.SaveSprites[i] = new Sprite(5 * Game1.UnitX, 13 * Game1.UnitY, 4 * Game1.UnitX, Game1.UnitY);
            }
            this.LeftSave = new Sprite(3 * Game1.UnitX, 13 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightSave = new Sprite(10 * Game1.UnitX, 13 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "StartMenu/StartMenu");
            this.Start.LoadContent(content, "StartMenu/Start");
            this.ExitButton.LoadContent(content, "StartMenu/Exit");
            this.Options.LoadContent(content, "StartMenu/Options");
            this.LeftSave.LoadContent(content, "Options/ArrowLeft");
            this.RightSave.LoadContent(content, "Options/ArrowRight");
            for (int i = 0; i < Save.NB_SAVE; i++) {
                this.SaveSprites[i].LoadContent(content, "StartMenu/Save" + (i+1));
            }
            string name = "Knight";
            this.Character.Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2, 4, 10);

            //Sounds
            Click = content.Load<SoundEffect>("Sound/click");
            EffectClick = Click.CreateInstance();
            EffectClick.Volume = EFFECT_VOLUME_MAX;

            Back = content.Load<SoundEffect>("Sound/negative-soft");
            EffectBack = Back.CreateInstance();
            EffectBack.Volume = EFFECT_VOLUME_MAX;

            Victory = content.Load<SoundEffect>("Sound/Victory");
            EffectVictory = Victory.CreateInstance();
            EffectVictory.Volume = EFFECT_VOLUME_MAX;

            Defeat = content.Load<SoundEffect>("Sound/Defeat");
            EffectDefeat = Defeat.CreateInstance();
            EffectDefeat.Volume = EFFECT_VOLUME_MAX;

            //Music
            Song = content.Load<SoundEffect>("Sound/Dust");
            MainTheme = Song.CreateInstance();
            MainTheme.IsLooped = true;
            MainTheme.Volume = VOLUME_ON;
            MainTheme.Play();
        }
        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X,currentMouseState.Y ,10,10);

                if (isOver(mouse, Options)) {
                    EffectClick.Play();
                    Game1.GameState = GameState.Options;
                }
                else if (isOver(mouse, Start)) {
                    EffectClick.Play();
                    Game1.GameState = GameState.GameMenu;
                    Game1.saveFileName = string.Format("save{0}.sav", Game1.Save.SelectedSave);
                    Game1.LoadCharacters();
                }
                else if (isOver(mouse, ExitButton)) {
                    Game1.GameState = GameState.Exit;
                }
                else if (isOver(mouse, LeftSave) && Game1.Save.SelectedSave > 0) {
                    EffectClick.Play();
                    Game1.Save.SelectedSave -= 1;
                }
                else if (isOver(mouse, RightSave) && Game1.Save.SelectedSave < Save.NB_SAVE - 1) {
                    EffectClick.Play();
                    Game1.Save.SelectedSave += 1;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Start.Draw(spriteBatch, gameTime);
            this.ExitButton.Draw(spriteBatch, gameTime);
            this.Options.Draw(spriteBatch, gameTime);
            this.LeftSave.Draw(spriteBatch, gameTime);
            this.RightSave.Draw(spriteBatch, gameTime);
            this.SaveSprites[Game1.Save.SelectedSave].Draw(spriteBatch, gameTime);
            this.Character.DrawFrame(spriteBatch);
        }
    }
}
