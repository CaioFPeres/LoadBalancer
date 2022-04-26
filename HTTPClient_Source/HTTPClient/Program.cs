namespace HTTPClient
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length == 0)
            {
                Console.WriteLine("Quantidade padrao: 1 client e 1 request\n");
                args = new string[2];
                args[0] = "1";
                args[1] = "1";
            }
            else if (args.Length == 1)
            {
                Console.WriteLine("Insira quantos clientes deseja simular! (Numero de Threads)\n");
                return;
            }
            else if(args.Length > 2)
            {
                Console.WriteLine("Muitos argumentos!\n");
                return;
            }


            List<ClientThread> threadList= new List<ClientThread>();


            for(int i = 0; i < Int32.Parse(args[0]); i++)
            {
                threadList.Add(new ClientThread(new Client("172.17.0.5", 3000), Int32.Parse(args[1]), i));
                threadList[i].Start();
            }


            while (threadList[Int32.Parse(args[0]) - 1].IsAlive)
            {
                Thread.Sleep(100);
            }

        }
    }

}