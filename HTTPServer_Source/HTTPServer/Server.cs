using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HTTPServer
{
    class Server
    {

        private Socket serverSocket;

        private bool running;
        private int id;
        private int port;

        MongoClient Client;
        MongoDatabaseBase MongoDB;
        MongoCollectionBase<BsonDocument> Collec;

        public Server(int port, int id)
        {
            this.port = port;
            this.id = id;
            this.running = true;

            Client = new MongoClient("mongodb://172.17.0.2:27017");
            MongoDB = (MongoDatabaseBase) Client.GetDatabase("WebServer");
            Collec = (MongoCollectionBase<BsonDocument>) MongoDB.GetCollection<BsonDocument>("Response");

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
                ClientThread clT = new ClientThread(clientSocket, id, Collec);
                clT.Start();

            }


        }

        public void Stop()
        {
            running = false;
        }


    }
}
