using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorpIS.Task1.Lib.Messages;
using CorpIS.Task1.Lib.Model;
using CorpIS.Task1.TcpServer.Model;

namespace CorpIS.Task1.TcpServer
{
    public class MessageHandler : IDisposable
    {
        private readonly EntityContext _context;

        public MessageHandler()
        {
            _context = new EntityContext();
        }
        public void HadleMessage(SocketMessage msg)
        {
            Console.WriteLine("SocketMsg");
        }

        public SocketMessage HadleMessage(ChangeBalanceRequest msg)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == msg.CustomerID);
            if (customer != null)
            {
                customer.Balance += msg.Difference;
                _context.SaveChanges();
            }
            var response = new GetCustomersResponse();
            response.Customers = new List<Customer>() { customer };
            return response;
        }

        public SocketMessage HadleMessage(GetCustomersRequest msg)
        {
            var response = new GetCustomersResponse();
            if (msg.CustomerIDs == null)
            {
                response.Customers = _context.Customers.ToList();
            }
            else
            {
                response.Customers = _context.Customers.Where(c => msg.CustomerIDs.Contains(c.Id)).ToList();
            }
            return response;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
