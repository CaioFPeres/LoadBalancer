using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient
{
    class ClientThread : BaseThread
    {

        private Client cl;
        private int id;
        private int numOfReqs;

        public ClientThread(Client client, int numOfReqs, int id)
        {
            this.id = id;
            this.cl = client;
            this.numOfReqs = numOfReqs;
        }

        public override void RunThread()
        {
            // testando a capacidade máxima do servidor:
            for (int i = 0; i < numOfReqs; i++)
            {
                string webpage = cl.MakeRequest("");
            }
        }
    }
}
