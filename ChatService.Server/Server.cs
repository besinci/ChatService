using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Server
{
    public class Server
    {
        private static readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int _port = 100;
        private const int _bufferSize = 2048;
        private static readonly byte[] _buffer = new byte[_bufferSize];

        public static void StartServer()
        {
            Console.WriteLine("Activating server...");

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);
            _serverSocket.Bind(endPoint);

            _serverSocket.Listen(0);
            _serverSocket.BeginAccept(Connect, null);

            Console.WriteLine("Server is active.");
        }

        private static void Connect(IAsyncResult result)
        {
            Socket socket;
            try
            {
                // accept the connection and set to a new socket.
                socket = _serverSocket.EndAccept(result);
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
