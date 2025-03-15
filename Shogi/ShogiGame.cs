using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Shogi.Pieces;
using Shogi.Utils;
using Shogi.Rendering;
using System;

namespace Shogi
{
    public class ShogiGame : Game
    {
        private static string _language = JAP;
        public static string GameLanguage
        {
            get { return _language; }
            set { _language = (value == "en" || value == "jp") ? value : "jp"; }
        }

        public const string ENG = "en";
        public const string JAP = "jp";

        // Text font, the japanese font is used for the piece names if set to japanese
        private SpriteFont font;
        private SpriteFont jp_font;

        // Board and piece renderer
        private Board board;
        private Vector2 boardPosition = new(50, 50);
        private BoardRenderer boardRenderer;
        private PieceRenderer pieceRenderer;

        private Vector2 selectedPosition = new(-1, -1);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public ShogiGame()
        {
            // MonoGame initialization
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set the window size
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            // Initialize the board
            board = new Board();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Font");
            jp_font = Content.Load<SpriteFont>("JP_Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize the BoardRenderer
            boardRenderer = new BoardRenderer(Content, GraphicsDevice, board, GameLanguage == ENG ? font : jp_font);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            boardRenderer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            boardRenderer.Draw();

            base.Draw(gameTime);
        }

    
    }
}