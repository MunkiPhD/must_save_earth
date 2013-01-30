using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MustSaveEarth {
    class Player {
        public Texture2D Texture { get; private set; }

        private ContentManager _content;
        private Viewport _view;
        private Vector2 _velocity;
        private Vector2 _position;
        private Rectangle _rectangleHitBox;
        private float _maxSpeed = 5f;

        public Player(ContentManager content, Viewport view) {
            _content = content;
            _view = view;

            Texture = _content.Load<Texture2D>("PlayerTank1");

            _velocity = Vector2.Zero;
            _position = new Vector2(300, 200);
            _rectangleHitBox = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {
            // get user input
            HandleGamepadInput();
            HandleKeyboardInput();

            _velocity *= _maxSpeed;

            // add velocity to the current vector
            _position += _velocity;



            // if theyre not on the bottom, drop them until they reach
            if((_position.Y + Texture.Height) < (_view.TitleSafeArea.Bottom + 200))
                _velocity.Y = 1;
            else
                _velocity.Y = 0;



            // clamp the player to the bounds of the viewing area
            _position.X = MathHelper.Clamp(_position.X, _view.TitleSafeArea.Left, _view.TitleSafeArea.Right - Texture.Width);
            _position.Y = MathHelper.Clamp(_position.Y, _view.TitleSafeArea.Bottom - Texture.Height - 120, _view.TitleSafeArea.Top);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, Color.White);
        }



        /// <summary>
        /// Handles input from the gamepad
        /// </summary>
        public void HandleGamepadInput() {
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            if(gamepadState.IsConnected) {
                _velocity.X = gamepadState.ThumbSticks.Left.X;
            }
        }


        public void HandleKeyboardInput() {
            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.A))
                _velocity.X = -1;
            else if(keyboardState.IsKeyDown(Keys.D))
                _velocity.X = 1;
            else
                _velocity.X = 0;

            if(keyboardState.IsKeyDown(Keys.Space))
                if(ShotManager.CanFireShot())
                    ShotManager.FireShot(this.Position);
        }

        #region Properties

        /// <summary>
        /// The center of the texture
        /// </summary>
        public Vector2 RelativeCenter {
            get {
                return new Vector2(Texture.Width / 2, Texture.Height / 2);
            }
        }


        public Rectangle RectangleHitBox {
            get {
                return _rectangleHitBox;
            }
        }


        public Vector2 Position {
            get {
                return _position;
            }
        }
        #endregion

    }
}
