using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MustSaveEarth {
    static class Player {
        static public Texture2D Texture { get; private set; }

        static private ContentManager _content;
        static private Viewport _view;
        static private Vector2 _velocity;
        static private Vector2 _position;
        static private Rectangle _initialFrame;
        static private float _maxSpeed = 5f;
        static private int _score = 0;
        static private SpriteFont _spriteFont;

        static public bool Floating = true;

        public static void Initialize(ContentManager content, Viewport view) {
            _content = content;
            _view = view;

            Texture = _content.Load<Texture2D>("PlayerTank1");
            _spriteFont = _content.Load<SpriteFont>("SpriteFont1");

            _velocity = Vector2.Zero;
            _position = new Vector2(300, 200);
            _initialFrame = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime) {
            // get user input
            HandleGamepadInput();
            HandleKeyboardInput();

            _velocity.X *= _maxSpeed;

            // add velocity to the current vector
            _position += _velocity;



            // if theyre not on the bottom, drop them until they reach
            if ((_position.Y + Texture.Height) < (_view.TitleSafeArea.Bottom + 200))
                _velocity.Y = 4;
            else
                _velocity.Y = 0;


            PlayerMovement.MovePlayerToMapYLocation();


            // clamp the player to the bounds of the viewing area
            _position.X = MathHelper.Clamp(_position.X, _view.TitleSafeArea.Left, _view.TitleSafeArea.Right - Texture.Width);
            _position.Y = MathHelper.Clamp(_position.Y, _view.TitleSafeArea.Top, _view.TitleSafeArea.Bottom - Texture.Height);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.DrawString(_spriteFont, string.Format("Score: {0}", Score), new Vector2(10, 10), Color.White);
        }



        /// <summary>
        /// Handles input from the gamepad
        /// </summary>
        public static void HandleGamepadInput() {
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            if (gamepadState.IsConnected) {
                _velocity.X = gamepadState.ThumbSticks.Left.X;
            }
        }



        /// <summary>
        /// Handles input for the keyboard
        /// </summary>
        public static void HandleKeyboardInput() {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
                _velocity.X = -1;
            else if (keyboardState.IsKeyDown(Keys.D))
                _velocity.X = 1;
            else
                _velocity.X = 0;

            if (keyboardState.IsKeyDown(Keys.Space))
                if (ShotManager.CanFireShot())
                    ShotManager.FireShot(Position);
        }



        /// <summary>
        /// Adds score to the player
        /// </summary>
        /// <param name="score">The score as an integer to add (can be negative)</param>
        public static void AddScore(int score) {
            // doing this as a method in case we have modifiers like 2x points or somehting
            _score += score;
        }
        #region Properties

        /// <summary>
        /// The center of the texture
        /// </summary>
        public static Vector2 RelativeCenter {
            get {
                return new Vector2(Texture.Width / 2, Texture.Height / 2);
            }
        }


        public static Rectangle RectangleHitBox {
            get {
                return _initialFrame;
            }
        }


        public static Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
            }
        }


        public static Vector2 WorldPosition {
            get {
                return _position + RelativeCenter;
            }
        }


        /// <summary>
        /// The score for the player
        /// </summary>
        public static int Score {
            get {
                return _score;
            }
        }
        #endregion

    }
}
