using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RythmRPG.Pages;
using RythmRPG.Character;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RythmRPG {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        /// <summary>
        /// The graphics
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// The sprite batch
        /// </summary>
        SpriteBatch spriteBatch;


        /// <summary>
        /// The background
        /// </summary>
        private Texture2D background;

        /// <summary>
        /// The default windows width
        /// </summary>
        public const int DEFAULT_WINDOWS_WIDTH = 1280;
        /// <summary>
        /// The default windows height
        /// </summary>
        public const int DEFAULT_WINDOWS_HEIGHT = 720;

        /// <summary>
        /// The save file name
        /// </summary>
        public static string saveFileName;
        /// <summary>
        /// The content
        /// </summary>
        private static ContentManager content;
        /// <summary>
        /// The characters
        /// </summary>
        public static Characters characters = new Characters();

        //Options
        /// <summary>
        /// The game state
        /// </summary>
        public static GameState GameState;
        /// <summary>
        /// The selected theme
        /// </summary>
        public static int SelectedTheme;
        /// <summary>
        /// The volume music
        /// </summary>
        public static int VolumeMusic = 10;
        /// <summary>
        /// The volume sound
        /// </summary>
        public static int VolumeSound = 10;
        /// <summary>
        /// The volume menu
        /// </summary>
        public static int VolumeMenu = 5;
        /// <summary>
        /// The save
        /// </summary>
        public static Save Save;
        /// <summary>
        /// The difficulty
        /// </summary>
        public static Difficulty Difficulty;

        /// <summary>
        /// The wav file directory
        /// </summary>
        public static string WavFileDirectory = "musicDir/";
        /// <summary>
        /// The is challenge mode
        /// </summary>
        public static bool IsChallengeMode = false;
        /// <summary>
        /// The current selected wav file
        /// </summary>
        public static string CurrentSelectedWavFile = "";
        /// <summary>
        /// The width
        /// </summary>
        public static int Width;
        /// <summary>
        /// The height
        /// </summary>
        public static int Height;
        /// <summary>
        /// The unit x
        /// </summary>
        public static int UnitX;
        /// <summary>
        /// The unit y
        /// </summary>
        public static int UnitY;
        /// <summary>
        /// The button width
        /// </summary>
        public static int ButtonWidth;
        /// <summary>
        /// The button height
        /// </summary>
        public static int ButtonHeight;

        /// <summary>
        /// Gets or sets the state of the current mouse.
        /// </summary>
        /// <value>
        /// The state of the current mouse.
        /// </value>
        public MouseState CurrentMouseState { get; set; }
        /// <summary>
        /// Gets or sets the state of the previous mouse.
        /// </summary>
        /// <value>
        /// The state of the previous mouse.
        /// </value>
        public MouseState PreviousMouseState { get; set; }
        /// <summary>
        /// Gets or sets the state of the current key board.
        /// </summary>
        /// <value>
        /// The state of the current key board.
        /// </value>
        public KeyboardState CurrentKeyBoardState { get; set; }
        /// <summary>
        /// Gets or sets the state of the previous key board.
        /// </summary>
        /// <value>
        /// The state of the previous key board.
        /// </value>
        public KeyboardState PreviousKeyBoardState { get; set; }
        /// <summary>
        /// Gets or sets the start menu.
        /// </summary>
        /// <value>
        /// The start menu.
        /// </value>
        public StartMenu StartMenu { get; set; }
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public Options Options { get; set; }
        /// <summary>
        /// Gets or sets the game menu.
        /// </summary>
        /// <value>
        /// The game menu.
        /// </value>
        public GameMenu GameMenu { get; set; }
        /// <summary>
        /// Gets or sets the single music.
        /// </summary>
        /// <value>
        /// The single music.
        /// </value>
        public SingleMusic SingleMusic { get; set; }
        /// <summary>
        /// Gets or sets the playlist challenge.
        /// </summary>
        /// <value>
        /// The playlist challenge.
        /// </value>
        public PlaylistChallenge PlaylistChallenge { get; set; }
        /// <summary>
        /// Gets or sets the character management.
        /// </summary>
        /// <value>
        /// The character management.
        /// </value>
        public CharacterManagement CharacterManagement { get; set; }
        /// <summary>
        /// Gets or sets the victory.
        /// </summary>
        /// <value>
        /// The victory.
        /// </value>
        public AfterGame Victory { get; set; }
        /// <summary>
        /// Gets or sets the song victory.
        /// </summary>
        /// <value>
        /// The song victory.
        /// </value>
        public SongVictory SongVictory { get; set; }
        /// <summary>
        /// Gets or sets the playlist defeat.
        /// </summary>
        /// <value>
        /// The playlist defeat.
        /// </value>
        public PlaylistDefeat PlaylistDefeat { get; set; }
        /// <summary>
        /// Gets or sets the playlist victory.
        /// </summary>
        /// <value>
        /// The playlist victory.
        /// </value>
        public AfterGame PlaylistVictory { get; set; }
        /// <summary>
        /// Gets or sets the pause.
        /// </summary>
        /// <value>
        /// The pause.
        /// </value>
        public Pause Pause { get; set; }
        /// <summary>
        /// Gets or sets the defeat.
        /// </summary>
        /// <value>
        /// The defeat.
        /// </value>
        public AfterGame Defeat { get; set; }
        /// <summary>
        /// Gets or sets the music playing.
        /// </summary>
        /// <value>
        /// The music playing.
        /// </value>
        public MusicPlaying MusicPlaying { get; set; }
        /// <summary>
        /// Gets or sets the modify character.
        /// </summary>
        /// <value>
        /// The modify character.
        /// </value>
        public ModifyCharacter ModifyCharacter { get; set; }
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = DEFAULT_WINDOWS_WIDTH;
            graphics.PreferredBackBufferHeight = DEFAULT_WINDOWS_HEIGHT;
            Content.RootDirectory = "Content";
            content = Content;
            

            Width = DEFAULT_WINDOWS_WIDTH;
            Height = DEFAULT_WINDOWS_HEIGHT;

            UnitX = Game1.Width / 32;
            UnitY = Game1.Height / 18;
            ButtonWidth = 8 * Game1.Width / 32;
            ButtonHeight = 1 * Game1.Height / 18;

            Save = new Save();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            GameState = RythmRPG.GameState.StartMenu;

            this.StartMenu = new StartMenu();
            this.StartMenu.Initialize();
            this.Options = new Options();
            this.Options.Initialize();
            this.GameMenu = new GameMenu();
            this.GameMenu.Initialize();
            this.CharacterManagement = new CharacterManagement();
            this.CharacterManagement.Initialize();
            this.SingleMusic = new SingleMusic();
            this.SingleMusic.Initialize();
            this.PlaylistChallenge = new PlaylistChallenge();
            this.PlaylistChallenge.Initialize();
            this.Victory = new AfterGame("Victory","You win XX XP");
            this.Victory.Initialize();
            this.SongVictory = new SongVictory();
            this.SongVictory.Initialize();
            this.PlaylistDefeat = new PlaylistDefeat("PlaylistDefeat", "Loser !");
            this.PlaylistDefeat.Initialize();
            this.PlaylistVictory = new AfterGame("PlaylistVictory", "You win XX XP");
            this.PlaylistVictory.Initialize();
            this.Pause = new Pause();
            this.Pause.Initialize();
            this.Defeat = new AfterGame("Defeat", "You loose");
            this.Defeat.Initialize();
            this.MusicPlaying = new MusicPlaying();
            this.MusicPlaying.Initialize();
            this.ModifyCharacter = new ModifyCharacter();
            this.ModifyCharacter.Initialize();

            this.SingleMusic.MusicPlaying = this.MusicPlaying;
            this.PlaylistChallenge.MusicPlaying = this.MusicPlaying;
            this.CharacterManagement.ModifyCharacter = this.ModifyCharacter;
            this.MusicPlaying.Defeat = this.Defeat;
            this.MusicPlaying.Victory = this.Victory;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            this.background = Content.Load<Texture2D>("MainBackground");

            this.StartMenu.LoadContent(this.Content);
            this.Options.LoadContent(this.Content);
            this.GameMenu.LoadContent(this.Content);
            this.CharacterManagement.LoadContent(this.Content);
            this.SingleMusic.LoadContent(this.Content);
            this.PlaylistChallenge.LoadContent(this.Content);
            this.Victory.LoadContent(this.Content);
            this.SongVictory.LoadContent(this.Content);
            this.PlaylistDefeat.LoadContent(this.Content);
            this.PlaylistVictory.LoadContent(this.Content);
            this.Pause.LoadContent(this.Content);
            this.Defeat.LoadContent(this.Content);
            this.MusicPlaying.LoadContent(this.Content);
            this.ModifyCharacter.LoadContent(this.Content);

            
            
            //Inputs
            this.CurrentKeyBoardState = Keyboard.GetState();
            this.PreviousKeyBoardState = this.CurrentKeyBoardState;
            this.CurrentMouseState = Mouse.GetState();
            this.PreviousMouseState = this.CurrentMouseState;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Inputs
            this.PreviousMouseState = this.CurrentMouseState;
            this.CurrentMouseState = Mouse.GetState();
            this.PreviousKeyBoardState = this.CurrentKeyBoardState;
            this.CurrentKeyBoardState = Keyboard.GetState();

            // TODO: Add your update logic here
            switch (GameState) {
                case GameState.StartMenu:
                    this.StartMenu.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.Options:
                    this.Options.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.GameMenu:
                    this.GameMenu.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.CharacterManagement:
                    this.CharacterManagement.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.SingleMusic:
                    this.SingleMusic.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.PlaylistChallenge:
                    this.PlaylistChallenge.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.Victory:
                    this.Victory.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.SongVictory:
                    this.SongVictory.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.PlaylistDefeat:
                    this.PlaylistDefeat.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.PlaylistVictory:
                    this.PlaylistVictory.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.Pause:
                    this.Pause.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.Defeat:
                    this.Defeat.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.MusicPlaying:
                    this.MusicPlaying.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    this.MusicPlaying.Update(gameTime);
                    break;
                case RythmRPG.GameState.ModifyCharacter:
                    this.ModifyCharacter.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    break;
                case RythmRPG.GameState.Exit:
                    this.Exit();
                    break;
            };

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 0.70f, SpriteEffects.None, 0);
            this.spriteBatch.End();

            switch (GameState) {
                case GameState.StartMenu:
                    this.StartMenu.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.Options:
                    this.Options.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.GameMenu:
                    this.GameMenu.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.CharacterManagement:
                    this.CharacterManagement.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.SingleMusic:
                    this.SingleMusic.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.PlaylistChallenge:
                    this.PlaylistChallenge.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.Victory:
                    this.Victory.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.SongVictory:
                    this.SongVictory.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.PlaylistDefeat:
                    this.PlaylistDefeat.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.PlaylistVictory:
                    this.PlaylistVictory.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.Pause:
                    this.Pause.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.Defeat:
                    this.Defeat.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.MusicPlaying:
                    this.MusicPlaying.Draw(spriteBatch, gameTime);
                    break;
                case RythmRPG.GameState.ModifyCharacter:
                    this.ModifyCharacter.Draw(spriteBatch, gameTime);
                    break;
            };

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            if(saveFileName != null)
            {
                IFormatter format = new BinaryFormatter();
                Stream stream;

                try
                {
                    stream = new FileStream("./Save/" + saveFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                }
                catch (DirectoryNotFoundException e)
                {
                    DirectoryInfo di = Directory.CreateDirectory("./Save");
                    stream = new FileStream("./Save/" + saveFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                }

                for (int i = 0; i < characters.NbCharacter; i++)
                {
                    characters.selectedCharacter = i;
                    characters.getSelectedCharacter().prepareForMusic();
                }
                characters.selectedCharacter = 0;
                format.Serialize(stream, characters);
                stream.Close();
            }

            base.OnExiting(sender, args);
        }

        public static void LoadCharacters()
        {
            
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("./Save/" + saveFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                characters = (Characters)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception e) // i.e. the file doesn't exist
            {
                characters.CreateDataCharacters();
            }

            characters.LoadContent(content);
            for(int i=0;i<4;i++)
            {
                string tmp = characters.characterArray[i].Name;
            }

            characters.selectedCharacter = 0;
        }
    }
}