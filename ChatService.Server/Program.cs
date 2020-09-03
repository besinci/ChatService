using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Server";
            Server.StartServer();

            // Server should wait.
            Console.WriteLine("Press any key to close server.");
            Console.ReadLine();
        }
    }
}
