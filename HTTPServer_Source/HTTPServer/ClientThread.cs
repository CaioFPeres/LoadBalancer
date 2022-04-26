using System.Net;
using System.Net.Sockets;
using System.Text;

using MongoDB.Bson;
using MongoDB.Driver;

namespace HTTPServer
{
    class ClientThread : BaseThread
    {

        private Socket clientSocket;
        private int id;

        private MongoCollectionBase<BsonDocument> Collec;

        public ClientThread(Socket client, int id, MongoCollectionBase<BsonDocument> Collec)
        {
            this.id = id;
            this.clientSocket = client;
            this.Collec = Collec;
        }

        public override void RunThread()
        {
            byte[] content = new byte[1];
            string data = "";

            // bloqueia thread aguardando por resposta
            clientSocket.Receive(content);
            data += Encoding.ASCII.GetString(content, 0, 1);

            // enquanto ainda sobrar bytes, continua pegando no buffer de tamanho 1
            while (clientSocket.Available > 0)
            {
                clientSocket.Receive(content);
                data += Encoding.ASCII.GetString(content, 0, 1);
            }


            byte[] array = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nHost: " + IPAddress.Parse( ((IPEndPoint)this.clientSocket.RemoteEndPoint).Address.ToString() ) + "\r\nUser - Agent: Server\r\nContent-Type: text/html\r\n\nEssa eh uma pagina 'html' do server " + this.id);


            clientSocket.Send(array);
            InsertInDB();

            //fecha socket
            clientSocket.Close();
        }

        private void InsertInDB()
        {
            var document = new BsonDocument
            {
                {"HTTPResponse","Essa eh uma pagina 'html' do server " + this.id},
            };

            Collec.InsertOne(document);

        }
    }
}
