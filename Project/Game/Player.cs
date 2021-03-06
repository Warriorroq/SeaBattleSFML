using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Project
{
    public class Player
    {
        private Socket playerSocket = null;
        public PlayerData data;
        public string nikName = string.Empty;
        public bool shoot = true;
        public int lost = 0;
        public int wins = 0;
        public Player(Socket socket)
        {
            playerSocket = socket;
        }
        public Socket GetSocket()
            => playerSocket;
        public void SetNickName(InputField nik)
        {
            nikName = nik.Text;
            Console.WriteLine(nikName);
        }
    }
}
