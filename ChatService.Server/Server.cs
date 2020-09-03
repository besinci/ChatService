using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatService.Server
{
    public class Server
    {
        public Socket ServerSocket { get; private set; }
        public int PORT { get; private set; }
        public Server(int port)
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            PORT = port;
        }

        private const int _bufferSize = 2048;
        private static readonly byte[] _buffer = new byte[_bufferSize];

        public void StartServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            ServerSocket.Bind(endPoint);

            ServerSocket.Listen(0);
            ServerSocket.BeginAccept(Connect, null); 
        }

        private void Connect(IAsyncResult result)
        {
            Socket socket;
            try
            {
                // accept the connection and set to a new socket.
                socket = ServerSocket.EndAccept(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return;
            }

            socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, Listen, socket);
        }

        private static void Listen(IAsyncResult result)
        {
            Socket current = (Socket)result.AsyncState;
            int received;

            try
            {
                // get the text.
                received = current.EndReceive(result);
            }
            catch (Exception ex)
            {
                current.Close();
                Console.WriteLine("Error: {0}", ex.Message);
                return;
            }

            ShowMessage(received);

            SendInformation(current);

            // Calling same method again, recursive for obvious reasons...
            current.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, Listen, current);
        }

        private static void SendInformation(Socket currentSocket)
        {
            byte[] response = Encoding.ASCII.GetBytes("Message successfuly delivered.");
            currentSocket.Send(response);
        }

        private static void ShowMessage(int received)
        {
            byte[] recBuf = new byte[received];
            Array.Copy(_buffer, recBuf, received);
            string message = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Message: " + message);
        }
    }
}
