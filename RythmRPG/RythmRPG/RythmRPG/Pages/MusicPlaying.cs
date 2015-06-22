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
        private float noteLimitPositionX = 26.5f * Game1.UnitX;
        private const int NB_MOB = 2;
        private const int NB_BOSS = 1;

        private Texture2D background;

        private NAudio.Wave.WaveStream pcm = null;
        private bool firstUpdate = true;
        private int currentEnemy = 0;
        private int earnedXP = 0;
        private PlayableCharacter player;
        private float target = 25.5f * Game1.UnitX;//circle center
        private const double BASE_ALLOWED_ERROR_WIDTH = 0.2;//valid pressed key from -0.25sec to +0.25sec with Casual difficulty
        private const double ALLOWED_ERROR_WIDTH_COEFFICIENT = 0.05;

        public static ContentManager Content { get; set; }
        public bool IsLoading { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsFinished { get; set; }
        public List<AbstractCharacter> Monsters { get; set; }
        public TextSprite HP { get; set; }
        public int HPStart { get; set; }
        public Sprite InputsSprite { get; set; }
        public TextSprite[] Skills { get; set; }
        public TextSprite Ability { get; set; }
        public List<Sprite> LinesSprite { get; set; }
        public List<Sprite> Circles { get; set; }
        internal Queue<Note>[] LinesNotes { get; set; }
        public SortedSet<double>[] SSNotes { get; set; }
        public SortedSet<double>[] SSNotes2 { get; set; }
        public TimeSpan span { get; set; }
        public double AllowedError { get; set; }

        public static float LengthSpeedUnit;
        public static NAudio.Wave.DirectSoundOut output = null;
        public static NAudio.Wave.BlockAlignReductionStream stream = null;


        public AfterGame Victory { get; set; }
        public AfterGame Defeat { get; set; }

        public override void Initialize()
        {
            this.HP = new TextSprite(5 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.White);
            this.InputsSprite = new Sprite(27 * Game1.UnitX, 11 * Game1.UnitY, 3 * Game1.UnitX, 7 * Game1.UnitY);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            MusicPlaying.Content = content;

            this.background = Content.Load<Texture2D>("BackgroundLevel");
            this.HP.LoadContent(content, "Arial16");
            this.InputsSprite.LoadContent(content, "MusicPlaying/inputs");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                output.Pause();
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
                if ((int)Game1.Difficulty > 0) KeyPressed(3);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.LeftShift) && previousKeyboardState.IsKeyUp(Keys.LeftShift))
            {//Touche Shift
                if ((int)Game1.Difficulty > 1) KeyPressed(4);
            }
        }

        public override void Update(GameTime gametime)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            Game1.characters.getSelectedCharacter().UpdateFrame(elapsed);

            this.Monsters.ElementAt<AbstractCharacter>(this.currentEnemy).UpdateFrame(elapsed);


            if (firstUpdate)
            {
                output.Play();
                firstUpdate = false;
            }

            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                short finishedLine = 0;
                double currentSeconds = pcm.CurrentTime.TotalSeconds;
                if (SSNotes2[i].Min <= MusicPlaying.LengthSpeedUnit)
                {
                    SSNotes2[i].Remove(SSNotes2[i].Min);//too soon beats are discarded

                }
                else if (SSNotes2[i].Count > 0 && SSNotes2[i].Min - LengthSpeedUnit < currentSeconds + 0.02 && currentSeconds - 0.02 < SSNotes2[i].Min - LengthSpeedUnit)//add note to the line
                {

                    this.LinesNotes[i].Enqueue(new Note(SSNotes2[i].Min, i));//throwing the note to the player
                    SSNotes2[i].Remove(SSNotes2[i].Min);

                }
                else if (SSNotes2[i].Count == 0)//no more note on line i
                {
                    finishedLine++;
                }


                if (span.TotalSeconds < currentSeconds)//song finished
                {
                    IsFinished = true;
                    output.Stop();
                    this.Victory.Xp.Text = string.Format("You survived !\r\n you won {0} XP", this.earnedXP);
                    Game1.GameState = RythmRPG.GameState.Victory;
                }


                if (this.LinesNotes[i].Count != 0 && this.LinesNotes[i].Peek().Position.X > noteLimitPositionX)//remove note when out of the line
                {
                    AbstractCharacter monster = this.Monsters.ElementAt<AbstractCharacter>(0);
                    monster.attackCharacter(this.player);

                    this.HP.Text = player.Health.ToString() + " / " + this.HPStart.ToString();

                    if (this.player.isDead())
                    {
                        output.Stop();
                        Game1.GameState = RythmRPG.GameState.Defeat;
                    }

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

            this.InputsSprite.Draw(spriteBatch, gameTime);
            //Console.WriteLine(gameTime.TotalGameTime.TotalMilliseconds);
        }

        public void LoadDataCharacter(PlayableCharacter character)
        {
            if (this.HP == null)
            {
                this.HP = new TextSprite(5 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.White);
                this.InputsSprite = new Sprite(27 * Game1.UnitX, 11 * Game1.UnitY, 3 * Game1.UnitX, 7 * Game1.UnitY);
            }
            this.HPStart = character.Health;

            this.HP.Text = character.Health.ToString() + " / " + this.HPStart.ToString();
        }

        public void LoadGame()
        {
            IsLoading = true;
            IsFinished = false;
            firstUpdate = true;

            //player's charachter
            this.player = Game1.characters.getSelectedCharacter();

            //AudioProcessing
            AllowedError = MusicPlaying.BASE_ALLOWED_ERROR_WIDTH + MusicPlaying.ALLOWED_ERROR_WIDTH_COEFFICIENT * (double)Game1.Difficulty;

            float speed = Note.DEFAULT_BASE_SPEED + Note.DEFAULT_SPEED_COEFFICIENT * (float)Game1.Difficulty;//per millisec
            float lineLength = 25 * Game1.UnitX;

            AllowedError *= speed * 1000.0;
            MusicPlaying.LengthSpeedUnit = lineLength / speed / 1000;//in second

            string wavFilePath = Game1.CurrentSelectedWavFile;
            this.SSNotes = new SortedSet<double>[Chart.LaneNumber];//raw(not scaled)
            this.SSNotes2 = new SortedSet<double>[Chart.LaneNumber];//scaled
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                this.SSNotes[i] = new SortedSet<double>();
                this.SSNotes2[i] = new SortedSet<double>();
            }
            this.SSNotes = Chart.getSetArray(AudioAnalyser.DetectBeat(wavFilePath), Game1.Difficulty);


            double max = 0;
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                double tmp = this.SSNotes[i].Max;
                if (tmp > max) max = tmp;
            }

            this.LinesNotes = new Queue<Note>[Chart.LaneNumber];
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                this.LinesNotes[i] = new Queue<Note>();
            }

            //Circles
            this.Circles = new List<Sprite>();
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                Sprite circle = new Sprite(this.target, (12 + i) * Game1.UnitY, Game1.UnitX, Game1.UnitY);
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
            Vector2 position = new Vector2(25f * Game1.UnitX, 12.5f * Game1.UnitY);
            Mob mob;
            Boss boss;

            for (int i = 0; i < NB_MOB; i++)
            {
                mob = new Mob(Game1.Difficulty, position, 0.75f);
                mob.Load(Content);
                mob.setOriginBottomRight();
                this.Monsters.Add(mob);
            }
            for (int i = 0; i < NB_BOSS; i++)
            {
                boss = new Boss(Game1.Difficulty, position, 0.75f);
                boss.Load(Content);
                boss.setOriginBottomRight();
                this.Monsters.Add(boss);
            }


            //play the selected music
            NAudio.Wave.WaveFileReader reader = new NAudio.Wave.WaveFileReader(Game1.CurrentSelectedWavFile);
            this.pcm = new NAudio.Wave.WaveChannel32(reader);
            MusicPlaying.stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            output = new NAudio.Wave.DirectSoundOut();
            output.Init(stream);


            this.span = reader.TotalTime;
            for (int i = 0; i < Chart.LaneNumber; i++)
            {
                foreach (double beat in SSNotes[i])
                {
                    SSNotes2[i].Add(beat * span.TotalSeconds / max);//scale the beats with the real audio stream length
                }
            }

            this.IsLoaded = true;
        }
        public void KeyPressed(int pressedKey)
        {
            AbstractCharacter monster = this.Monsters.ElementAt<AbstractCharacter>(this.currentEnemy);
            bool goodHit = false;

            if (this.LinesNotes[pressedKey].Count == 0) goodHit = false;
            else if (Math.Abs(this.LinesNotes[pressedKey].Peek().Position.X - this.target) <= this.AllowedError)
            {
                goodHit = true;
                this.LinesNotes[pressedKey].Dequeue();
            }

            if (goodHit)
            {
                player.attackCharacter(monster);

                if (monster.isDead())
                {
                    this.currentEnemy++;
                    this.currentEnemy %= this.Monsters.Count;

                    this.earnedXP += monster.giveXP();
                    monster.prepareForMusic();
                }
            }
            else
            {
                monster.attackCharacter(player);

                this.HP.Text = player.Health.ToString() + " / " + this.HPStart.ToString();

                if (player.isDead())
                {
                    output.Stop();
                    Game1.GameState = RythmRPG.GameState.Defeat;
                }
            }
        }

    }
}
