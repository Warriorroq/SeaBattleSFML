using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
namespace Project
{
    public class Map : GameObject
    {
        public Cell[,] cells = new Cell[6, 6];
        public int shipChance = 0;
        public Map(int shipChance)
        {
            IsActive = false;
            this.shipChance = shipChance;
        }
        public void UpdateActive()
        {
            IsActive = !IsActive;
            UpdateCellActive();
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
            if (Program.lobby.mainPlayer.shoot && cell.currentType == Cell.celltype.water)
            {
                byte[] shot = CommandConverter.ShotToBytes(2, new int[] { cell.Position.X, cell.Position.Y });
                Program.lobby.Send(shot);
                Program.lobby.mainPlayer.shoot = false;
            }
        }
        public void UpdateCell(int x, int y)
        {
            int x1 = x - 70;
            int y1 = y - 70;
            x1 /= 70;
            y1 /= 70;
            if (cells[y1, x1].currentType == Cell.celltype.ship)
            {
                cells[y1, x1].ChangeType(Cell.celltype.destroyedShip);
                Program.lobby.mainPlayer.shoot = false;
                byte[] shot = CommandConverter.ShotToBytes(3, new int[] { x, y });
                Program.lobby.Send(shot);
            }
            if (cells[y1, x1].currentType == Cell.celltype.water)
            {
                cells[y1, x1].ChangeType(Cell.celltype.miss);
                byte[] shot = CommandConverter.ShotToBytes(4, new int[] { x, y });
                Program.lobby.Send(shot);
                Program.lobby.mainPlayer.shoot = true;
            }
        }
        public void UpdateCell(int x, int y, Cell.celltype type)
        {
            int x1 = x - 70;
            int y1 = y - 70;
            x1 /= 70;
            y1 /= 70;
            cells[y1, x1].ChangeType(type);
        }
    }
}
