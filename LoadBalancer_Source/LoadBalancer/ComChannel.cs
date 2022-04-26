using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LoadBalancer
{
    class ComChannel : BaseThread
    {

        private Client receiveClient;
        private Client serverRequest;

        private int serverPort;


        public ComChannel(Client client, int serverPort)
        {
            this.receiveClient = client;
            this.serverPort = serverPort;
        }

        public override void RunThread()
        {

            if (this.serverPort == 5000)
                serverRequest = new Client("172.17.0.3", this.serverPort);
            else
                serverRequest = new Client("172.17.0.4", this.serverPort);

            // bloqueia thread aguardando por request do CLIENT
            string request = receiveClient.ReceiveRequest();

            // faz request para o server
            string response = serverRequest.MakeRequest(request);

            // devolve resposta para o client
            receiveClient.SendResponse(response);

        }
    }
}
