using System;
using System.Diagnostics;
using System.Management;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if(args.Length == 0)
            {
                args = new string[1];
                args[0] = "0";
            }
            else if(args.Length != 1)
            {
                Console.WriteLine("Digite qual o id do servidor!");
                return;
            }

            int id = Int32.Parse(args[0]);

            if ( id > 1)
            {
                Console.WriteLine("Só temos 2 servidores possíveis ( 0 e 1 )");
                return;
            }

            List<int> port = new List<int> { 5000, 5001 };

            Console.WriteLine(port[id]);

            Server sv = new Server(port[id], id);
            sv.Listen();
            
        }

    }

}