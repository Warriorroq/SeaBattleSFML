using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace Project
{
    public static class DebugUtility
    {

        public static Font consoleFont;
        public static Text LOG;

        public static void LoadContent(string font)
        {
            consoleFont = new Font(font);
            LOG = new Text("", consoleFont, 16);
            LOG.OutlineColor = Color.Black;
            LOG.Color = Color.Black;
            LOG.Position = new Vector2f(0, 0);
        }
        public static void Message(GameLoop gameLoop, object message)
        {

            if (consoleFont == null)
                return;

            if(message is null)
                LOG.DisplayedString = "NULL";
            else
                LOG.DisplayedString = message.ToString();

            WindowParams.renderWindow.Draw(LOG);
        }
    }
}