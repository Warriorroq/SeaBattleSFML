using System;
using System.Text;
using SFML.Graphics;
using SFML.System;
namespace Project
{
    class Image : GameObject
    {
        public Shape shape = null;
        public Image(Vector2f position, Vector2f size, Texture texture)
        {
            shape = new RectangleShape()
            {
                Position = position,
                Size = size,
                Texture = texture
            };
        }
        public override void Draw()
        {
            WindowParams.renderWindow.Draw(shape);
        }
    }
}
