using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Project
{
    class Game : GameLoop
    {
        public const string titleOfTheWindow = "Game";
        public static Random random = new Random();
        public Scene scene = null;
        public Game() : base(titleOfTheWindow)
        {

        }
        public override void LoadContent()
        {
            DebugUtility.LoadContent(Fonts.CARTOONIST);
            GameTextures.LoadContent();
        }
        public override void Init()
        {
            scene = SceneFabric.CreateStartScene();
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            scene?.Draw();
        }
        public void StartServer()
        {
            
            scene.Dispose();
            scene = SceneFabric.CreateMapScene();
        }
        public void Connect()
        {
            scene.Dispose();
            scene = SceneFabric.CreateMapScene();
        }
        private void ShowFPS()
            => Debug($"FPS: {(1 / Time.deltaTime):0.0}");
    }
}