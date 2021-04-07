using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
namespace Project
{
    public class Map : GameObject
    {
        public Cell[,] cells = new Cell[6, 6];
        public Map()
        {
            IsActive = false;
        }
        public void CreateCells()
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    var cell = new Cell(new Vector2f(y * 70, x * 70), new Vector2f(70, 70));
                    cell.OnClicked += UseCell;
                    cells[y, x] = cell;
                }
            }
            IsActive = true;
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
