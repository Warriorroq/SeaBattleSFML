using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            MainMenu();
        }
        public override void Update()
        {
            Program.lobby.UpdateConnection();
        }
        public void UseChat(InputField field)
        {
            Program.lobby.Send(CommandConverter.StringToBytes($"{Program.lobby.mainPlayer.nikName}: {field.Text}"));
        }
        public override void Draw()
        {
            scene?.Draw();
            ShowFPS();
        }
        public void MainMenu()
        {
            scene?.Destroy();
            scene = Scenes.CreateStartScene();
        }
        public void SendToChat(string message)
        {
            scene.Find<Chat>().AddMessage(message);
        }
        public void StartServer()
        {
            scene?.Destroy();
            scene = Scenes.CreateServerLobby();
        }
        public void Connect()
        {
            scene?.Destroy();
            scene = Scenes.CreateConnectionLobby();
            Program.lobby.mainPlayer.shoot = false;
        }
        private void ShowFPS()
            => Debug($"FPS: {(1 / Time.deltaTime):0.0}");
    }
}