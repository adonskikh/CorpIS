using System;
using System.Collections.Generic;
using CorpIS.Task1.Lib.Messages;
using CorpIS.Task1.Lib.Model;
using CorpIS.Task1.WpfClient.ViewModel;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CorpIS.Task1.WpfClient
{
    public class MessageHandler
    {
        private MainViewModel _dataContext;

        public MessageHandler()
        {
            _dataContext = SimpleIoc.Default.GetInstance<MainViewModel>();
        }
        public void HadleMessage(SocketMessage msg)
        {
            Console.WriteLine("SocketMsg");
        }

        public void HadleMessage(GetCustomersResponse msg)
        {
            foreach (var customer in msg.Customers)
            {
                var oldCustomer = _dataContext.Customers.SingleOrDefault(c => c.Id == customer.Id);
                if (oldCustomer != null)
                {
                    oldCustomer.Name = customer.Name;
                    oldCustomer.Balance = customer.Balance;
                }
                else
                {
                    _dataContext.Customers.Add(new CustomerViewModel(customer));
                }

            }
        }
    }
}
