using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatService.Client
{
    public class Client
    {
        public int ID { get; private set; }
        public int PORT { get; private set; }
        public Socket ClientSocket { get; private set; }

        public Client(int port)
        {
            ID = new Random().Next(0, 100);
            PORT = port;
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            while (ClientSocket.Connected == false)
            {
                try
                {
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception: {0}", ex.Message);
                    return;
                }
            }
        }


    }
}
