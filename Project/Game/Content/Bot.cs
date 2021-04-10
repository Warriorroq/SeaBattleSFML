using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Project
{
    class Bot : Player
    {
        private List<(int, int)> shots = new List<(int, int)>();
        private int hp = 0;
        private int chance = 15;
        private int fires = 0;
        public Bot(Socket socket) : base(socket)
        {
            nikName = "__bot";
            hp = Game.random.Next(7, 12);
        }
        public void GetData(byte[] data)
        {
            ReadMessage(data);
        }
        public void ReadMessage(byte[] data)
        {
            byte[] command = new byte[4];
            for (int i = 0; i < 4; i++)
                command[i] = data[i];
            var commandNum = CommandConverter.BytesToInt(command);
            var intData = CommandConverter.BytesToInts(data);
            if (commandNum == 2)
            {
                Shoot(intData);
            }
            else if (commandNum == 3)
            {
                GetShoot(intData);
            }
        }
        private void Shoot(int[] data)
        {
            (int, int) shot = (0, 0);
            while (FindShot(shot))
                shot = (Game.random.Next(0, 6), Game.random.Next(0, 6));
            GetSocket().Send(CommandConverter.ShotToBytes(2, new int[] { shot.Item1, shot.Item2}));
        }
        private bool FindShot((int, int) shot)
        {
            foreach(var shooted in shots)
            {
                if (shooted == shot)
                    return true;
            }
            return false;
        }
        private void GetShoot(int[] data)
        {
            fires++;
            if (fires > 36 - hp)
                chance = 100;
            if (Game.random.Next(0, 100) < chance)
            {
                hp--;
                GetSocket().Send(CommandConverter.ShotToBytes(3, new int[] { data[0], data[1], hp }));
            }
            if (hp == 0)
                Restart();
        }
        private void Restart()
        {
            hp = Game.random.Next(7, 12);
            chance = 15;
            fires = 0;
        }
    }
}
