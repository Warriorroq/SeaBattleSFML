using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Project
{
    public class Cell : GameObject
    {
        public enum celltype
        {
            water = 0,
            ship = 1,
            destroyedShip = 2,
            miss = 3,
        }

        private Shape shape = null;
        public event OnClick OnClicked;
        public delegate void OnClick(Cell cell);
        public celltype currentType = celltype.water;
        public Vector2f Position
        {
            get
                => shape.Position;
        }
        public Shape GetShape {
            get 
                => shape;
        }

        public Cell(Vector2f position, Vector2f size, int chance)
        {
            drawLayer = 4;
            if (Game.random.Next(0, 100) < chance)
                currentType = celltype.ship;
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = new Color(Color.White.R, Color.White.G, Color.White.B, (byte)(Color.White.A - 75)),
                OutlineThickness = 1f,
                OutlineColor = Color.Black
            };
            shape.Texture = currentType == celltype.water ? null : GameTextures.ship;
            WindowParams.renderWindow.MouseButtonPressed += GlobalClick;
        }
        public void SetShape(Shape shape)
            => this.shape = shape;
        private void GlobalClick(object sender, MouseButtonEventArgs e)
        {
            Vector2f mappedpos = WindowParams.renderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y));
            if (shape.GetGlobalBounds().Contains(mappedpos.X, mappedpos.Y) && IsActive)
                OnClicked?.Invoke(this);
        }
        public override void Draw()
            =>WindowParams.renderWindow.Draw(shape);
        public override void Destroy()
        {
            WindowParams.renderWindow.MouseButtonPressed -= GlobalClick;
            OnClicked = null;
        }
    }
}
