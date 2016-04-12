using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Importador
{
    public class Game1 : Game
    {
        public static float scale = 0.75f;
        public static float unitSize = 128f;
        public static List<Sprite> scene;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

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

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: Matrix.CreateScale(scale));
            foreach (Sprite sprite in scene)
            {
                sprite.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
