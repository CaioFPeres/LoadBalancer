using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LoadBalancer
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

        public Client (Socket client)
        {
            this.socket = client;
        }


        private void ConnectSocket()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);

            this.socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            this.socket.Connect(ipe);
        }

        public void CloseSocket()
        {
            this.socket.Close();
        }


        public string MakeRequest(string request)
        {

            if (!this.socket.Connected)
            {
                this.ConnectSocket();
            }

            if (this.socket == null)
                return ("Connection failed");


            byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
            byte[] receiveBuffer = new byte[1];
            string data = "";

            // envia
            this.socket.Send(sendBuffer);

            //bloqueia thread aguardando por resposta
            this.socket.Receive(receiveBuffer);
            data = data + Encoding.ASCII.GetString(receiveBuffer, 0, 1);

            // enquanto ainda sobrar bytes, continua pegando no buffer de tamanho 1
            while (this.socket.Available > 0)
            {
                this.socket.Receive(receiveBuffer);
                data = data + Encoding.ASCII.GetString(receiveBuffer, 0, 1);
            }
            
            //fecha socket
            this.socket.Close();

            return data;
        }

        public string ReceiveRequest()
        {

            byte[] content = new byte[1];
            string data = "";

            // bloqueia thread aguardando por resposta do CLIENT
            socket.Receive(content);
            data += Encoding.ASCII.GetString(content, 0, 1);

            // enquanto ainda sobrar bytes, continua pegando no buffer de tamanho 1
            while (socket.Available > 0)
            {
                socket.Receive(content);
                data += Encoding.ASCII.GetString(content, 0, 1);
            }

            return data;
        }

        public void SendResponse(string response)
        {

            byte[] sendBuffer = Encoding.ASCII.GetBytes(response);

            // envia
            this.socket.Send(sendBuffer);

            //fecha socket
            this.socket.Close();

        }

    }
}
