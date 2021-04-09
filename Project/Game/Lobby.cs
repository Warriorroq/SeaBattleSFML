using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Lobby
    {
        public static int port = 8005;
        public static string address = "127.0.0.1";
        public Player mainSocket = new Player(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
        private IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        private List<Player> serverPlayers = null;
        public async void SetUpServer()
        {
            serverPlayers = new List<Player>();
            try
            {
                mainSocket.GetSocket().Bind(ipPoint);

                mainSocket.GetSocket().Listen(2);
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
            for (int i = 0; i < data.Item1.Length; i++)
                command[i] = data.Item1[i];
        }
        public void ConnectPlayers()
        {
            while (serverPlayers.Count == 0)
            {
                var player = new Player(this.mainSocket.GetSocket().Accept());
                serverPlayers.Add(player);
                Program.game.SendToChat($"Connected {player.nikName}");
            }
        }
        public void SetUpConnection()
        {
            try
            {
                mainSocket.GetSocket().Connect(ipPoint);
                Console.WriteLine("Подключилось к серверу...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Send(string str)
        {
            if (serverPlayers is null)
            {
                mainSocket.GetSocket().Send(Converter.StringToBytes(str));
                return;
            }

            ReSendData(Converter.StringToBytes(str));
        }
        public void UpdateConnection()
        {
            if (!(serverPlayers is null))
            {
                UpdateSocketsInfo();
            }
            if(mainSocket.GetSocket().Connected)
                WriteNewInformation();
        }
        public void Disconnect()
        {
            mainSocket.GetSocket().Shutdown(SocketShutdown.Receive);
            mainSocket.GetSocket().Close();
            Console.Read();
        }
        private void UpdateSocketsInfo()
        {
            foreach (var socket in serverPlayers)
            {
                if (socket.GetSocket().Available > 0)
                {
                    var data = ListenSocketReceive(socket.GetSocket());
                    ReSendData(data.Item1);
                }
            }
        }
        private void WriteNewInformation()
        {
            if (mainSocket.GetSocket().Available > 0)
            {
                var data = ListenSocketReceive(mainSocket.GetSocket());
                Program.game.SendToChat(Converter.BytesToString(data.Item1, data.Item2));
            }
        }
        private void ReSendData(byte[] data)
        {
            foreach (var player in serverPlayers)
                player.GetSocket().Send(data);
            Program.game.SendToChat(Converter.BytesToString(data, data.Length));
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
