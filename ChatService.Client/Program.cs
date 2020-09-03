using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientId = GenerateClientId();
            Console.Title = $"The Client #{ clientId }";


        }

        private static int GenerateClientId()
        {
            return new Random().Next(0, 100);
        }
    }
}
