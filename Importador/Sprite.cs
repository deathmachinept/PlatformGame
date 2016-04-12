using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Importador
{
    class Sprite
    {
        Texture2D image;
        Vector2 position;

        public Sprite(ContentManager content, 
             string imageName, Vector2 position)
        {
            image = content.Load<Texture2D>(imageName);
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float height = spriteBatch.GraphicsDevice.Viewport.Height;
            float y = height / Game1.scale - image.Height - position.Y * 128;
            spriteBatch.Draw(image, new Vector2(position.X * 128, y), Color.White);
        }
    }
}
