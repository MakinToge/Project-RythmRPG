using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RythmRPG.CharacterStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class PlaylistChallenge : Page{
        public Sprite MainImage { get; set; }
        public Sprite Back { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
        public Sprite[] Casual { get; set; }
        public Sprite[] Veteran { get; set; }
        public Sprite[] GodLike { get; set; }
        public int SelectedDifficulty { get; set; }
        public Sprite Play { get; set; }
        public Sprite ChooseMusic { get; set; }

        public Sprite[] Classic { get; set; }
        public Sprite[] Hardcore { get; set; }
        public int SelectedMode { get; set; }
        public MusicPlaying MusicPlaying { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Back = new Sprite(26 * Game1.UnitX, 16 * Game1.UnitY, 6 * Game1.UnitX, 2 * Game1.UnitY);

            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new Sprite(21 * Game1.UnitX, 5 * Game1.UnitY, 8 * Game1.UnitX, 8 * Game1.UnitY);
            }

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

            this.Play = new Sprite(11 * Game1.UnitX, 14 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.ChooseMusic = new Sprite(11 * Game1.UnitX, 4 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);

            this.Classic = new Sprite[2] {
                new Sprite(Game1.UnitX, 12 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 12 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
            this.Hardcore = new Sprite[2] {
                new Sprite(Game1.UnitX, 13 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY),
                new Sprite(Game1.UnitX, 13 * Game1.UnitY, 8 * Game1.UnitX, Game1.UnitY)
            };
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "PlaylistChallenge/PlaylistChallenge");
            this.Back.LoadContent(content, "Options/Back");

            this.Casual[0].LoadContent(content, "SingleMusic/Casual");
            this.Casual[1].LoadContent(content, "SingleMusic/Selected/Casual");
            this.Veteran[0].LoadContent(content, "SingleMusic/Veteran");
            this.Veteran[1].LoadContent(content, "SingleMusic/Selected/Veteran");
            this.GodLike[0].LoadContent(content, "SingleMusic/GodLike");
            this.GodLike[1].LoadContent(content, "SingleMusic/Selected/GodLike");

            this.Play.LoadContent(content, "SingleMusic/Play!");
            this.ChooseMusic.LoadContent(content, "SingleMusic/ChooseMusic");

            this.Classic[0].LoadContent(content, "PlaylistChallenge/Classic");
            this.Classic[1].LoadContent(content, "PlaylistChallenge/Selected/Classic");
            this.Hardcore[0].LoadContent(content, "PlaylistChallenge/Hardcore");
            this.Hardcore[1].LoadContent(content, "PlaylistChallenge/Selected/Hardcore");

            //Character Data
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].IdleSpriteName);
            }
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);

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
                else if (isOver(mouse, Classic[0])) {
                    StartMenu.EffectClick.Play();
                    this.SelectedMode = 0;
                }
                else if (isOver(mouse, Hardcore[0])) {
                    StartMenu.EffectClick.Play();
                    this.SelectedMode = 1;
                }
                else if (isOver(mouse, ChooseMusic)) {//Clique sur Choose Music
                    StartMenu.EffectClick.Play();

                }
                else if (isOver(mouse, Play)) {// Clique sur Play!
                    StartMenu.EffectClick.Play();
                    StartMenu.MainTheme.Stop();
                    Game1.GameState = GameState.MusicPlaying;
                    int selectedCharacter = Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter;
                    PlayableCharacter character = Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[selectedCharacter];

                    //Charge le jeu
                    this.MusicPlaying.LoadDataCharacter(character);
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

            this.Classic[0].Draw(spriteBatch, gameTime);
            this.Hardcore[0].Draw(spriteBatch, gameTime);

            if (this.SelectedMode == 0) {
                this.Classic[1].Draw(spriteBatch, gameTime);
            }
            else if (SelectedMode == 1) {
                this.Hardcore[1].Draw(spriteBatch, gameTime);
            }

            this.ChooseMusic.Draw(spriteBatch, gameTime);
            this.Play.Draw(spriteBatch, gameTime);

            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].Draw(spriteBatch, gameTime);
        }
    }
}
