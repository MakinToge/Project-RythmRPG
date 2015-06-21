using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class ModifyCharacter : Page{
        public Sprite MainImage { get; set; }
        public Sprite Back { get; set; }
        public Sprite CharacterSprite { get; set; }
        public PlayableCharacter Character { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite HP { get; set; }
        public TextSprite Strength { get; set; }
        public TextSprite Vitality { get; set; }
        public TextSprite StatsPoints { get; set; }
        public TextSprite Gold { get; set; }
        public Sprite Confirm { get; set; }
        public Sprite Cancel { get; set; }
        public Sprite UpgradeVitality { get; set; }
        public Sprite UpgradeAttack { get; set; }
        public Sprite UpgradeDefense { get; set; }
        public Sprite ResetStatsPoints { get; set; }
        public TextSprite ExplainResetStats { get; set; }
        public Rectangle MouseRectangle { get; set; }

        public int DefensePlus { get; set; }
        public int AttackPlus { get; set; }
        public int VitalityPlus { get; set; }
        public int UsedStatPoints { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            this.Cancel = new Sprite(13 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.Confirm = new Sprite(21 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.UpgradeAttack = new Sprite(21 * Game1.UnitX, 8 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.UpgradeDefense = new Sprite(21 * Game1.UnitX, 9 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.UpgradeVitality = new Sprite(21 * Game1.UnitX, 10 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY);
            this.ResetStatsPoints = new Sprite(21 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            //Explain TextSprite
            this.ExplainResetStats = this.HP = new TextSprite(22 * Game1.UnitX, 6.2f * Game1.UnitY, "Cost : 100 Gold", Color.Black);

            //Character Data
            this.CharacterSprite = new Sprite(2 * Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, 8 * Game1.UnitY);
            this.Name = new TextSprite(15 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(16 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(17 * Game1.UnitX, 9.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(14 * Game1.UnitX, 7.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(17 * Game1.UnitX, 8.2f * Game1.UnitY, "", Color.Black);
            this.StatsPoints = new TextSprite(19 * Game1.UnitX, 6.2f * Game1.UnitY, "", Color.Black);
            this.Gold = new TextSprite(16 * Game1.UnitX, 11.2f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(18 * Game1.UnitX, 10.2f * Game1.UnitY, "", Color.Black);

        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "ModifyCharacter/Custom");
            this.Back.LoadContent(content, "Options/Back");
            this.CharacterSprite.LoadContent(content, "Characters/custom");
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
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
            this.MouseRectangle = mouse;
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {

                if (isOver(mouse, Back)) {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.CharacterManagement;
                }
                else if (isOver(mouse, UpgradeVitality) && IsUpgradable()) {
                    StartMenu.EffectClick.Play();
                    this.VitalityPlus += 1;
                    this.Vitality.Text = (this.Character.Vitality + this.VitalityPlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (this.Character.statPoints - this.UsedStatPoints).ToString();
                    this.HP.Text = (this.Character.Level * (this.Character.Vitality + this.VitalityPlus)).ToString();
                }
                else if (isOver(mouse, UpgradeAttack) && IsUpgradable()) {
                    StartMenu.EffectClick.Play();
                    this.AttackPlus += 1;
                    this.Strength.Text = (this.Character.Attack + this.AttackPlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (this.Character.statPoints - this.UsedStatPoints).ToString();
                }
                else if (isOver(mouse, UpgradeDefense) && IsUpgradable()) {
                    StartMenu.EffectClick.Play();
                    this.DefensePlus += 1;
                    this.Endurance.Text = (this.Character.Defense + this.DefensePlus).ToString();
                    this.UsedStatPoints += 1;
                    this.StatsPoints.Text = (this.Character.statPoints - this.UsedStatPoints).ToString();
                }
                else if (isOver(mouse, ResetStatsPoints)) {
                    StartMenu.EffectClick.Play();
                    this.Character.respec();
                    this.LoadDataCharacter(this.Character);
                }
                else if (isOver(mouse, Cancel)) {
                    StartMenu.EffectClick.Play();
                    this.AttackPlus = 0;
                    this.DefensePlus = 0;
                    this.VitalityPlus = 0;
                    this.UsedStatPoints = 0;
                    this.Strength.Text = (this.Character.Attack + this.AttackPlus).ToString();
                    this.Endurance.Text = (this.Character.Defense + this.DefensePlus).ToString();
                    this.Vitality.Text = (this.Character.Vitality + this.VitalityPlus).ToString();
                    this.StatsPoints.Text = (this.Character.statPoints - this.UsedStatPoints).ToString();
                    this.HP.Text = (this.Character.Level * (this.Character.Vitality + this.VitalityPlus)).ToString();
                }
                else if (isOver(mouse, Confirm)) {
                    StartMenu.EffectClick.Play();
                    this.Character.addAttack(this.AttackPlus);
                    this.Character.addDefense(this.DefensePlus);
                    this.Character.addVitality(this.VitalityPlus);
                    this.Character.statPoints = this.Character.statPoints - this.UsedStatPoints;

                    this.AttackPlus = 0;
                    this.DefensePlus = 0;
                    this.VitalityPlus = 0;

                    this.LoadDataCharacter(this.Character);
                }
            }

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);
            this.CharacterSprite.Draw(spriteBatch, gameTime);
            this.Cancel.Draw(spriteBatch, gameTime);
            this.Confirm.Draw(spriteBatch, gameTime);
            this.UpgradeAttack.Draw(spriteBatch, gameTime);
            this.UpgradeDefense.Draw(spriteBatch, gameTime);
            this.UpgradeVitality.Draw(spriteBatch, gameTime);
            this.ResetStatsPoints.Draw(spriteBatch, gameTime);

            //Character Data
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.StatsPoints.Draw(spriteBatch, gameTime);
            this.Gold.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);

            
            if (isOver(this.MouseRectangle, ResetStatsPoints)) {
                this.ExplainResetStats.Draw(spriteBatch, gameTime);
            }
        }

        public void LoadDataCharacter(PlayableCharacter character) {
            this.Character = character;

            this.Name.Text = character.Name;
            this.Level.Text = character.Level.ToString();
            this.Endurance.Text = (character.Defense + this.DefensePlus).ToString();
            this.Vitality.Text = (character.Vitality + this.VitalityPlus).ToString();
            this.HP.Text = character.Health.ToString();
            this.Strength.Text = (character.Attack + this.AttackPlus).ToString();
            this.StatsPoints.Text = character.statPoints.ToString();
            this.Gold.Text = character.gold.ToString();
        }
        public bool IsUpgradable() {
            if ( this.UsedStatPoints < this.Character.statPoints) {
                return true;
            }
            return false;
        }
    }
}
