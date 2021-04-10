using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Project
{
    public class Button : GameObject
    {
        public Text text = null;
        public bool Pressed = false;
        private Shape shape = null;
        public event Action Clicked;
        public Button(Vector2f position, Vector2f size, string text)
        {
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = Color.White,
                OutlineThickness = 1f,
                OutlineColor = Color.Black
            };

            this.text = new Text(text, new Font(Fonts.CARTOONIST)) {
                Position = shape.Position,
                Color = Color.Black
            };

            WindowParams.renderWindow.MouseButtonPressed += GlobalClick;
            WindowParams.renderWindow.MouseButtonReleased += GlobalRelease;
        }
        public void SetShape(Shape shape)
            =>this.shape = shape;
        private void GlobalClick(object sender, MouseButtonEventArgs e)
        {
            Vector2f mappedpos = WindowParams.renderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y));
            if (shape.GetGlobalBounds().Contains(mappedpos.X, mappedpos.Y) && IsActive)
            {
                Clicked?.Invoke();
                Pressed = true;
            }
        }
        private void GlobalRelease(object sender, MouseButtonEventArgs e)
        {
            Pressed = false;
        }
        public override void Draw()
        {
            WindowParams.renderWindow.Draw(shape);
            WindowParams.renderWindow.Draw(text);
        }
        public override void Destroy()
        {
            WindowParams.renderWindow.MouseButtonPressed -= GlobalClick;
            WindowParams.renderWindow.MouseButtonReleased -= GlobalRelease;
            Clicked = null;
        }
    }
}
