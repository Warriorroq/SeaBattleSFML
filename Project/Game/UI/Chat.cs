using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
namespace Project
{
    public class Chat : GameObject
    {
        public Shape shape;
        private Text text;
        public List<string> messages = new List<string>();
        public Chat(Vector2f position)
        {
            drawLayer = 2;
            shape = new RectangleShape() {
                Size = new Vector2f(400, 420),
                Position = position,
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };

            text = new Text("", new Font(Fonts.CARTOONIST)) { 
                Position = position,
                Color = Color.Black,
                CharacterSize = 20
            };
        }
        public void UpdateActive()
            => IsActive = !IsActive;
        public void AddMessage(string message)
        {
            messages.Add($"\n{message}");
            if (messages.Count * text.CharacterSize > 420 - 5 * text.CharacterSize)
                messages.Remove(messages[0]);
            UpdateText();
        }
        private void UpdateText()
        {
            text.DisplayedString = "";
            foreach (var message in messages)
                text.DisplayedString += message;
        }
        public override void Draw()
        {
            WindowParams.renderWindow.Draw(shape);
            WindowParams.renderWindow.Draw(text);
        }
    }
}
