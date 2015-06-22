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
        private int currentEnemy = 0;

        public Timer timer;
        public static long MillisecondsSinceLoadGame = 0;

        public override void Initialize()
        {
            this.HP = new TextSprite(5 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.White);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            MusicPlaying.Content = content;

            this.background = Content.Load<Texture2D>("BackgroundLevel");
            this.HP.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                StartMenu.EffectClick.Play();
                Game1.GameState = GameState.Pause;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Z) && previousKeyboardState.IsKeyUp(Keys.Z))
            {//Touche Z
                KeyPressed(0);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
            {//Touche Q
                KeyPressed(1);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
            {//Touche D
                KeyPressed(2);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
            {//Touche Space
                KeyPressed(3);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.LeftShift) && previousKeyboardState.IsKeyUp(Keys.LeftShift))
            {//Touche Shift
                KeyPressed(4);
            }
        }

        public override void Update(GameTime gametime)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            Game1.characters.getSelectedCharacter().UpdateFrame(elapsed);

            this.Monsters.ElementAt<AbstractCharacter>(this.currentEnemy).UpdateFrame(elapsed);


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
            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            tmp.setPosition(new Vector2(8 * Game1.UnitX, 7.75f * Game1.UnitY));
            tmp.setScale(0.7f);
            tmp.Draw(spriteBatch);

            this.Monsters.ElementAt<AbstractCharacter>(this.currentEnemy).Draw(spriteBatch);
                
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
            
            // Create monsters list
            this.Monsters = new List<AbstractCharacter>();
            Mob m = new Mob(1, 20, 1, 1, new Vector2(16 * Game1.UnitX, 7.75f * Game1.UnitY), 0.75f);
            m.Load(Content, "Spritesheet/Mob/Mob_Witch_Idle", "Spritesheet/Mob/Mob_Witch_Attack", 2, 4, 10);
            m.setOriginBottomLeft();
            this.Monsters.Add(m);
            

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
            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();
            AbstractCharacter c = this.Monsters.ElementAt<AbstractCharacter>(0);

            if(pressedKey == 0)
            {
                tmp.attackCharacter(c);

                if(c.isDead())
                {
                    this.currentEnemy++;
                    this.currentEnemy %= this.Monsters.Count;
                }
            }
            else
            {
                c.attackCharacter(tmp);

                this.HP.Text = tmp.Health.ToString() + " / " + this.HPStart.ToString();

                if (tmp.isDead())
                {
                    // Perdu
                    Game1.GameState = RythmRPG.GameState.Defeat;
                }
            }
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MusicPlaying.MillisecondsSinceLoadGame += 15;
        }

        // gametime.ElapsedGameTime.TotalMilliseconds
    }
}
