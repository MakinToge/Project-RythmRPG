using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class ModifyCharacter : Page
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
        /// Gets or sets the vitality.
        /// </summary>
        /// <value>
        /// The vitality.
        /// </value>
        public TextSprite Vitality { get; set; }

        /// <summary>
        /// Gets or sets the stats points.
        /// </summary>
        /// <value>
        /// The stats points.
        /// </value>
        public TextSprite StatsPoints { get; set; }

        /// <summary>
        /// Gets or sets the gold.
        /// </summary>
        /// <value>
        /// The gold.
        /// </value>
        public TextSprite Gold { get; set; }

        /// <summary>
        /// Gets or sets the confirm.
        /// </summary>
        /// <value>
        /// The confirm.
        /// </value>
        public Sprite Confirm { get; set; }

        /// <summary>
        /// Gets or sets the cancel.
        /// </summary>
        /// <value>
        /// The cancel.
        /// </value>
        public Sprite Cancel { get; set; }

        /// <summary>
        /// Gets or sets the upgrade vitality.
        /// </summary>
        /// <value>
        /// The upgrade vitality.
        /// </value>
        public Sprite UpgradeVitality { get; set; }

        /// <summary>
        /// Gets or sets the upgrade attack.
        /// </summary>
        /// <value>
        /// The upgrade attack.
        /// </value>
        public Sprite UpgradeAttack { get; set; }

        /// <summary>
        /// Gets or sets the upgrade defense.
        /// </summary>
        /// <value>
        /// The upgrade defense.
        /// </value>
        public Sprite UpgradeDefense { get; set; }

        /// <summary>
        /// Gets or sets the reset stats points.
        /// </summary>
        /// <value>
        /// The reset stats points.
        /// </value>
        public Sprite ResetStatsPoints { get; set; }

        /// <summary>
        /// Gets or sets the explain reset stats.
        /// </summary>
        /// <value>
        /// The explain reset stats.
        /// </value>
        public TextSprite ExplainResetStats { get; set; }

        /// <summary>
        /// Gets or sets the mouse rectangle.
        /// </summary>
        /// <value>
        /// The mouse rectangle.
        /// </value>
        public Rectangle MouseRectangle { get; set; }

        /// <summary>
        /// Gets or sets the defense plus.
        /// </summary>
        /// <value>
        /// The defense plus.
        /// </value>
        public int DefensePlus { get; set; }

        /// <summary>
        /// Gets or sets the attack plus.
        /// </summary>
        /// <value>
        /// The attack plus.
        /// </value>
        public int AttackPlus { get; set; }

        /// <summary>
        /// Gets or sets the vitality plus.
        /// </summary>
        /// <value>
        /// The vitality plus.
        /// </value>
        public int VitalityPlus { get; set; }

        /// <summary>
        /// Gets or sets the used stat points.
        /// </summary>
        /// <value>
        /// The used stat points.
        /// </value>
        public int UsedStatPoints { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            this.Cancel = new Sprite(13 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.Confirm = new Sprite(21 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.UpgradeAttack = new Sprite(21 * Game1.UnitX, 8 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.UpgradeDefense = new Sprite(21 * Game1.UnitX, 9 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.UpgradeVitality = new Sprite(21 * Game1.UnitX, 10 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.ResetStatsPoints = new Sprite(21 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            // Explain TextSprite
            this.ExplainResetStats = this.HP = new TextSprite(22 * Game1.UnitX, 6.2f * Game1.UnitY, "Cost : 100 Gold", Color.Black);

            // Character Data
            this.Name = new TextSprite(15 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(16 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(17 * Game1.UnitX, 9.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(14 * Game1.UnitX, 7.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(17 * Game1.UnitX, 8.2f * Game1.UnitY, "", Color.Black);
            this.StatsPoints = new TextSprite(19 * Game1.UnitX, 6.2f * Game1.UnitY, "", Color.Black);
            this.Gold = new TextSprite(16 * Game1.UnitX, 11.2f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(18 * Game1.UnitX, 10.2f * Game1.UnitY, "", Color.Black);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.MainImage.LoadContent(content, "ModifyCharacter/Custom");
            this.Back.LoadContent(content, "Options/Back");
            this.Cancel.LoadContent(content, "ModifyCharacter/Cancel");
            this.Confirm.LoadContent(content, "ModifyCharacter/Confirm");
            this.UpgradeAttack.LoadContent(content, "ModifyCharacter/Upgrade");
            this.UpgradeDefense.LoadContent(content, "ModifyCharacter/Upgrade");
            this.UpgradeVitality.LoadContent(content, "ModifyCharacter/Upgrade");
            this.ResetStatsPoints.LoadContent(content, "ModifyCharacter/ResetStatsPoints");

            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.StatsPoints.LoadContent(content, "Arial16");
            this.Gold.LoadContent(content, "Arial16");
            this.ExplainResetStats.LoadContent(content, "Arial16");
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
            PlayableCharacter character = Game1.characters.getSelectedCharacter();
            Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
            this.MouseRectangle = mouse;
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (isOver(mouse, Back))
                {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.CharacterManagement;
                }
                else if (isOver(mouse, UpgradeVitality) && IsUpgradable())
                {
                    StartMenu.EffectClick.Play();
                    this.VitalityPlus += 1;
                    this.Vitality.Text = (character.Vitality + this.VitalityPlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (character.statPoints - this.UsedStatPoints).ToString();
                    this.HP.Text = (character.Level * (character.Vitality + this.VitalityPlus) + 10).ToString();
                }
                else if (isOver(mouse, UpgradeAttack) && IsUpgradable())
                {
                    StartMenu.EffectClick.Play();
                    this.AttackPlus += 1;
                    this.Strength.Text = (character.Attack + this.AttackPlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (character.statPoints - this.UsedStatPoints).ToString();
                }
                else if (isOver(mouse, UpgradeDefense) && IsUpgradable())
                {
                    StartMenu.EffectClick.Play();
                    this.DefensePlus += 1;
                    this.Endurance.Text = (character.Defense + this.DefensePlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (character.statPoints - this.UsedStatPoints).ToString();
                }
                else if (isOver(mouse, ResetStatsPoints))
                {
                    StartMenu.EffectClick.Play();

                    if (character.respec())
                    {
                        this.DefensePlus = 0;
                        this.AttackPlus = 0;
                        this.VitalityPlus = 0;
                        this.UsedStatPoints = 0;
                    }

                    this.LoadDataCharacter(character);
                }
                else if (isOver(mouse, Cancel))
                {
                    StartMenu.EffectClick.Play();
                    this.AttackPlus = 0;
                    this.DefensePlus = 0;
                    this.VitalityPlus = 0;
                    this.UsedStatPoints = 0;

                    this.LoadDataCharacter(character);
                }
                else if (isOver(mouse, Confirm))
                {
                    StartMenu.EffectClick.Play();
                    character.addAttack(this.AttackPlus);
                    character.addDefense(this.DefensePlus);
                    character.addVitality(this.VitalityPlus);

                    this.AttackPlus = 0;
                    this.DefensePlus = 0;
                    this.VitalityPlus = 0;
                    this.UsedStatPoints = 0;

                    this.LoadDataCharacter(character);
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
            PlayableCharacter character = Game1.characters.getSelectedCharacter();
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);
            character.setPosition(new Vector2(4 * Game1.UnitX, 5 * Game1.UnitY));
            character.Draw(spriteBatch);

            this.Cancel.Draw(spriteBatch, gameTime);
            this.Confirm.Draw(spriteBatch, gameTime);
            this.UpgradeAttack.Draw(spriteBatch, gameTime);
            this.UpgradeDefense.Draw(spriteBatch, gameTime);
            this.UpgradeVitality.Draw(spriteBatch, gameTime);
            this.ResetStatsPoints.Draw(spriteBatch, gameTime);

            // Character Data
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.StatsPoints.Draw(spriteBatch, gameTime);
            this.Gold.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);

            if (isOver(this.MouseRectangle, ResetStatsPoints))
            {
                this.ExplainResetStats.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Loads the data character.
        /// </summary>
        /// <param name="character">The character.</param>
        public void LoadDataCharacter(PlayableCharacter character)
        {

            this.Name.Text = character.Name;
            this.Level.Text = character.Level.ToString();
            this.Endurance.Text = (character.Defense + this.DefensePlus).ToString();
            this.Vitality.Text = (character.Vitality + this.VitalityPlus).ToString();
            this.HP.Text = character.getMaxHealth().ToString();
            this.Strength.Text = (character.Attack + this.AttackPlus).ToString();
            this.StatsPoints.Text = character.statPoints.ToString();
            this.Gold.Text = character.gold.ToString();
        }

        /// <summary>
        /// Determines whether this instance is upgradable.
        /// </summary>
        /// <returns></returns>
        public bool IsUpgradable()
        {
            if (this.UsedStatPoints < Game1.characters.getSelectedCharacter().statPoints)
            {
                return true;
            }
            return false;
        }
    }
}
