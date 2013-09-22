using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CorpIS.Task1.Lib;
using CorpIS.Task1.Lib.Model;
using CorpIS.Task1.TcpServer.Model;

namespace CorpIS.Task1.TcpServer
{
    class Program
    {
        private static MessageHandler _messageHandler;

        public static void StartListening()
        {
            var localEndPoint = new IPEndPoint(IPAddress.Loopback, 11000);

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();

                    var messenger = new TcpSocketMessenger(handler);
                    var msg = messenger.ReceiveMessage();

                    Console.WriteLine(string.Format(
                        "Received {0} from {1}:{2}", 
                        msg.GetType().Name, 
                        ((IPEndPoint) handler.RemoteEndPoint).Address,  
                        ((IPEndPoint) handler.RemoteEndPoint).Port));
                    
                    var response = _messageHandler.HadleMessage(msg);
                    if(response != null)
                        messenger.SendMessage(response);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        static void Main(string[] args)
        {
           /* Customer customer = new Customer() { BALANCE = 999, NAME = "Customer 1"};
            EntityContext context = new EntityContext();
            context.Customers.Add(customer);
            context.SaveChanges();
            foreach (var customer1 in context.Customers)
            {
                Console.WriteLine(customer1.ID + " " + customer1.NAME + " " + customer1.BALANCE);
            }*/
            _messageHandler = new MessageHandler();
            StartListening();
        }
    }
}
