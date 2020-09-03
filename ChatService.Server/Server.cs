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
        private static readonly Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int _port = 100;
        private const int _bufferSize = 2048;
        private static readonly byte[] _buffer = new byte[_bufferSize];

        public static void StartServer()
        {
            Console.WriteLine("Server is about the start...");

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);
            _socket.Bind(endPoint);

            _socket.Listen(0);
            _socket.BeginAccept(CallBack, null);
        }

        private static void CallBack(IAsyncResult result)
        {
        }
    }
}
