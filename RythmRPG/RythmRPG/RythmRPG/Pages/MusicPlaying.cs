using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages
{
    public class MusicPlaying : Page
    {
        public const int NOTE_LIMIT_POSITIONX = 29 * Game1.UnitX;

        private Texture2D background;

        public static ContentManager Content { get; set; }
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public Sprite MainImage { get; set; }
        public CharacterSprites[] SpriteCharacters { get; set; }
        public List<AbstractCharacter> Monsters { get; set; }
        public TextSprite Name { get; set; }
        public TextSprite Level { get; set; }
        public TextSprite Endurance { get; set; }
        public TextSprite HP { get; set; }
        public int HPStart { get; set; }
        public TextSprite Strength { get; set; }
        public Difficulty Difficulty { get; set; }
        public TextSprite[] Skills { get; set; }
        public TextSprite Ability { get; set; }
        public List<Sprite> LinesSprite { get; set; }
        public List<Sprite> Circles { get; set; }
        public Queue<Note>[] LinesNotes { get; set; }
        public SortedSet<double>[] SSNotes { get; set; }
        public float LengthSpeedUnit { get; set; }

        public override void Initialize()
        {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);

            //Character Data

            this.SpriteCharacters = new CharacterSprites[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++)
            {
                this.SpriteCharacters[i] = new CharacterSprites(new Vector2(17 * Game1.UnitX, 2 * Game1.UnitY), 0, 1f, 0);
            }
            this.Name = new TextSprite(17 * Game1.UnitX, 1f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(26 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(26 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(25 * Game1.UnitX, 3.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(26 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);

            this.Ability = new TextSprite(28 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);


        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            MusicPlaying.Content = content;

            this.MainImage.LoadContent(content, "MusicPlaying/MusicPlaying");
            this.background = Content.Load<Texture2D>("BackgroundLevel");
            //Character Data

            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++)
            {
                string name = Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].Name;
                this.SpriteCharacters[i].Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2, 4, 10);
            }

            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Ability.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState)
        {
            /*
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
            }
            */
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                StartMenu.EffectClick.Play();
                Game1.GameState = GameState.Pause;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Z))
            {//Touche Z
                KeyPressed(0);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Q))
            {//Touche Q
                KeyPressed(1);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {//Touche D
                KeyPressed(2);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Space))
            {//Touche Space
                KeyPressed(3);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {//Touche Shift
                KeyPressed(4);
            }
        }

        public override void Update(GameTime gametime)
        {
            for (int i = 0; i < this.SpriteCharacters.Length; i++)
            {
                this.SpriteCharacters[i].UpdateFrame((float)gametime.ElapsedGameTime.TotalSeconds);
            }

            float timer1 = 1.0f;
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                if (SSNotes[i].Count > 0 && SSNotes[i].Min < timer1 - this.LengthSpeedUnit)//add note to the line
                {
                    this.LinesNotes[i].Enqueue(new Note(SSNotes[i].Min, i));
                    SSNotes[i].Remove(SSNotes[i].Min);
                }
                if (this.LinesNotes[i].Peek().Position.X > NOTE_LIMIT_POSITIONX)//remove note when out of the line
                {
                    //MonsterAttack;
                    this.LinesNotes[i].Dequeue();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 0.70f, SpriteEffects.None, 0);
            spriteBatch.End();
            this.MainImage.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].DrawFrame(spriteBatch);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Ability.Draw(spriteBatch, gameTime);

            //Draw la position des notes
            foreach (Queue<Note> line in LinesNotes)
            {
                foreach (Note note in line)
                {
                    note.Draw(spriteBatch, gameTime);
                }
            }
            //Draw circles
            foreach (Sprite item in Circles)
            {
                item.Draw(spriteBatch, gameTime);
            }

            //Draw LinesSprite
            foreach (Sprite item in this.LinesSprite)
            {
                item.Draw(spriteBatch, gameTime);
            }
        }

        public void LoadDataCharacter(PlayableCharacter character)
        {
            this.HPStart = character.Health;
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
            this.HP.Text = character.Health.ToString() + " / " + this.HPStart.ToString();
            this.Strength.Text = character.Attack.ToString();
            this.Ability.Text = character.uniqueSkill.ToString();
        }

        public void LoadGame()
        {
            string wavFilePath = "";
            this.SSNotes = new SortedSet<double>[Chart.LaneNumber];
            this.SSNotes = Chart.getSetArray(AudioAnalyser.DetectBeat(wavFilePath), Game1.Difficulty);
            float speed = 0.3f + 0.1f * (float)Game1.Difficulty;//per millisec
            float lineLength = 25 * Game1.UnitX;
            this.LengthSpeedUnit = lineLength / speed;//millisec

            /*
             * timer1 = 0
             * timer2 = lengthSpeedUnit
             * 
             * 
             */
            this.LinesNotes = new Queue<Note>[Chart.LaneNumber];

            //Circles
            this.Circles = new List<Sprite>();
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                Sprite circle = new Sprite(28 * Game1.UnitX, (11 + i) * Game1.UnitY, Game1.UnitX, Game1.UnitY);
                this.Circles.Add(circle);
                this.Circles[i].LoadContent(MusicPlaying.Content, "MusicPlaying/noteCircle");
            }
            //LinesSprite
            this.LinesSprite = new List<Sprite>();

            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                Sprite oneString = new Sprite(2 * Game1.UnitX, (11.5f + i) * Game1.UnitY, 27 * Game1.UnitX, 1);
                this.LinesSprite.Add(oneString);
                this.LinesSprite[i].LoadContent(MusicPlaying.Content, "Options/One");
            }

            //Juste pour l'exemple
            //for (int i = 0; i < Chart.LaneNumber; i++)
            //{
            //    this.AddNote(i);
            //}

            this.isLoaded = true;
        }
        public void KeyPressed(int pressedKey)
        {
            /*
             * timer1 = X
             * timer2 = X + lengthSpeedUnit
             * 
             * 
             * 
             */
        }

    }
}
