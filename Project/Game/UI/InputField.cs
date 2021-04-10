using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Project
{
    public class InputField : GameObject
    {
        public string Text 
            => text.DisplayedString;
        private Text text = null;
        private int maxLenght = 0;
        private Shape shape = null;
        private bool InputInformation = false;
        public event Action<InputField> EndWrite;
        public InputField(Vector2f position, Vector2f size, int maxLenght, string startText)
        {
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = Color.White,
                OutlineThickness = 1f,
                OutlineColor = Color.Black
            };

            this.text = new Text(startText, new Font(Fonts.CARTOONIST))
            {
                Position = shape.Position,
                Color = Color.Black,
                CharacterSize = 20
            };

            this.maxLenght = maxLenght;
            WindowParams.renderWindow.MouseButtonPressed += GlobalClick;
            WindowParams.renderWindow.KeyPressed += OnKeyPressed;
        }
        public override void Draw()
        {
            WindowParams.renderWindow.Draw(shape);
            WindowParams.renderWindow.Draw(text);
        }
        public void SetText(string text)
            =>this.text.DisplayedString = text;

        [Obsolete]
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (InputInformation)
                InputKey(e.Code);
        }

        [Obsolete]
        private void InputKey(Keyboard.Key key)
        {
            if (key == Keyboard.Key.BackSpace && text.DisplayedString.Length > 0)
            {
                text.DisplayedString = text.DisplayedString.Substring(0, text.DisplayedString.Length - 1);
                return;
            }
            if (key == Keyboard.Key.Enter)
            {
                InputInformation = false;
                EndWrite?.Invoke(this);
                return;
            }
            if(key != Keyboard.Key.BackSpace && text.DisplayedString.Length <= maxLenght)
                text.DisplayedString += key.ToString();
        }
        private void GlobalClick(object sender, MouseButtonEventArgs e)
        {
            InputInformation = false;
            Vector2f mappedpos = WindowParams.renderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y));
            if (shape.GetGlobalBounds().Contains(mappedpos.X, mappedpos.Y) && IsActive)
            {
                text.DisplayedString = "";
                InputInformation = true;
            }
        }
        public override void Destroy()
        {
            WindowParams.renderWindow.MouseButtonPressed -= GlobalClick;
            WindowParams.renderWindow.KeyPressed -= OnKeyPressed;
            EndWrite = null;
        }
    }
}
