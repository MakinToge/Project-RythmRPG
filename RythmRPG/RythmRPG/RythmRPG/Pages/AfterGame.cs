using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class AfterGame : Page{
        /// <summary>
        /// Gets or sets the main image.
        /// </summary>
        /// <value>
        /// The main image.
        /// </value>
        public Sprite MainImage { get; set; }
        /// <summary>
        /// Gets or sets the asset name main image.
        /// </summary>
        /// <value>
        /// The asset name main image.
        /// </value>
        public string AssetNameMainImage { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the xp.
        /// </summary>
        /// <value>
        /// The xp.
        /// </value>
        public TextSprite Xp { get; set; }
        /// <summary>
        /// Gets or sets the play again.
        /// </summary>
        /// <value>
        /// The play again.
        /// </value>
        public Sprite PlayAgain { get; set; }
        /// <summary>
        /// Gets or sets the game menu.
        /// </summary>
        /// <value>
        /// The game menu.
        /// </value>
        public Sprite GameMenu { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TextSprite Type { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public TextSprite Name { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public TextSprite Level { get; set; }
        /// <summary>
        /// Gets or sets the endurance.
        /// </summary>
        /// <value>
        /// The endurance.
        /// </value>
        public TextSprite Endurance { get; set; }
        /// <summary>
        /// Gets or sets the vitality.
        /// </summary>
        /// <value>
        /// The vitality.
        /// </value>
        public TextSprite Vitality { get; set; }
        /// <summary>
        /// Gets or sets the hp.
        /// </summary>
        /// <value>
        /// The hp.
        /// </value>
        public TextSprite HP { get; set; }
        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        public TextSprite Strength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AfterGame"/> class.
        /// </summary>
        /// <param name="assetNameMainImage">The asset name main image.</param>
        /// <param name="message">The message.</param>
        public AfterGame(string assetNameMainImage, string message) {
            this.AssetNameMainImage = assetNameMainImage;
            this.Message = message;
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.PlayAgain = new Sprite(22 * Game1.UnitX, 6 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.GameMenu = new Sprite(22 * Game1.UnitX, 9 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.Xp = new TextSprite(22 * Game1.UnitX, 3.9f * Game1.UnitY, this.Message, Color.Black);
            this.Type = new TextSprite(17 * Game1.UnitX, 3.9f * Game1.UnitY, "", Color.Black);
            this.Name = new TextSprite(17 * Game1.UnitX, 4.9f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(17 * Game1.UnitX, 5.9f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(16 * Game1.UnitX, 6.9f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(18 * Game1.UnitX, 7.9f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(18 * Game1.UnitX, 8.9f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(19 * Game1.UnitX, 9.9f * Game1.UnitY, "", Color.Black);
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "AfterGame/" + this.AssetNameMainImage);
            this.PlayAgain.LoadContent(content, "AfterGame/PlayAgain");
            this.GameMenu.LoadContent(content, "AfterGame/GameMenu");

            this.Xp.LoadContent(content, "Arial16");
            this.Type.LoadContent(content, "Arial16");
            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Vitality.LoadContent(content, "Arial16");
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
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
                Game1.characters.getSelectedCharacter().prepareForMusic();
                if (isOver(mouse, PlayAgain)) { //Clique sur Play Again
                    StartMenu.EffectClick.Play();
                    MusicPlaying.output.Dispose();
                    MusicPlaying.output = null;
                    MusicPlaying.stream.Dispose();
                    MusicPlaying.stream = null;
                    MusicPlaying musicPlaying = new MusicPlaying();
                    Game1.GameState = GameState.MusicPlaying;

                    musicPlaying.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                    musicPlaying.LoadGame();

                }
                else if (isOver(mouse, GameMenu)) {
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Play();
                    MusicPlaying.output.Dispose();
                    MusicPlaying.output = null;
                    MusicPlaying.stream.Dispose();
                    MusicPlaying.stream = null;
                    Game1.GameState = GameState.GameMenu;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.PlayAgain.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Xp.Draw(spriteBatch, gameTime);

            //Character Data
            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            tmp.prepareForMusic();
            if (tmp.Name == "Barbarian")
            {
                tmp.setPosition(new Vector2(2.5f * Game1.UnitX, 5.5f * Game1.UnitY));
            }
            else
            {
                tmp.setPosition(new Vector2(4.5f * Game1.UnitX, 5.5f * Game1.UnitY));
            }
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

        /// <summary>
        /// Loads the data character.
        /// </summary>
        /// <param name="character">The character.</param>
        public void LoadDataCharacter(PlayableCharacter character) {
            this.Type.Text = character.Name;
            this.Name.Text = character.Name;
            if (character.NbRestart == 0) {
                this.Level.Text = character.Level.ToString();
            }
            else {
                this.Level.Text = string.Format("{0} ({1})", character.Level, character.NbRestart);
            }
            this.Endurance.Text = character.Defense.ToString();
            this.HP.Text = character.getMaxHealth().ToString();
            this.Strength.Text = character.Attack.ToString();
            this.Vitality.Text = character.Vitality.ToString();
        }
    }
}
