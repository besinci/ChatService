﻿using System;
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
            Console.Title = "The Matrix";
            Server.StartServer();
        }
    }
}