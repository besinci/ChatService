using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Infrastructure
{
    public abstract class BaseService
    {
        public int PORT { get; set; }
        public Socket Socket { get; set; }
    }
}
