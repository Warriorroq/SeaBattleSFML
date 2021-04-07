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
        public Button btn = null;
        public InputField name = null;
        public InputField name2 = null;
        public Game() : base(titleOfTheWindow)
        {

        }
        public override void LoadContent()
        {
            DebugUtility.LoadContent(Fonts.CARTOONIST);
        }
        public override void Init()
        {
            btn = new Button(new Vector2f(100, 100), new Vector2f(100,60),"button 1");
            name = new InputField(new Vector2f(100, 300), new Vector2f(100, 40));
            name2 = new InputField(new Vector2f(300, 300), new Vector2f(100, 40));
            btn.OnClicked += Console.Clear;
            btn.OnClicked += Print;
            btn.OnClicked += Console.WriteLine;
            btn.OnClicked += Print;
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            btn.Draw();
            name.Draw();
            name2.Draw();
        }
        public void Print()
        {
            Console.WriteLine("Poshel hanui");
            name2.SetText("Poshel hanui");
        }
        private void ShowFPS()
            => Debug($"FPS: {(1 / Time.deltaTime):0.0}");
    }
}