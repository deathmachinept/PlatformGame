using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Importador
{
    public class Sprite
    {
        Texture2D image;
        protected Vector2 position;
        protected Rectangle bbox;

        public Sprite(ContentManager content, 
             string imageName, Vector2 position)
        {
            image = content.Load<Texture2D>(imageName);
            this.position = position;
            bbox = new Rectangle(
                (int)(position.X * Game1.unitSize),
                (int)(position.Y * Game1.unitSize),
                image.Width, image.Height);
            /* bbox = new Rectangle(
                (position * Game1.unitSize).ToPoint(),
                new Point(image.Width, image.Height)
                ); */
        }

        protected void updateBoundingBox(Vector2 pos)
        {
            // estamos a pressupor que o tamanho da imagem
            // nunca muda, portanto, é só atualizar as
            // coordenadas da bounding box.
            bbox.Location = 
                (position * Game1.unitSize).ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float height = spriteBatch.GraphicsDevice.Viewport.Height;
            float y = height / Game1.scale - image.Height - position.Y * Game1.unitSize;
            spriteBatch.Draw(image, new Vector2(position.X * Game1.unitSize, y), Color.White);
        }

        // Verifica se rect colide com algum objeto da cena
        // Se colidir, retorna a bounding box desse objeto
        // Se nao colidir retorna null
        protected Rectangle? collides(Rectangle rect)
        {
            foreach (Sprite s in Game1.scene)
            {
                if (rect.Intersects(s.bbox))
                    return s.bbox;
            }
            return null;
        }
    }
}
