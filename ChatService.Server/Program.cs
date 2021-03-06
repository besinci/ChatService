﻿using System;

namespace ChatService.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Server";
            
            Console.WriteLine("Activating server...");
            var server = new Server(100);
            server.StartServer();
            Console.WriteLine("Server is active.");

            // Server should wait.
            Console.WriteLine("Press any key to close server.");
            Console.ReadLine();
        }
    }
}
