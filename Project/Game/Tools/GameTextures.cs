using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public static class GameTextures
    {
        public static Texture bg1 = null;
        public static Texture bg2 = null;
        public static Texture bg3 = null;
        public static Texture ship = null;
        private static string bg1Source = "./Textures/bg1.jpg";
        private static string bg2Source = "./Textures/bg2.jpg";
        private static string bg3Source = "./Textures/bg3.jpg";
        private static string shipSource = "./Textures/ship.jpg";

        public static void LoadContent()
        {
            bg1 = new Texture(bg1Source);
            bg2 = new Texture(bg2Source);
            bg3 = new Texture(bg3Source);
            ship = new Texture(shipSource);
        }
    }
}
