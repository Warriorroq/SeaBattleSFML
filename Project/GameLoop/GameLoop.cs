using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
namespace Project
{
    public abstract class GameLoop
    {
        public const int FPS = 144;
        public const float updateTime = 1f / FPS;

        protected GameLoop(uint widthOfTheWindow, uint heightOfTheWindow, string nameOfTheWindow)
        {
            var window = new RenderWindow(new VideoMode(widthOfTheWindow, heightOfTheWindow), nameOfTheWindow);
            window.Closed += WindowClosed;

            Time.SetTimer(new GameTime());
            WindowParams.renderWindow = window;
        }
        public void Run()
        {

            LoadContent();
            Init();

            float totalTimeBeforeUpdate = 0f;
            float previosTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock();
            while (WindowParams.renderWindow.IsOpen)
            {
                WindowParams.renderWindow.DispatchEvents();
                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previosTimeElapsed;
                previosTimeElapsed = totalTimeElapsed;
                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= updateTime)
                {
                    Time.GetTimer().Update(totalTimeBeforeUpdate, totalTimeElapsed);
                    totalTimeBeforeUpdate = 0f;

                    Update();

                    WindowParams.renderWindow.Clear(Color.White);
                    Draw();
                    WindowParams.renderWindow.Display();
                }
            }
        }
        private void WindowClosed(object sender, EventArgs e)
            => WindowParams.renderWindow.Close();

        public abstract void LoadContent();
        public abstract void Init();
        public abstract void Update();
        public abstract void Draw();
        public void Debug(object message)
            => DebugUtility.Message(this, message);
    }
}