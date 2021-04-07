using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public static class SceneFabric
    {
        public static Scene CreateStartScene() {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg1));

            var button = new Button(new Vector2f(500, 500), new Vector2f(100, 60), "Create room");            
            button.OnClicked += Program.game.StartServer;
            scene.Add(button);

            button = new Button(new Vector2f(500, 600), new Vector2f(100, 60), "Connect room");
            button.OnClicked += Program.game.Connect;
            scene.Add(button);

            scene.Add(new InputField(new Vector2f(500, 400), new Vector2f(100, 30), $"NickName {Game.random.Next(0,100)}"));
            return scene;
        }
        public static Scene CreateMapScene()
        {
            var scene = new Scene();
            scene.Add(new Image(new Vector2f(0, 0), new Vector2f(WindowParams.widthWindow, WindowParams.heightWindow), GameTextures.bg3));
            var map = new Map();
            var button = new Button(new Vector2f(500, 500), new Vector2f(100, 60), "Create auto");
            button.OnClicked += map.CreateCells;
            button.OnClicked += scene.DisposeAll<InputField>;
            button.OnClicked += scene.DisposeAll<Button>;
            scene.Add(map);
            scene.Add(button);
            return scene;
        }
    }
}
