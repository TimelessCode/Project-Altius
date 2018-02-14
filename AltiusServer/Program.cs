using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltiusServer
{
    class Program
    {

        static void setupserver(int port, out NetServer server)
        {

            var config = new NetPeerConfiguration("ProjAltius")
            { Port = port };
            try
            {
                server = new NetServer(config);
                server.Start();
                Console.WriteLine("Server online, listening on port :" + port);

            }
            catch (System.Net.Sockets.SocketException)
            {
                
                Console.WriteLine("Socket already in use, pick another");
                tryparsenum(out port);
                setupserver(port, out server);
              
            }

           

        }
        static void sendmessagetoall(string mes,NetServer server)
        {

            var message = server.CreateMessage();
            message.Write(mes );
            server.SendMessage(message, server.Connections,   NetDeliveryMethod.ReliableOrdered, 0);
        }

        static NetIncomingMessage FilterMessages(NetServer server)
        {


            NetIncomingMessage message;
            while ((message = server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // handle custom messages
                        return message;
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        Console.WriteLine(message.SenderConnection.Status.ToString());
                        return null;
                        
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        Console.WriteLine(message.ReadString());
                        return null;
                        break;

                    /* .. */
                    default:
                        Console.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        return null;
                        break;
                }
                return null;
            }
            return null;
        }
       static void tryparsenum(out int port)
        {
            try
            {
                port = int.Parse(Console.ReadLine());
                if (port > 65535) {
                    Console.WriteLine("invalid port number, check for any typos please!");
                    tryparsenum(out port);
                }
               
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You didn't enter a number, please enter one now!");
                tryparsenum(out port);

            }
            catch (FormatException)
            {
                Console.WriteLine("You entered something that wasn't a number.Enter a number please");
                tryparsenum(out port);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number is too high,port numbers only go up to 65535");
                tryparsenum(out port);
            }
        }
        static void Main(string[] args)
        {
            NetServer server; 
            int port = 0;

            String s = "";
            Console.WriteLine("Type the number of an option : \n 1.Host a game ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Pick a port number");
                    break;

   
                default:
                    Console.WriteLine("invalid number!");
                    break;
            }

            tryparsenum(out port);

            setupserver(port, out server);

            NetIncomingMessage inc;
            while (Console.ReadLine() != "quit") {


                inc = FilterMessages(server);
                if (Console.ReadLine() == "test")
                {
                    sendmessagetoall("testingbroadcast", server);

                }
            }
        }
    }
}
