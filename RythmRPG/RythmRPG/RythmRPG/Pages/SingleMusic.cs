using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RythmRPG.Pages {
    public class SingleMusic : Page{


        public Sprite MainImage { get; set; }
        public Sprite Back { get; set; }
        public TextSprite WaitPlease { get; set; }
        public Sprite[] Casual { get; set; }
        public Sprite[] Veteran { get; set; }
        public Sprite[] GodLike { get; set; }
        public int SelectedDifficulty { get; set; }
        public Sprite Play { get; set; }
        public Sprite NotPlay { get; set; }
        public Sprite ChooseMusic { get; set; }
        public MusicPlaying MusicPlaying { get; set; }
        public bool IsMusicChosen { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            

            this.Casual = new Sprite[2] {
                new Sprite(Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.Veteran = new Sprite[2] {
                new Sprite(Game1.UnitX, 6 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 6 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.GodLike = new Sprite[2] {
                new Sprite(Game1.UnitX, 7 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 7 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };

            this.SelectedDifficulty = 0;

            this.NotPlay = new Sprite(11 * Game1.UnitX, 14 * Game1.UnitY, 7 * Game1.UnitX, 2*Game1.UnitY);
            this.Play = new Sprite(11 * Game1.UnitX, 14 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.ChooseMusic = new Sprite(11 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.WaitPlease = new TextSprite(12 * Game1.UnitX, 6 * Game1.UnitY, "It might take some time", Color.Black);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "SingleMusic/SingleMusic");
            this.Back.LoadContent(content, "Options/Back");

            this.Casual[0].LoadContent(content, "SingleMusic/Casual");
            this.Casual[1].LoadContent(content, "SingleMusic/Selected/Casual");
            this.Veteran[0].LoadContent(content, "SingleMusic/Veteran");
            this.Veteran[1].LoadContent(content, "SingleMusic/Selected/Veteran");
            this.GodLike[0].LoadContent(content, "SingleMusic/GodLike");
            this.GodLike[1].LoadContent(content, "SingleMusic/Selected/GodLike");

            this.NotPlay.LoadContent(content, "SingleMusic/Play!2");
            this.Play.LoadContent(content, "SingleMusic/Play!");
            this.ChooseMusic.LoadContent(content, "SingleMusic/ChooseMusic");
            this.WaitPlease.LoadContent(content, "Arial16");
            
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && previousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
                string choosedFile = "";
                if (isOver(mouse, Back)) {
                    StartMenu.EffectBack.Play();
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, Casual[0])) {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 0;
                    Game1.Difficulty = Difficulty.Casual;
                }
                else if (isOver(mouse, Veteran[0])) {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 1;
                    Game1.Difficulty = Difficulty.Veteran;
                }
                else if (isOver(mouse, GodLike[0])) {
                    StartMenu.EffectClick.Play();
                    this.SelectedDifficulty = 2;
                    Game1.Difficulty = Difficulty.GodLike;
                }
                else if (isOver(mouse, ChooseMusic)) {//Clique sur Choose Music
                    StartMenu.EffectClick.Play();

                    OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
                    open.Filter = "MP3 File (*.mp3)|*.mp3;";
                    open.Multiselect = false;
                    if (open.ShowDialog() != DialogResult.OK) return;
                    choosedFile = open.FileName;
                    string wavFile;
                    Resampler.Resampling(choosedFile, Game1.WavFileDirectory ,Resampler.RESAMPLING_SAMPLE_RATE, out wavFile);
                    //remplace bouton play
                    this.IsMusicChosen = true;

                    Game1.CurrentSelectedWavFile = wavFile;
                }
                else if (isOver(mouse, Play)) {// Clique sur Play!
                    if (Game1.CurrentSelectedWavFile == "") return;
                    
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Stop();
                    Game1.GameState = GameState.MusicPlaying;
                    int selectedCharacter = Game1.Save.CharactersArray[Game1.Save.SelectedSave].selectedCharacter;
                    
                    this.IsMusicChosen = false;

                    //Charge le jeu
                    this.MusicPlaying.LoadDataCharacter(Game1.characters.getSelectedCharacter());
                    this.MusicPlaying.LoadGame();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Back.Draw(spriteBatch, gameTime);

            this.Casual[0].Draw(spriteBatch, gameTime);
            this.Veteran[0].Draw(spriteBatch, gameTime);
            this.GodLike[0].Draw(spriteBatch, gameTime);

            if (this.SelectedDifficulty == 0) {
                this.Casual[1].Draw(spriteBatch, gameTime);
            }
            else if (this.SelectedDifficulty == 1) {
                this.Veteran[1].Draw(spriteBatch, gameTime);
            }
            else if (this.SelectedDifficulty == 2) {
                this.GodLike[1].Draw(spriteBatch, gameTime);
            }

            this.ChooseMusic.Draw(spriteBatch, gameTime);

            if (IsMusicChosen) {
                this.Play.Draw(spriteBatch, gameTime);
            }
            else {
                this.WaitPlease.Draw(spriteBatch, gameTime);
                this.NotPlay.Draw(spriteBatch, gameTime);
            }

            PlayableCharacter tmp = Game1.characters.getSelectedCharacter();

            if (tmp.Name == "Barbarian")
            {
                tmp.setPosition(new Vector2(20f * Game1.UnitX, 4.5f * Game1.UnitY));
                tmp.setScale(2);
            }
            else
            {
                tmp.setPosition(new Vector2(23.5f * Game1.UnitX, 4 * Game1.UnitY));
                tmp.setScale(2);
            }
            tmp.Draw(spriteBatch); 
        }
    }
}
