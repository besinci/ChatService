using ChatService.Infrastructure;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatService.Server
{
    public class Server : BaseService
    {
        public Server(int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            PORT = port;
        }

        private const int _bufferSize = 2048;
        private static readonly byte[] _buffer = new byte[_bufferSize];

        public void StartServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            Socket.Bind(endPoint);

            Socket.Listen(0);
            Socket.BeginAccept(Connect, null); 
        }

        private void Connect(IAsyncResult result)
        {
            Socket socket;
            try
            {
                // accept the connection and set to a new socket.
                socket = Socket.EndAccept(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return;
            }

            socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, Listen, socket);
            Socket.BeginAccept(Connect, null);
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
