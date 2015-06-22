using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;

namespace RythmRPG.Pages
{
    public class MusicPlaying : Page
    {
        public static float NOTE_LIMIT_POSITIONX = 26.5f * Game1.UnitX;

        private Texture2D background;
        private NAudio.Wave.BlockAlignReductionStream stream = null;
        private NAudio.Wave.DirectSoundOut output = null;

        public static ContentManager Content { get; set; }
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public CharacterSprites[] SpriteCharacters { get; set; }
        public List<AbstractCharacter> Monsters { get; set; }
        public TextSprite HP { get; set; }
        public int HPStart { get; set; }
        public Difficulty Difficulty { get; set; }
        public TextSprite[] Skills { get; set; }
        public TextSprite Ability { get; set; }
        public List<Sprite> LinesSprite { get; set; }
        public List<Sprite> Circles { get; set; }
        internal Queue<Note>[] LinesNotes { get; set; }
        public SortedSet<double>[] SSNotes { get; set; }
        public float LengthSpeedUnit { get; set; }

        public Timer timer;
        public static long MillisecondsSinceLoadGame = 0;

        public override void Initialize()
        {


            //Character Data
            
            this.SpriteCharacters = new CharacterSprites[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++)
            {
                if(i == 1)
                {
                    this.SpriteCharacters[i] = new CharacterSprites(new Vector2(8 * Game1.UnitX, 7.75f * Game1.UnitY), 0, 0.7f, 0);
                }
                else if(i == 2)
                {
                    this.SpriteCharacters[i] = new CharacterSprites(new Vector2(8 * Game1.UnitX, 8.5f * Game1.UnitY), 0, 0.7f, 0);
                }
                else
                {
                    this.SpriteCharacters[i] = new CharacterSprites(new Vector2(8 * Game1.UnitX, 8 * Game1.UnitY), 0, 0.7f, 0);
                }
            }
            this.HP = new TextSprite(5 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.White);

        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            MusicPlaying.Content = content;

            //this.MainImage.LoadContent(content, "MusicPlaying/MusicPlaying");
            this.background = Content.Load<Texture2D>("BackgroundLevel");
            //Character Data
            
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++)
            {
                string name = Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].Name;
                this.SpriteCharacters[i].Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2, 4, 10);
            }
            this.HP.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState)
        {

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
            //MusicPlaying.MillisecondsSinceLoadGame += (int)gametime.ElapsedGameTime.TotalMilliseconds;

            for (int i = 0; i < this.SpriteCharacters.Length; i++)
            {
                this.SpriteCharacters[i].UpdateFrame((float)gametime.ElapsedGameTime.TotalSeconds);
            }

            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                if (SSNotes[i].Count > 0 && SSNotes[i].Min < (MusicPlaying.MillisecondsSinceLoadGame - this.LengthSpeedUnit) / 1000.0)//add note to the line
                {
                    this.LinesNotes[i].Enqueue(new Note(SSNotes[i].Min, i));
                    SSNotes[i].Remove(SSNotes[i].Min);
                    }
                if (this.LinesNotes[i].Count != 0 && this.LinesNotes[i].Peek().Position.X > NOTE_LIMIT_POSITIONX)//remove note when out of the line
                {
                    //MonsterAttack;
                    this.LinesNotes[i].Dequeue();
                }
                foreach (Note note in this.LinesNotes[i])
                {
                    note.Update(gametime);
            }
        }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 0.70f, SpriteEffects.None, 0);
            spriteBatch.End();

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].DrawFrame(spriteBatch);
            this.HP.Draw(spriteBatch, gameTime);

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

            //Console.WriteLine(gameTime.TotalGameTime.TotalMilliseconds);
        }

        public void LoadDataCharacter(PlayableCharacter character)
        {
            this.HPStart = character.Health;
            
            this.HP.Text = character.Health.ToString() + " / " + this.HPStart.ToString();
        }

        public void LoadGame()
        {

            this.timer = new Timer();
            this.timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            this.timer.Interval = 9;
            this.timer.Enabled = true;

            string wavFilePath = Game1.CurrentSelectedWavFile;
            this.SSNotes = new SortedSet<double>[Chart.LaneNumber];
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                this.SSNotes[i] = new SortedSet<double>();
            }
            this.SSNotes = Chart.getSetArray(AudioAnalyser.DetectBeat(wavFilePath), Game1.Difficulty);
            float speed = 0.3f + 0.1f * (float)Game1.Difficulty;//per millisec
            float lineLength = 25 * Game1.UnitX;
            this.LengthSpeedUnit = lineLength / speed;//millisec


            this.LinesNotes = new Queue<Note>[Chart.LaneNumber];
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                this.LinesNotes[i] = new Queue<Note>();
            }
            
            //Circles
            this.Circles = new List<Sprite>();
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                Sprite circle = new Sprite(25.5f * Game1.UnitX, (12 + i) * Game1.UnitY, Game1.UnitX, Game1.UnitY);
                this.Circles.Add(circle);
                this.Circles[i].LoadContent(MusicPlaying.Content, "MusicPlaying/noteCircle");
            }
            //LinesSprite
            this.LinesSprite = new List<Sprite>();
            
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                Sprite oneString = new Sprite(Game1.UnitX, (12.5f + i) * Game1.UnitY, 26 * Game1.UnitX, 1);
                this.LinesSprite.Add(oneString);
                this.LinesSprite[i].LoadContent(MusicPlaying.Content, "Options/One");
            }

            //play the selected music
            NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(Game1.CurrentSelectedWavFile));
            stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            output = new NAudio.Wave.DirectSoundOut();
            output.Init(stream);
            output.Play();

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

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MusicPlaying.MillisecondsSinceLoadGame += 15;
        }

        // gametime.ElapsedGameTime.TotalMilliseconds
    }
}
