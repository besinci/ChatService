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
            var client = new Client(100);
            Console.Title = $"The Client #{ client.ID }";


        }


    }
}
