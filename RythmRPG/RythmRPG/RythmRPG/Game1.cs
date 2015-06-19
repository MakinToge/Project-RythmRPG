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

namespace RythmRPG {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int DEFAULT_WINDOWS_WIDTH = 1280;
        public const int DEFAULT_WINDOWS_HEIGHT = 720;
        
        public static GameState GameState;
        public static int VolumeMusic = 4;
        public static int VolumeSound = 4;
        public static Save Save;

        public static int Width;
        public static int Height;
        public static int UnitX;
        public static int UnitY;
        public static int ButtonWidth;
        public static int ButtonHeight;

        public MouseState CurrentMouseState { get; set; }
        public MouseState PreviousMouseState { get; set; }
        public KeyboardState CurrentKeyBoardState { get; set; }
        public KeyboardState PreviousKeyBoardState { get; set; }
        public StartMenu StartMenu { get; set; }
        public Options Options { get; set; }
        public GameMenu GameMenu { get; set; }
        public SingleMusic SingleMusic { get; set; }
        public PlaylistChallenge PlaylistChallenge { get; set; }
        public CharacterManagement CharacterManagement { get; set; }
        public AfterGame Victory { get; set; }
        public SongVictory SongVictory { get; set; }
        public PlaylistDefeat PlaylistDefeat { get; set; }
        public AfterGame PlaylistVictory { get; set; }
        public Pause Pause { get; set; }
        public AfterGame Defeat { get; set; }
        public MusicPlaying MusicPlaying { get; set; }
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = DEFAULT_WINDOWS_WIDTH;
            graphics.PreferredBackBufferHeight = DEFAULT_WINDOWS_HEIGHT;
            Content.RootDirectory = "Content";

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

            this.SingleMusic.MusicPlaying = this.MusicPlaying;
            this.PlaylistChallenge.MusicPlaying = this.MusicPlaying;
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

            //A Supprimer (Juste pour tester les pages)
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.A)) {
                Game1.GameState = RythmRPG.GameState.Defeat;
            }
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.Z)) {
                Game1.GameState = RythmRPG.GameState.PlaylistDefeat;
            }
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.E)) {
                Game1.GameState = RythmRPG.GameState.PlaylistVictory;
            }
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.R)) {
                Game1.GameState = RythmRPG.GameState.SongVictory;
            }
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.T)) {
                Game1.GameState = RythmRPG.GameState.Victory;
            }
            if (this.CurrentKeyBoardState.IsKeyDown(Keys.Y)) {
                Game1.GameState = RythmRPG.GameState.Pause;
            }

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
            };

            base.Draw(gameTime);
        }
    }
}