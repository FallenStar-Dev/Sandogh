using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Bussiness.SendMessage
{
    interface ISendMessage
    {
        public void Send(string Reciver, string Message);
    }
}
