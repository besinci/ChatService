using ChatService.Infrastructure;
using System;
using System.Net;
using System.Net.Sockets;


namespace ChatService.Client
{
    public class Client : BaseService
    {
        public int ID { get; private set; }

        public Client(int port)
        {
            ID = new Random().Next(0, 100);
            PORT = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            while (Socket.Connected == false)
            {
                try
                {
                    Socket.Connect(IPAddress.Loopback, PORT);
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
