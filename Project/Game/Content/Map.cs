using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
namespace Project
{
    public class Map : GameObject
    {
        public Cell[,] cells = new Cell[6, 6];
        private int shipChance = 0;
        public Map(int shipChance)
        {
            IsActive = false;
            this.shipChance = shipChance;
        }
        public void Move()
        {
            Vector2f position = new Vector2f(70, 70);
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    cells[y, x].GetShape.Position = new Vector2f(position.X + x * 70, position.Y + y * 70);
                }
            }
        }
        public void CreateCells()
        {
            DestroyCells();
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    var cell = new Cell(new Vector2f(350 + x * 70, 35 + y * 70), new Vector2f(70, 70), shipChance);
                    cell.OnClicked += UseCell;
                    cells[y, x] = cell;
                }
            }
            IsActive = true;
        }
        public void UpdateCellActive()
        {
            foreach (var cell in cells)
                cell.IsActive = IsActive;
        }
        public override void Destroy()
        {
            DestroyCells();
        }
        private void DestroyCells()
        {
            foreach (var cell in cells)
                cell?.Destroy();
        }
        public override void Draw()
        {
            foreach (var cell in cells)
                cell?.Draw();
        }
        public static void UseCell(Cell cell)
        {
            Console.WriteLine(cell.Position);
        }
    }
}
