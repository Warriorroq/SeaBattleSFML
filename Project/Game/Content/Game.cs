using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Project
{
    class Game : GameLoop
    {
        public const string titleOfTheWindow = "Game";
        private Random random = new Random();
        public Game() : base(WindowParams.widthWindow, WindowParams.heightWindow, titleOfTheWindow)
        {

        }
        public override void LoadContent()
        {
            DebugUtility.LoadContent(Fonts.CARTOONIST);
        }
        public override void Init()
        {

        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            ShowFPS();
        }
        private void ShowFPS()
            => Debug($"FPS: {(1 / Time.deltaTime):0.0}");
    }
}