using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class Options : Page{
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
        /// Gets or sets the see controls.
        /// </summary>
        /// <value>
        /// The see controls.
        /// </value>
        public Sprite SeeControls { get; set; }
        /// <summary>
        /// Gets or sets the left device.
        /// </summary>
        /// <value>
        /// The left device.
        /// </value>
        public Sprite LeftDevice { get; set; }
        /// <summary>
        /// Gets or sets the right device.
        /// </summary>
        /// <value>
        /// The right device.
        /// </value>
        public Sprite RightDevice { get; set; }
        /// <summary>
        /// Gets or sets the left music.
        /// </summary>
        /// <value>
        /// The left music.
        /// </value>
        public Sprite LeftMusic { get; set; }
        /// <summary>
        /// Gets or sets the right music.
        /// </summary>
        /// <value>
        /// The right music.
        /// </value>
        public Sprite RightMusic { get; set; }
        /// <summary>
        /// Gets or sets the left sound.
        /// </summary>
        /// <value>
        /// The left sound.
        /// </value>
        public Sprite LeftSound { get; set; }
        /// <summary>
        /// Gets or sets the right sound.
        /// </summary>
        /// <value>
        /// The right sound.
        /// </value>
        public Sprite RightSound { get; set; }
        /// <summary>
        /// Gets or sets the volume music.
        /// </summary>
        /// <value>
        /// The volume music.
        /// </value>
        public Sprite[] VolumeMusic { get; set; }
        /// <summary>
        /// Gets or sets the volume sound.
        /// </summary>
        /// <value>
        /// The volume sound.
        /// </value>
        public Sprite[] VolumeSound { get; set; }
        /// <summary>
        /// Gets or sets the reset progression.
        /// </summary>
        /// <value>
        /// The reset progression.
        /// </value>
        public Sprite ResetProgression { get; set; }
        /// <summary>
        /// Gets or sets the keyboard.
        /// </summary>
        /// <value>
        /// The keyboard.
        /// </value>
        public Sprite Keyboard { get; set; }
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        public Sprite Devices { get; set; }
        /// <summary>
        /// Gets or sets the mute menu.
        /// </summary>
        /// <value>
        /// The mute menu.
        /// </value>
        public Sprite MuteMenu { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);
            this.SeeControls = new Sprite(5 * Game1.UnitX, 13 * Game1.UnitY, Game1.ButtonWidth, Game1.ButtonHeight);

            this.LeftDevice = new Sprite(5 * Game1.UnitX, 7 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightDevice = new Sprite(12 * Game1.UnitX, 7 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.LeftMusic = new Sprite(20 * Game1.UnitX, 7 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightMusic = new Sprite(26 * Game1.UnitX, 7 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.LeftSound = new Sprite(20 * Game1.UnitX, 11 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightSound = new Sprite(26 * Game1.UnitX, 11 * Game1.UnitY, Game1.UnitX, Game1.UnitY);

            this.VolumeMusic = new Sprite[5];
            this.VolumeSound = new Sprite[5];
            for (int i = 0; i < this.VolumeMusic.Length; i++) {
                this.VolumeMusic[i] = new Sprite((21 + i) * Game1.UnitX, 7 * Game1.UnitY, Game1.UnitX / 2, Game1.UnitY);
            }
            for (int i = 0; i < this.VolumeSound.Length; i++) {
                this.VolumeSound[i] = new Sprite((21 + i) * Game1.UnitX, 11 * Game1.UnitY, Game1.UnitX / 2, Game1.UnitY);
            }

            this.ResetProgression = new Sprite(12 * Game1.UnitX, 16 * Game1.UnitY, Game1.ButtonWidth, Game1.ButtonHeight);
            this.Keyboard = new Sprite(7 * Game1.UnitX, 10 * Game1.UnitY, 4 * Game1.UnitX, 2 * Game1.UnitY);
            this.Devices = new Sprite(7 * Game1.UnitX, 6 * Game1.UnitY, 4 * Game1.UnitX, 2 * Game1.UnitY);

            this.MuteMenu = new Sprite(20 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "Options/Options");
            this.Back.LoadContent(content, "Options/Back");
            this.SeeControls.LoadContent(content, "Options/SeeControls");
            this.ResetProgression.LoadContent(content, "Options/ResetProgression");

            this.LeftDevice.LoadContent(content, "Options/ArrowLeft");
            this.RightDevice.LoadContent(content, "Options/ArrowRight");
            this.LeftMusic.LoadContent(content, "Options/ArrowLeft");
            this.RightMusic.LoadContent(content, "Options/ArrowRight");
            this.LeftSound.LoadContent(content, "Options/ArrowLeft");
            this.RightSound.LoadContent(content, "Options/ArrowRight");

            this.Keyboard.LoadContent(content, "Options/keyboard");
            this.Devices.LoadContent(content, "Options/keyboard");

            this.MuteMenu.LoadContent(content, "Options/MuteMenu");

            foreach (Sprite item in this.VolumeMusic) {
                item.LoadContent(content, "Options/One");
            }
            foreach (Sprite item in this.VolumeSound) {
                item.LoadContent(content, "Options/One");
            }
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

                if (isOver(mouse, Back)) {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.StartMenu;
                }
                else if (isOver(mouse, ResetProgression)) { //Clique sur ResetProgression
                    StartMenu.EffectClick.Play();
                }
                    //Music Volume
                else if (isOver(mouse, this.LeftMusic) && Game1.VolumeMusic > 0) {
                    Game1.VolumeMusic -= 2;
                    StartMenu.MainTheme.Volume -= 0.1f;
                    StartMenu.EffectClick.Play();
                }
                else if (isOver(mouse, this.RightMusic) && Game1.VolumeMusic < 10) {           
                    Game1.VolumeMusic += 2;
                    StartMenu.MainTheme.Volume += 0.1f;
                    StartMenu.EffectClick.Play();
                }
                    //Sound Volume
                else if (isOver(mouse, this.LeftSound) && Game1.VolumeSound > 0) {
                    Game1.VolumeSound -= 2;
                    StartMenu.EffectBack.Volume -= 0.2f;
                    StartMenu.EffectClick.Volume -= 0.2f;
                    StartMenu.EffectVictory.Volume -= 0.2f;
                    StartMenu.EffectDefeat.Volume -= 0.2f;
                    StartMenu.EffectClick.Play();
                }
                else if (isOver(mouse, this.RightSound) && Game1.VolumeSound < 10) {
                    Game1.VolumeSound += 2;
                    StartMenu.EffectBack.Volume += 0.2f;
                    StartMenu.EffectClick.Volume += 0.2f;
                    StartMenu.EffectVictory.Volume += 0.2f;
                    StartMenu.EffectDefeat.Volume += 0.2f;
                    StartMenu.EffectClick.Play();
                }
                //Menu Volume
                else if (isOver(mouse, this.MuteMenu)) {
                    if (Game1.VolumeMenu == 0) {
                        Game1.VolumeMenu = 5;
                        Game1.VolumeMusic = 10;
                        StartMenu.MainTheme.Volume = StartMenu.VOLUME_ON;
                    }
                    else {
                        Game1.VolumeMenu = 0;
                        Game1.VolumeMusic = 0;
                        StartMenu.MainTheme.Volume = StartMenu.VOLUME_OFF;
                    }
                    
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
            this.Back.Draw(spriteBatch, gameTime);
            this.SeeControls.Draw(spriteBatch, gameTime);
            this.ResetProgression.Draw(spriteBatch, gameTime);
            this.LeftDevice.Draw(spriteBatch, gameTime);
            this.RightDevice.Draw(spriteBatch, gameTime);
            this.LeftMusic.Draw(spriteBatch, gameTime);
            this.RightMusic.Draw(spriteBatch, gameTime);
            this.LeftSound.Draw(spriteBatch, gameTime);
            this.RightSound.Draw(spriteBatch, gameTime);

            this.Keyboard.Draw(spriteBatch, gameTime);
            this.Devices.Draw(spriteBatch, gameTime);

            this.MuteMenu.Draw(spriteBatch, gameTime);

            for (int i = 0; i < Game1.VolumeMusic /2; i++) {
                VolumeMusic[i].Draw(spriteBatch, gameTime);
            }
            for (int i = 0; i < Game1.VolumeSound /2; i++) {
                VolumeSound[i].Draw(spriteBatch, gameTime);
            }
        }

    }
}
