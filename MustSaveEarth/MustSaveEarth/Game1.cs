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

namespace MustSaveEarth {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player _playerOne;
        Texture2D _background; // refactor to own class
        Texture2D _foreground;
        MapData _mapData;
        EnemyManager _enemyManager;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("Background_1");
            _foreground= Content.Load<Texture2D>("CanyonFore");
            _playerOne = new Player(Content, GraphicsDevice.Viewport);
            _mapData = new MapData(Content, GraphicsDevice.Viewport);
            _enemyManager = new EnemyManager(Content, GraphicsDevice.Viewport);
            PlayerMovement.Initialize(_playerOne, _foreground);
            ShotManager.Initialize(Content, GraphicsDevice.Viewport);
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
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ShotManager.Update(gameTime);
            _playerOne.Update(gameTime);
            _enemyManager.Update(gameTime);
            _mapData.Update(gameTime, GraphicsDevice.Viewport);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Rectangle(0, 0, _background.Width, _background.Height), Color.White);
            spriteBatch.Draw(_foreground, new Rectangle(0, 0, _foreground.Width, _foreground.Height), Color.White);
            ShotManager.Draw(spriteBatch);
            _enemyManager.Draw(spriteBatch);
            _mapData.Draw(spriteBatch);
            _playerOne.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
