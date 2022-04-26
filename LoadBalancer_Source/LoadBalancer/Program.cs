namespace LoadBalancer
{   
    class Program
    {
        static void Main(string[] args)
        {

            Server sv = new Server(3000);
            sv.Listen();

        }
    }
}