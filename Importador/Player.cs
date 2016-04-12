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
    }
}
