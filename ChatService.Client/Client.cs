using ChatService.Infrastructure;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

        public void Listen()
        {
            // we should listen our client, so this is an infinite loop.
            while (true)
            {
                string message = GetMessage();
                SendMessage(message);
            }
        }

        private void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Message can't be null!");
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            Socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private string GetMessage()
        {
            Console.WriteLine("Type your message: ....");
            return Console.ReadLine();
        }
    }
}
