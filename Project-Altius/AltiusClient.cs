using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Project_Altius
{
   public class AltiusClient
    {
        NetClient myClient;

       public AltiusClient()
        {
            var config = new NetPeerConfiguration("ProjAltius");
             myClient = new NetClient(config);
          
        }

       public void connect(int _port)
        {
            myClient.Start();
            myClient.Connect(host: "127.0.0.1", port: _port);
        }

        public void checkformessages()
        {
            NetIncomingMessage message;
            while ((message = myClient.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // handle custom messages
                        var data = message.ReadString();
                         System.Diagnostics.Debug.WriteLine (data);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                       
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        Console.WriteLine(message.ReadString());
                        break;

                    /* .. */
                    default:
                        Console.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        break;
                }
            }
        }
    }
}
