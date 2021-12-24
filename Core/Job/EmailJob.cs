using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Job
{
   public static class EmailJob
    {
        public static void SendMail(string to)
        {
            Console.WriteLine($"{to} Aramıza hoş geldiniz ");
        }
    }
}
