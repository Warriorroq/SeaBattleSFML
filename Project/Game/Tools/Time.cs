using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public static class Time
    {
        private static GameTime time = null;

        public static float deltaTime
        {
            get => time.DeltaTime;
        }
        public static GameTime GetTimer() 
            => time;
        public static void SetTimer(GameTime gameTime)
        {
            time = gameTime;
        }
    }
}