using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Importador
{
    class Player : Sprite
    {
        float accelerationY = .5f;
        float velocityY = 0f;
            
        public Player(ContentManager content,
            string imagename, Vector2 position) :
            base(content, imagename, position)
        {

        }

        public void Update(GameTime gameTime)
        {
            // Gravidade
            velocityY += accelerationY *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 target = position;
            target.Y -= velocityY;
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
                position.Y =
                    (collision.Value.Top + bbox.Height) / Game1.unitSize;
                velocityY = 0f;
            }



        }
    }
}
