using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class GameMenu : Page
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
        /// Gets or sets the character management.
        /// </summary>
        /// <value>
        /// The character management.
        /// </value>
        public Sprite CharacterManagement { get; set; }

        /// <summary>
        /// Gets or sets the playlist challenge.
        /// </summary>
        /// <value>
        /// The playlist challenge.
        /// </value>
        public Sprite PlaylistChallenge { get; set; }

        /// <summary>
        /// Gets or sets the single music.
        /// </summary>
        /// <value>
        /// The single music.
        /// </value>
        public Sprite SingleMusic { get; set; }

        /// <summary>
        /// Gets or sets the left character.
        /// </summary>
        /// <value>
        /// The left character.
        /// </value>
        public Sprite LeftCharacter { get; set; }

        /// <summary>
        /// Gets or sets the right character.
        /// </summary>
        /// <value>
        /// The right character.
        /// </value>
        public Sprite RightCharacter { get; set; }

        /// <summary>
        /// Gets or sets the skill list.
        /// </summary>
        /// <value>
        /// The skill list.
        /// </value>
        public List<TextSprite> SkillList { get; set; }

        /// <summary>
        /// The font
        /// </summary>
        private SpriteFont font;

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
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        public TextSprite[] Skills { get; set; }

        /// <summary>
        /// Gets or sets the ability.
        /// </summary>
        /// <value>
        /// The ability.
        /// </value>
        public TextSprite Ability { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);
            this.SingleMusic = new Sprite(3 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.PlaylistChallenge = new Sprite(3 * Game1.UnitX, 7 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.CharacterManagement = new Sprite(3 * Game1.UnitX, 10 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.LeftCharacter = new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightCharacter = new Sprite(22 * Game1.UnitX, 2 * Game1.UnitY, Game1.UnitX, Game1.UnitY);

            this.Type = new TextSprite(27 * Game1.UnitX, 3.2f * Game1.UnitY, "", Color.Black);
            this.Name = new TextSprite(27 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(28 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(28 * Game1.UnitX, 9.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(26 * Game1.UnitX, 6.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(28 * Game1.UnitX, 8.2f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(29 * Game1.UnitX, 7.2f * Game1.UnitY, "", Color.Black);
            this.Ability = new TextSprite(28 * Game1.UnitX, 10.2f * Game1.UnitY, "", Color.Black);

            this.SkillList = new List<TextSprite>();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.MainImage.LoadContent(content, "GameMenu/GameMenu");
            this.Back.LoadContent(content, "Options/Back");
            this.SingleMusic.LoadContent(content, "GameMenu/SingleMusic");
            this.PlaylistChallenge.LoadContent(content, "GameMenu/PlaylistChallenge");
            this.CharacterManagement.LoadContent(content, "GameMenu/CharactersManagement");

            this.LeftCharacter.LoadContent(content, "Options/ArrowLeft");
            this.RightCharacter.LoadContent(content, "Options/ArrowRight");

            this.font = content.Load<SpriteFont>("Arial16");
            this.Type.LoadContent(content, "Arial16");
            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Ability.LoadContent(content, "Arial16");
            this.Vitality.LoadContent(content, "Arial16");
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

                if (isOver(mouse, Back))
                {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.StartMenu;
                }
                else if (isOver(mouse, CharacterManagement))
                {
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.CharacterManagement;
                }
                else if (isOver(mouse, LeftCharacter) && Game1.characters.selectedCharacter > 0)
                {
                    StartMenu.EffectClick.Play();
                    Game1.characters.selectedCharacter -= 1;
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, RightCharacter) && Game1.characters.selectedCharacter < Game1.characters.NbCharacter - 1)
                {
                    StartMenu.EffectClick.Play();
                    Game1.characters.selectedCharacter += 1;
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, SingleMusic))
                {
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.SingleMusic;
                }
                else if (isOver(mouse, PlaylistChallenge))
                {
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.PlaylistChallenge;
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
            this.SingleMusic.Draw(spriteBatch, gameTime);
            this.PlaylistChallenge.Draw(spriteBatch, gameTime);
            this.CharacterManagement.Draw(spriteBatch, gameTime);

            this.LeftCharacter.Draw(spriteBatch, gameTime);
            this.RightCharacter.Draw(spriteBatch, gameTime);

            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            this.LoadDataCharacter(tmp);

            if (tmp.Name == "Barbarian")
            {
                tmp.setPosition(new Vector2(13 * Game1.UnitX, 5 * Game1.UnitY));
                tmp.setScale(2);
            }
            else
            {
                tmp.setPosition(new Vector2(16 * Game1.UnitX, 5 * Game1.UnitY));
                tmp.setScale(2);
            }
            tmp.Draw(spriteBatch);

            this.Type.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Ability.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);

            for (int i = 0; i < this.SkillList.Count; i++)
            {
                this.SkillList.ElementAt<TextSprite>(i).Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Loads the data character.
        /// </summary>
        /// <param name="character">The character.</param>
        public void LoadDataCharacter(PlayableCharacter character)
        {
            this.Type.Text = character.Name;
            this.Name.Text = character.Name;

            if (character.NbRestart == 0)
            {
                this.Level.Text = character.Level.ToString();
            }
            else
            {
                this.Level.Text = string.Format("{0} ({1})", character.Level, character.NbRestart);
            }

            this.Endurance.Text = character.Defense.ToString();
            this.Vitality.Text = character.Vitality.ToString();
            this.HP.Text = character.Health.ToString();
            this.Strength.Text = character.Attack.ToString();
            this.Ability.Text = character.uniqueSkill.ToString();

            this.loadSkills(character);
        }

        /// <summary>
        /// Loads the skills.
        /// </summary>
        /// <param name="character">The character.</param>
        public void loadSkills(PlayableCharacter character)
        {
            this.SkillList.Clear();

            for (int i = 0; i < character.skills.Count; i++)
            {
                Vector2 position = new Vector2(25 * Game1.UnitX, 12f * Game1.UnitY + i * 30);
                TextSprite textSprite = new TextSprite(position, character.skills.ElementAt<Skills>(i).ToString(), Color.Black);
                textSprite.Font = this.font;
                this.SkillList.Add(textSprite);
            }
        }
    }
}
