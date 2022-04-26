using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HTTPClient
{
    class Client
    {


        private Socket socket;
        private string ip;
        private int port;


        public Client(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.ConnectSocket();
        }


        private void ConnectSocket()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);

            this.socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            this.socket.Connect(ipe);
        }


        public string MakeRequest(string request)
        {

            if (!this.socket.Connected)
            {
                this.ConnectSocket();
            }

            if(request.Length == 0)
                request = "GET / HTTP/1.1\r\nHost: " + this.ip + ":" + this.port +
                            "\r\n";


            byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
            byte[] receiveBuffer = new byte[1];
            string page = "";


            if (this.socket == null)
                return ("Connection failed");


            // envia
            this.socket.Send(sendBuffer);

            //bloqueia thread aguardando por resposta
            this.socket.Receive(receiveBuffer);
            page = page + Encoding.ASCII.GetString(receiveBuffer, 0, 1);

            // enquanto ainda sobrar bytes, continua pegando no buffer de tamanho 1
            while (this.socket.Available > 0)
            {
                this.socket.Receive(receiveBuffer);
                page = page + Encoding.ASCII.GetString(receiveBuffer, 0, 1);
            }
            
            //fecha socket
            this.socket.Close();

            return page;
        }

    }
}
