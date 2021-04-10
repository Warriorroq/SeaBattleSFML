using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading.Tasks;

namespace Project
{
    public class Lobby
    {
        public static int port = 8005;
        public static string address = "127.0.0.1";
        public Player mainPlayer = new Player(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
        private IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        private List<Player> serverPlayers = null;
        public async void SetUpServer()
        {
            serverPlayers = new List<Player>();
            try
            {
                mainPlayer.GetSocket().Bind(ipPoint);

                mainPlayer.GetSocket().Listen(2);
                await Task.Run(() => ConnectPlayers());
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ReadMessage((byte[],int) data)
        {
            byte[] command = new byte[4];
            for (int i = 0; i < 4; i++)
                command[i] = data.Item1[i];
            var commandNum = CommandConverter.BytesToInt(command);
            if (commandNum == 1)
                Program.game.SendToChat(CommandConverter.BytesToString(data.Item1, data.Item2));
            else if (commandNum == 2)
            {
                var map = Program.game.scene.FindAll<Map>().Where(x => x.shipChance > 0).ToArray()[0];
                var shoot = CommandConverter.BytesToShot(data.Item1);
                map.UpdateCell(shoot[0], shoot[1]);
            }
            else if (commandNum == 3)
            {
                var map = Program.game.scene.FindAll<Map>().Where(x => x.shipChance == 0).ToArray()[0];
                var shoot = CommandConverter.BytesToShot(data.Item1);
                map.UpdateCell(shoot[0], shoot[1], Cell.celltype.destroyedShip);
                mainPlayer.shoot = true;
            }
            else if (commandNum == 4)
            {
                var map = Program.game.scene.FindAll<Map>().Where(x => x.shipChance == 0).ToArray()[0];
                var shoot = CommandConverter.BytesToShot(data.Item1);
                map.UpdateCell(shoot[0], shoot[1], Cell.celltype.miss);
                mainPlayer.shoot = false;
            }
        }
        public void ConnectPlayers()
        {
            while (serverPlayers.Count == 0)
            {
                var player = new Player(this.mainPlayer.GetSocket().Accept());
                serverPlayers.Add(player);
                Program.game.SendToChat($"Connected {player.nikName}");
            }
        }
        public void SetUpConnection()
        {
            try
            {
                mainPlayer.GetSocket().Connect(ipPoint);
                Console.WriteLine("Подключилось к серверу...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Send(byte[] data)
        {
            if (CommandConverter.BytesToInt(CommandConverter.RemoveBytes(0, 4, data)) == 1)
                Program.game.SendToChat(CommandConverter.BytesToString(data, data.Length));
            if (serverPlayers is null)
            {
                mainPlayer.GetSocket().Send(data);
                return;
            }
            ReSendData(data, null);
        }
        public void UpdateConnection()
        {
            if (!(serverPlayers is null))
            {
                UpdateSocketsInfo();
            }
            if(mainPlayer.GetSocket().Connected)
                WriteNewInformation();
        }
        public void Disconnect()
        {
            mainPlayer.GetSocket().Shutdown(SocketShutdown.Receive);
            mainPlayer.GetSocket().Close();
            Console.Read();
        }
        private void UpdateSocketsInfo()
        {
            foreach (var socket in serverPlayers)
            {
                if (socket.GetSocket().Available > 0)
                {
                    var data = ListenSocketReceive(socket.GetSocket());
                    ReadMessage(data);
                    ReSendData(data.Item1, socket.GetSocket());
                }
            }
        }
        private void WriteNewInformation()
        {
            if (mainPlayer.GetSocket().Available > 0)
            {
                var data = ListenSocketReceive(mainPlayer.GetSocket());
                ReadMessage(data);
            }
        }
        private void ReSendData(byte[] data, Socket socket)
        {
            foreach (var player in serverPlayers)
                if(player.GetSocket() != socket)
                    player.GetSocket().Send(data);
        }
        private static (byte[], int) ListenSocketReceive(Socket socket)
        {

            byte[] data = new byte[512];
            int bytes = 0;
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
            }
            while (socket.Available > 0);
            return (data, bytes);
        }
    }
}
