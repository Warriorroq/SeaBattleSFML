using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public static class Scenes
    {
        public static Scene CreateStartScene() {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg1));

            var button = new Button(new Vector2f(500, 500), new Vector2f(160, 60), "Create room");            
            button.OnClicked += Program.game.StartServer;
            button.OnClicked += scene.Destroy<Button>;
            scene.Add(button);

            button = new Button(new Vector2f(500, 600), new Vector2f(160, 60), "Connect room");
            button.OnClicked += Program.game.Connect;
            button.OnClicked += scene.Destroy<Button>;
            scene.Add(button);
            var input = new InputField(new Vector2f(500, 400), new Vector2f(160, 30), 14, $"NickName {Game.random.Next(0, 100)}");
            input.OnEndWrite += Program.lobby.mainSocket.SetNickName;
            scene.Add(input);

            return scene;
        }
        public static Scene CreateConnectionLobby()
        {
            var scene = CreateMapCastScene();
            var button = new Button(new Vector2f(WindowParams.widthWindow - 260, WindowParams.heightWindow - 80), new Vector2f(180, 40), "Connect server");
            button.OnClicked += Program.lobby.SetUpConnection;
            button.OnClicked += scene.Find<Map>().Move;
            button.OnClicked += scene.Find<Chat>().UpdateActive;
            button.OnClicked += scene.Destroy<Button>;
            button.OnClicked += (() => scene.Add(new InputField(new Vector2f(WindowParams.widthWindow - 470, 500), new Vector2f(400, 30), 35, $"message: ")));
            button.OnClicked += (() => scene.Find<InputField>().OnEndWrite += Program.game.UseChat);
            scene.Add(button);
            return scene;
        }
        public static void CreateChat()
        {

        }
        public static Scene CreateServerLobby()
        {
            var scene = CreateMapCastScene();
            var button = new Button(new Vector2f(WindowParams.widthWindow - 240, WindowParams.heightWindow - 80), new Vector2f(160, 40), "Create server");
            button.OnClicked += Program.lobby.SetUpServer;
            button.OnClicked += scene.Find<Chat>().UpdateActive;
            button.OnClicked += scene.Find<Map>().Move;
            button.OnClicked += scene.Destroy<Button>;
            button.OnClicked += (() => scene.Add(new InputField(new Vector2f(WindowParams.widthWindow - 470, 500), new Vector2f(400, 30), 35, $"message: ")));
            button.OnClicked += (() => scene.Find<InputField>().OnEndWrite += Program.game.UseChat);
            scene.Add(button);
            return scene;
        }
        private static Scene CreateMapCastScene()
        {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg3));

            var map = new Map(30);

            var button = new Button(new Vector2f(500, 500), new Vector2f(150, 60), "Create auto");
            button.OnClicked += map.CreateCells;
            button.OnClicked += scene.Destroy<InputField>;
            scene.Add(button);

            button = new Button(new Vector2f(80, WindowParams.heightWindow - 80), new Vector2f(120, 40), "main menu");
            button.OnClicked += scene.Destroy<Button>;
            button.OnClicked += Program.game.MainMenu;
            scene.Add(new Chat(new Vector2f(WindowParams.widthWindow - 470, 70)) { IsActive = false});
            scene.Add(button);
            scene.Add(map);
            return scene;
        }
    }
}
