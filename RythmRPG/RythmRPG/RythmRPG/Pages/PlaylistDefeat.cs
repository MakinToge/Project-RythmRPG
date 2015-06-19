using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class PlaylistDefeat : AfterGame{
        public Sprite Skip { get; set; }
        public Sprite Retry { get; set; }

        public PlaylistDefeat(string assetNameMainImage, string message)
            : base(assetNameMainImage, message) {
        }
        public override void Initialize() {
            base.Initialize();
            this.Skip = new Sprite(22 * Game1.UnitX, 12 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            this.Retry = new Sprite(22 * Game1.UnitX, 6 * Game1.UnitY, 7 * Game1.UnitX, 2 * Game1.UnitY);
            
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            base.LoadContent(content);
            this.Skip.LoadContent(content, "AfterGame/SkipMusic");
            this.Retry.LoadContent(content, "AfterGame/RetryMusic");
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            base.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
        }
        /*
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.Skip.Draw(spriteBatch, gameTime);
            this.Retry.Draw(spriteBatch, gameTime);
        }*/
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.GameMenu.Draw(spriteBatch, gameTime);
            this.Xp.Draw(spriteBatch, gameTime);
            this.Skip.Draw(spriteBatch, gameTime);
            this.Retry.Draw(spriteBatch, gameTime);

            //Character Data
            this.SpriteCharacters[Game1.Save.CharactersArray[Game1.Save.SelectedSave].SelectCharacter].Draw(spriteBatch, gameTime);
            this.Type.Draw(spriteBatch, gameTime);
            this.Name.Draw(spriteBatch, gameTime);
            this.Level.Draw(spriteBatch, gameTime);
            this.Endurance.Draw(spriteBatch, gameTime);
            this.HP.Draw(spriteBatch, gameTime);
            this.Strength.Draw(spriteBatch, gameTime);
            this.Vitality.Draw(spriteBatch, gameTime);
        }
    }
}
