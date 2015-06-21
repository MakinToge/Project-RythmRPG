using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmRPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class MusicPlaying : Page{
        private Texture2D background;

        public ContentManager Content { get; set; }
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public CharacterSprites[] SpriteCharacters { get; set; }
        public List<AbstractCharacter> Monsters { get; set; }
        public TextSprite HP { get; set; }
        public int HPStart { get; set; }
        public Difficulty Difficulty { get; set; }
        public TextSprite[] Skills { get; set; }
        public List<Sprite> Strings { get; set; }
        public List<Sprite>[] Lines { get; set; }
        public List<Sprite> Circles { get; set; }

        public override void Initialize() {

            //Character Data
            
            this.SpriteCharacters = new CharacterSprites[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new CharacterSprites(new Vector2(8 * Game1.UnitX, 8 * Game1.UnitY), 0, 0.7f, 0);
            }
            this.HP = new TextSprite(5 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.White);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.Content = content;

            //this.MainImage.LoadContent(content, "MusicPlaying/MusicPlaying");
            this.background = Content.Load<Texture2D>("BackgroundLevel");
            //Character Data
            
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++) {
                string name = Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].Name;
                this.SpriteCharacters[i].Load(content, "Spritesheet/Hero/Idle" + name, "Spritesheet/Hero/Attacking" + name, 2,4,10);
            }
            this.HP.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {

            if (currentKeyboardState.IsKeyDown(Keys.Escape)) {
                StartMenu.EffectClick.Play();
                Game1.GameState = GameState.Pause;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Z)) {//Touche Z
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Q)) {//Touche Q
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D)) {//Touche D
            }
            else if (currentKeyboardState.IsKeyDown(Keys.LeftShift)) {//Touche Shift
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Space)) {//Touche Space
            }
        }

        public override void Update(GameTime gametime) {
            for (int i = 0; i < this.SpriteCharacters.Length; i++)
            {
                this.SpriteCharacters[i].UpdateFrame((float)gametime.ElapsedGameTime.TotalSeconds);
            }

            int positionDisparaitre = 30 * Game1.UnitX;
            //Update les notes
            for (int i = 0; i < this.Lines.Length; i++) {
                for (int j = 0; j < this.Lines[i].Count; j++) {
                    //Supprimer la note si elle depasse la positionDisparaitre
                    Sprite note = this.Lines[i][j];
                    if (note.Position.X > positionDisparaitre){//Monstre doit attaquer ici
                        this.Lines[i].Remove(note);
                    }
                    note.Update(gametime);
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            spriteBatch.Begin();
            spriteBatch.Draw(this.background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 0.70f, SpriteEffects.None, 0);
            spriteBatch.End();

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].DrawFrame(spriteBatch);
            this.HP.Draw(spriteBatch, gameTime);

            //Draw la position des notes
            foreach (List<Sprite> list in Lines) {
                foreach (Sprite note in list) {
                    note.Draw(spriteBatch, gameTime);
                }
            }
            //Draw circles
            foreach (Sprite item in Circles) {
                item.Draw(spriteBatch, gameTime);
            }

            //Draw Strings
            foreach (Sprite item in this.Strings) {
                item.Draw(spriteBatch, gameTime);
            }
        }

        public void LoadDataCharacter(PlayableCharacter character) {
            this.HPStart = character.Health;

            this.HP.Text = character.Health.ToString() + " / " + this.HPStart.ToString();
        }

        public void LoadGame() {
            int nbLines;
            if (Game1.Difficulty == Difficulty.Casual) {
                nbLines = 3;
            }
            else if (Game1.Difficulty == Difficulty.Veteran) {
                nbLines = 4;
            }
            else {
                nbLines = 5;
            }
            
            this.Lines = new List<Sprite>[nbLines];
            for (int i = 0; i < nbLines; i++) {
                this.Lines[i] = new List<Sprite>();
			}
            //Circles
            this.Circles = new List<Sprite>();
            for (int i = 0; i < nbLines; i++) {
                Sprite circle = new Sprite(28 * Game1.UnitX, (12 + i) * Game1.UnitY, Game1.UnitX, Game1.UnitY);
                this.Circles.Add(circle);
                this.Circles[i].LoadContent(this.Content, "MusicPlaying/noteCircle");
            }
            //Strings
            this.Strings = new List<Sprite>();
            
            for (int i = 0; i < nbLines; i++) {
                Sprite oneString = new Sprite(2 * Game1.UnitX, (12.5f + i) * Game1.UnitY, 27 * Game1.UnitX, 1);
                this.Strings.Add(oneString);
                this.Strings[i].LoadContent(this.Content, "Options/One");
            }

            //Juste pour l'exemple
            for (int i = 0; i < nbLines; i++) {
                this.AddNote(i);
            }

            this.isLoaded = true;
        }

        public void AddNote(int line) {
            //Mettre la direction de la note; Gauche à droite:1; Droite à Gauche:-1
            float directionX = 1;
            float directionY = 0;
            //Position de départ de la note
            float positionX = 2 * Game1.UnitX;
            float positionY = (12 + line) * Game1.UnitY;
            //Vitesse
            float speed = 0.4f;
            Sprite note = new Sprite(positionX, positionY, Game1.UnitX, Game1.UnitY, directionX, directionY, speed);
            //Charge l'image
            string assetName = "MusicPlaying/note";
            if (line == 0) {
                assetName = "MusicPlaying/noteGreen";
            }
            else if (line == 1) {
                assetName = "MusicPlaying/noteRed";
            }
            else if (line == 2) {
                assetName = "MusicPlaying/noteYellow";
            }
            else if (line == 3) {
                assetName = "MusicPlaying/noteBlue";
            }
            else {
                assetName = "MusicPlaying/noteOrange";
            }
            note.LoadContent(this.Content, assetName);
            Lines[line].Add(note);
        }
    }
}
