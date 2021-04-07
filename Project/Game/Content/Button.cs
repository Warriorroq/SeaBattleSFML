using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Project
{
    public class Button : IDrawable
    {
        public Text text = null;
        public bool Clicked = false;
        private Shape shape = null;
        public event OnClick OnClicked;
        public delegate void OnClick();
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
            if (shape.GetGlobalBounds().Contains(mappedpos.X, mappedpos.Y))
            {
                OnClicked.Invoke();
                Clicked = true;
            }
        }
        private void GlobalRelease(object sender, MouseButtonEventArgs e)
        {
            Clicked = false;
        }
        public virtual void Draw()
        {
            WindowParams.renderWindow.Draw(shape);
            WindowParams.renderWindow.Draw(text);
        }
        
    }
}
