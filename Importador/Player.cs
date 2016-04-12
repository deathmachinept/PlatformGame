using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Importador
{
    class Player : Sprite
    {
        public Player(ContentManager content,
            string imagename, Vector2 position) :
            base(content, imagename, position)
        {

        }

        public void Update(GameTime gameTime)
        {
            Vector2 target = position;
            target.Y -= 23f *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            updateBoundingBox(target);

            Rectangle? collision = collides(bbox);
            if (collision == null)
            {
                // nenhuma colisao
                position = target;
            }
            else
            {
                // colidimos!!!
                
            }



        }
    }
}
