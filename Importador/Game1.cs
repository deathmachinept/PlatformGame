using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Importador
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static float scale = 0.75f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List<Sprite> scene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            loadScene("MainScene.json");

            base.Initialize();
        }

        void loadScene(string filename)
        {
            filename = @"Content\" + filename;
            string sceneContents = File.ReadAllText(filename);
            JObject json = JObject.Parse(sceneContents);
            JArray images = (JArray) json["composite"]["sImages"];

            scene = new List<Sprite>();
            foreach (JObject image in images)
            {
                string imageName = (string)image["imageName"];

                float x, y;
                JToken t;
                if (image.TryGetValue("x", out t))
                    x = (float)t;
                else
                    x = 0f;
                if (image.TryGetValue("y", out t))
                    y = (float)t;
                else
                    y = 0f;

                if (imageName == "player")
                {
                    player = new Player(Content, imageName,
                        new Vector2(x, y));
                }
                else {
                    scene.Add(new Sprite(Content, imageName,
                        new Vector2(x, y)));
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: Matrix.CreateScale(scale));
            foreach (Sprite sprite in scene)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
