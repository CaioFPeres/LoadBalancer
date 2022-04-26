using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LoadBalancer
{
    class Server
    {

        private Socket serverSocket;
        private List<int> serverPorts;

        private bool running;
        private int port;

        private int countingReq;

        public Server(int port)
        {
            this.port = port;
            this.running = true;
            this.countingReq = 0;

            serverPorts = new List<int>() { 5000, 5001 };
            this.Bind();
        }


        private void Bind()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ipe);
        }


        public void Listen()
        {
            if (!this.serverSocket.IsBound)
            {
                this.Bind();
            }

            while (running)
            {

                serverSocket.Listen();
                Socket clientSocket = serverSocket.Accept();

                // cada Conexão aceita é uma thread
                ComChannel clT = new ComChannel(new Client(clientSocket), serverPorts[countingReq++]);
                clT.Start();

                if(countingReq > 1)
                    countingReq = 0;

            }


        }

        public void Stop()
        {
            running = false;
        }

    }
}
