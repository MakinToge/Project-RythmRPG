using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class CharacterManagement : Page
    {
        /// <summary>
        /// The number of skills
        /// </summary>
        private const int NB_SKILLS = 9;

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
        /// Gets or sets the modify.
        /// </summary>
        /// <value>
        /// The modify.
        /// </value>
        public Sprite Modify { get; set; }
        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public PlayableCharacter Character { get; set; }

        /// <summary>
        /// Gets or sets the skill list.
        /// </summary>
        /// <value>
        /// The skill list.
        /// </value>
        public TextSprite[] SkillList { get; set; }
        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>
        /// The tool tip.
        /// </value>
        public TextSprite ToolTip { get; set; }
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
        /// Gets or sets the xp next level.
        /// </summary>
        /// <value>
        /// The xp next level.
        /// </value>
        public TextSprite XPNextLevel { get; set; }
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
        /// Gets or sets the vitality.
        /// </summary>
        /// <value>
        /// The vitality.
        /// </value>
        public TextSprite Vitality { get; set; }
        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        public TextSprite Strength { get; set; }
        /// <summary>
        /// Gets or sets the ability.
        /// </summary>
        /// <value>
        /// The ability.
        /// </value>
        public TextSprite Ability { get; set; }
        /// <summary>
        /// Gets or sets the xp.
        /// </summary>
        /// <value>
        /// The xp.
        /// </value>
        public TextSprite Xp { get; set; }
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
        /// Gets or sets the selected character.
        /// </summary>
        /// <value>
        /// The selected character.
        /// </value>
        public int SelectedCharacter { get; set; }
        /// <summary>
        /// Gets or sets the tab medium.
        /// </summary>
        /// <value>
        /// The tab medium.
        /// </value>
        public Sprite[] TabMedium { get; set; }
        /// <summary>
        /// Gets or sets the tab tank.
        /// </summary>
        /// <value>
        /// The tab tank.
        /// </value>
        public Sprite[] TabTank { get; set; }
        /// <summary>
        /// Gets or sets the tab DPS.
        /// </summary>
        /// <value>
        /// The tab DPS.
        /// </value>
        public Sprite[] TabDPS { get; set; }
        /// <summary>
        /// Gets or sets the tab custom.
        /// </summary>
        /// <value>
        /// The tab custom.
        /// </value>
        public Sprite[] TabCustom { get; set; }
        /// <summary>
        /// The tab selected
        /// </summary>
        private int tabSelected;
        public int TabSelected
        {
            get { return tabSelected; }
            set
            {
                tabSelected = value;
                this.SelectedCharacter = value;
            }
        }
        /// <summary>
        /// Gets or sets the modify character page.
        /// </summary>
        /// <value>
        /// The modify character page.
        /// </value>
        public ModifyCharacter ModifyCharacter { get; set; }


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            this.TabMedium = new Sprite[2] {
                new Sprite(2 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(2 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabTank = new Sprite[2] {
                new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(9 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabDPS = new Sprite[2] {
                new Sprite(16 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(16 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };
            this.TabCustom = new Sprite[2] {
                new Sprite(23 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY),
                new Sprite(23 * Game1.UnitX, 2 * Game1.UnitY, 7 * Game1.UnitX, Game1.UnitY)
            };

            this.SkillList = new TextSprite[NB_SKILLS];
            for (int i = 0; i < NB_SKILLS; i++)
            {
                this.SkillList[i] = new TextSprite(25 * Game1.UnitX, (7f * Game1.UnitY) + i * 30, "", Color.DarkSlateGray);
                this.SkillList[i].Text = ((Skills)i).ToString();
            }
            this.ToolTip = new TextSprite(25 * Game1.UnitX, (14f * Game1.UnitY), "Left click to activate,\r\nRight click to deactivate !", Color.Black);
            this.Name = new TextSprite(15 * Game1.UnitX, 3.3f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(6 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);
            this.Xp = new TextSprite(5 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.XPNextLevel = new TextSprite(8 * Game1.UnitX, 6.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(7 * Game1.UnitX, 10.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(5 * Game1.UnitX, 7.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(7 * Game1.UnitX, 9.2f * Game1.UnitY, "", Color.Black);
            this.Vitality = new TextSprite(8 * Game1.UnitX, 8.2f * Game1.UnitY, "", Color.Black);
            this.Ability = new TextSprite(25 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.StatsPoints = new TextSprite(8 * Game1.UnitX, 11.2f * Game1.UnitY, "", Color.Black);
            this.Gold = new TextSprite(6 * Game1.UnitX, 12.2f * Game1.UnitY, "", Color.Black);

            this.Modify = new Sprite(2 * Game1.UnitX, 13 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.TabSelected = 0;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.MainImage.LoadContent(content, "CharacterManagement/CharacterManagement");
            this.Back.LoadContent(content, "Options/Back");
            this.Modify.LoadContent(content, "CharacterManagement/ModifyCharacter");

            this.TabMedium[0].LoadContent(content, "CharacterManagement/Medium");
            this.TabMedium[1].LoadContent(content, "CharacterManagement/Selected/Medium");
            this.TabTank[0].LoadContent(content, "CharacterManagement/Tank");
            this.TabTank[1].LoadContent(content, "CharacterManagement/Selected/Tank");
            this.TabDPS[0].LoadContent(content, "CharacterManagement/DPS");
            this.TabDPS[1].LoadContent(content, "CharacterManagement/Selected/DPS");
            this.TabCustom[0].LoadContent(content, "CharacterManagement/Custom");
            this.TabCustom[1].LoadContent(content, "CharacterManagement/Selected/Custom");


            for (int i = 0; i < NB_SKILLS; i++)
            {
                this.SkillList[i].LoadContent(content, "Arial16");
            }
            this.ToolTip.LoadContent(content, "Arial16");
            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Ability.LoadContent(content, "Arial16");
            this.Vitality.LoadContent(content, "Arial16");
            this.Xp.LoadContent(content, "Arial16");
            this.XPNextLevel.LoadContent(content, "Arial16");
            this.StatsPoints.LoadContent(content, "Arial16");
            this.Gold.LoadContent(content, "Arial16");
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
            this.Character = Game1.characters.getSelectedCharacter();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

                if (isOver(mouse, Back))
                {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, TabMedium[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.TabSelected = 0;
                    Game1.characters.selectedCharacter = 0;
                    for (int i = 0; i < NB_SKILLS; i++)
                    {
                        this.SkillList[i].Color = Color.DarkSlateGray;
                    }
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, TabTank[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.TabSelected = 1;
                    Game1.characters.selectedCharacter = 1;
                    for (int i = 0; i < NB_SKILLS; i++)
                    {
                        this.SkillList[i].Color = Color.DarkSlateGray;
                    }
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, TabDPS[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.TabSelected = 2;
                    Game1.characters.selectedCharacter = 2;
                    for (int i = 0; i < NB_SKILLS; i++)
                    {
                        this.SkillList[i].Color = Color.DarkSlateGray;
                    }
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, TabCustom[0]))
                {
                    StartMenu.EffectClick.Play();
                    this.TabSelected = 3;
                    Game1.characters.selectedCharacter = 3;
                    for (int i = 0; i < NB_SKILLS; i++)
                    {
                        this.SkillList[i].Color = Color.DarkSlateGray;
                    }
                    this.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else if (isOver(mouse, Modify))
                {
                    StartMenu.EffectClick.Play();
                    Game1.GameState = GameState.ModifyCharacter;
                    this.ModifyCharacter.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                }
                else
                {
                    for (int i = 0; i < NB_SKILLS; i++)
                    {
                        if (this.SkillList[i].isOver(currentMouseState))
                        {
                            for (int j = 0; j < NB_SKILLS; j++)
                            {
                                if (((Skills)j).ToString() == this.SkillList[i].Text)
                                {
                                    if (this.Character.manageSkill(Skills.None, (Skills)j))
                                    {
                                        this.SkillList[i].Color = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                for (int i = 0; i < NB_SKILLS; i++)
                {
                    if (this.SkillList[i].isOver(currentMouseState))
                    {
                        for (int j = 0; j < NB_SKILLS; j++)
                        {
                            if (((Skills)j).ToString() == this.SkillList[i].Text)
                            {
                                if (this.Character.manageSkill((Skills)j, Skills.None))
                                {
                                    this.SkillList[i].Color = Color.DarkSlateGray;
                                }
                            }
                        }
                    }
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
            this.Character = Game1.characters.getSelectedCharacter();
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);


            this.TabMedium[0].Draw(spriteBatch, gameTime);
            this.TabTank[0].Draw(spriteBatch, gameTime);
            this.TabDPS[0].Draw(spriteBatch, gameTime);
            this.TabCustom[0].Draw(spriteBatch, gameTime);

            int tab = this.TabSelected;
            if (tab == 0)
            {
                this.TabMedium[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 1)
            {
                this.TabTank[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 2)
            {
                this.TabDPS[1].Draw(spriteBatch, gameTime);
            }
            else if (tab == 3)
            {
                this.TabCustom[1].Draw(spriteBatch, gameTime);
                this.Modify.Draw(spriteBatch, gameTime);
            }

            //Character Data
            this.LoadDataCharacter(this.Character);

            if (this.Character.Name == "Barbarian")
            {
                this.Character.setPosition(new Vector2(12 * Game1.UnitX, 5 * Game1.UnitY));
                this.Character.setScale(2);
                this.Character.Draw(spriteBatch);
            }
            else
            {
                this.Character.setPosition(new Vector2(14 * Game1.UnitX, 5 * Game1.UnitY));
                this.Character.setScale(2);
                this.Character.Draw(spriteBatch);
            }

            for (int i = 0; i < NB_SKILLS; i++)
            {
                this.SkillList[i].Draw(spriteBatch, gameTime);
            }
            this.ToolTip.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Ability.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);

            this.Xp.Draw(spriteBatch, gameTime);
            this.XPNextLevel.Draw(spriteBatch, gameTime);
            this.StatsPoints.Draw(spriteBatch, gameTime);
            this.Gold.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Loads the data character.
        /// </summary>
        /// <param name="character">The character.</param>
        public void LoadDataCharacter(PlayableCharacter character)
        {
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
            this.HP.Text = character.Health.ToString();
            this.Strength.Text = character.Attack.ToString();
            this.Vitality.Text = character.Vitality.ToString();
            this.Ability.Text = character.uniqueSkill.ToString();

            this.Xp.Text = character.xp.ToString();
            this.XPNextLevel.Text = character.xpToNextLevel().ToString();
            this.StatsPoints.Text = character.statPoints.ToString();
            this.Gold.Text = character.gold.ToString();

            for (int i = 0; i < NB_SKILLS; i++)
            {
                for (int j = 0; j < character.skills.Count; j++)
                {
                    if ((Skills)i == (character.skills.ElementAt<Skills>(j)))
                    {
                        this.SkillList[i].Color = Color.Black;
                    }
                }
            }
        }
    }
}
