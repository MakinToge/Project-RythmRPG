using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using RythmRPG.CharacterStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class MusicPlaying : Page{
        public ContentManager Content { get; set; }
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public Sprite MainImage { get; set; }
        public Sprite[] SpriteCharacters { get; set; }
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

        public List<Sprite>[] Lines { get; set; }
        public List<Sprite> Circles { get; set; }
        

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);

            //Character Data
            
            this.SpriteCharacters = new Sprite[Characters.NB_MAX_CHARACTERS];
            for (int i = 0; i < SpriteCharacters.Length; i++) {
                this.SpriteCharacters[i] = new Sprite(17 * Game1.UnitX, 2 * Game1.UnitY, 5 * Game1.UnitX, 5 * Game1.UnitY);
            }
            this.Name = new TextSprite(17 * Game1.UnitX, 1f * Game1.UnitY, "", Color.Black);
            this.Level = new TextSprite(26 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);
            this.Endurance = new TextSprite(26 * Game1.UnitX, 5.2f * Game1.UnitY, "", Color.Black);
            this.HP = new TextSprite(25 * Game1.UnitX, 3.2f * Game1.UnitY, "", Color.Black);
            this.Strength = new TextSprite(26 * Game1.UnitX, 4.2f * Game1.UnitY, "", Color.Black);

            this.Ability = new TextSprite(28 * Game1.UnitX, 2.2f * Game1.UnitY, "", Color.Black);
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.Content = content;

            this.MainImage.LoadContent(content, "MusicPlaying/MusicPlaying");

            //Character Data
            
            for (int i = 0; i < Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray.Length; i++) {
                this.SpriteCharacters[i].LoadContent(content, "Characters/" + Game1.Save.CharactersArray[Game1.Save.SelectedSave].CharacterArray[i].IdleSpriteName);
            }

            this.Name.LoadContent(content, "Arial16");
            this.Level.LoadContent(content, "Arial16");
            this.Endurance.LoadContent(content, "Arial16");
            this.HP.LoadContent(content, "Arial16");
            this.Strength.LoadContent(content, "Arial16");
            this.Ability.LoadContent(content, "Arial16");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            /*
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X, currentMouseState.Y, 10, 10);
            }
            */
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
            int positionDisparaitre = 30 * Game1.UnitX;
            //Update les notes
            for (int i = 0; i < this.Lines.Length; i++) {
                for (int j = 0; j < this.Lines[i].Count; j++) {
                    //Supprimer la note si elle depasse la positionDisparaitre
                    Sprite note = this.Lines[i][j];
                    if (note.Position.X > positionDisparaitre){
                        this.Lines[i].Remove(note);
                    }
                    note.Update(gametime);
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Ability.Draw(spriteBatch, gameTime);

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
        }

        public void LoadDataCharacter(PlayableCharacter character) {
            this.HPStart = character.Health;
            this.Name.Text = character.Name;
            if (character.NbRestart == 0) {
                this.Level.Text = character.Level.ToString();
            }
            else {
                this.Level.Text = string.Format("{0} ({1})", character.Level, character.NbRestart);
            }
            
            this.Endurance.Text = character.Defense.ToString();
            this.HP.Text = character.Health.ToString() + " / " + this.HPStart.ToString();
            this.Strength.Text = character.Attack.ToString();
            this.Ability.Text = character.uniqueSkill.ToString();
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
                Sprite circle = new Sprite(28 * Game1.UnitX, (11 + i) * Game1.UnitY, Game1.UnitX, Game1.UnitY);
                this.Circles.Add(circle);
                this.Circles[i].LoadContent(this.Content, "MusicPlaying/noteCircle");
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
            float positionY = (11 + line) * Game1.UnitY;
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
