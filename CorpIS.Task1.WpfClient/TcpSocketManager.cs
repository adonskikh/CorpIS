using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CorpIS.Task1.Lib;
using CorpIS.Task1.Lib.Messages;

namespace CorpIS.Task1.WpfClient
{
    public class TcpSocketManager
    {
        private readonly MessageHandler _messageHandler;

        public TcpSocketManager()
        {
            _messageHandler = new MessageHandler();
        }

        public void SendMessage(SocketMessage msg)
        {
            try
            {
                var remoteEP = new IPEndPoint(IPAddress.Loopback, 11000);

                using (var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    var messenger = new TcpSocketMessenger(sender);

                    sender.Connect(remoteEP);

                    messenger.SendMessage(msg);

                    var response = messenger.ReceiveMessage();

                    //System.Windows.MessageBox.Show(string.Format("{0} {1}", response.GetType().AssemblyQualifiedName,response.Message));
                    _messageHandler.HadleMessage(response);

                    sender.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(string.Format("Unexpected exception : {0}", e.ToString()));
            }
        }
    }
}
