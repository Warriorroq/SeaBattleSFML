using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Project
{
    public static class Scenes
    {
        public static Scene CreateStartScene() {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg1));

            var button = new Button(new Vector2f(500, 500), new Vector2f(160, 60), "Create room");            
            button.Clicked += Program.game.StartServer;
            button.Clicked += scene.Destroy<Button>;
            scene.Add(button);

            button = new Button(new Vector2f(500, 600), new Vector2f(160, 60), "Connect room");
            button.Clicked += Program.game.Connect;
            button.Clicked += scene.Destroy<Button>;
            scene.Add(button);
            var input = new InputField(new Vector2f(500, 400), new Vector2f(160, 30), 14, $"NickName {Game.random.Next(0, 100)}");
            input.EndWrite += Program.lobby.mainPlayer.SetNickName;
            scene.Add(input);

            return scene;
        }
        public static Scene CreateConnectionLobby()
        {
            var scene = CreateMapCastScene();
            var button = new Button(new Vector2f(WindowParams.widthWindow - 260, WindowParams.heightWindow - 80), new Vector2f(180, 40), "Connect server");
            button.Clicked += Program.lobby.SetUpConnection;
            button.Clicked += scene.Find<Map>().Move;
            button.Clicked += scene.Find<Chat>().UpdateActive;
            button.Clicked += scene.Destroy<Button>;
            button.Clicked += (() => scene.Add(new InputField(new Vector2f(WindowParams.widthWindow - 470, 500), new Vector2f(400, 30), 35, $"message: ")));
            button.Clicked += (() => scene.Find<InputField>().EndWrite += Program.game.UseChat);
            button.Clicked += AddElementsToScene;
            scene.Add(button);
            return scene;
        }
        public static void AddElementsToScene()
        {
            var button = new Button(new Vector2f(240, WindowParams.heightWindow - 80), new Vector2f(160, 40), "show map");
            Program.game.scene.Add(button);
            button.Clicked += (() => Program.game.scene.FindAll<Map>().ForEach(x => x.UpdateActive()));
        }
        public static Scene CreateServerLobby()
        {
            var scene = CreateMapCastScene();
            var button = new Button(new Vector2f(WindowParams.widthWindow - 240, WindowParams.heightWindow - 80), new Vector2f(160, 40), "Create server");
            button.Clicked += Program.lobby.SetUpServer;
            button.Clicked += scene.Find<Chat>().UpdateActive;
            button.Clicked += scene.Find<Map>().Move;
            button.Clicked += scene.Destroy<Button>;
            button.Clicked += (() => scene.Add(new InputField(new Vector2f(WindowParams.widthWindow - 470, 500), new Vector2f(400, 30), 35, $"message: ")));
            button.Clicked += (() => scene.Find<InputField>().EndWrite += Program.game.UseChat);
            button.Clicked += AddElementsToScene;
            scene.Add(button);
            return scene;
        }
        private static Scene CreateMapCastScene()
        {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg3));

            var map = new Map(30);

            var map2 = new Map(0);
            map2.CreateCells();
            map2.Move();
            map2.UpdateActive();

            var button = new Button(new Vector2f(500, 500), new Vector2f(150, 60), "Create auto");
            button.Clicked += map.CreateCells;
            button.Clicked += scene.Destroy<InputField>;
            scene.Add(button);

            button = new Button(new Vector2f(80, WindowParams.heightWindow - 80), new Vector2f(120, 40), "main menu");
            button.Clicked += scene.Destroy<Button>;
            button.Clicked += Program.game.MainMenu;
            scene.Add(new Chat(new Vector2f(WindowParams.widthWindow - 470, 70)) { IsActive = false});
            scene.Add(button);
            scene.Add(map);
            scene.Add(map2);
            return scene;
        }
    }
}
