using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpCommand;

namespace TestEnviroment
{
    public class Game : Microsoft.Xna.Framework.Game, IInputListener
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputCore input;

        public Game()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.input = new InputCore("bindings.txt", new XNAInputModule());
            this.input.AddListener(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            this.input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        public void PerformAction(string action)
        {
            if (action == "Test")
            {
                Console.WriteLine("The Test action is being performed");
            }
        }
    }
}
