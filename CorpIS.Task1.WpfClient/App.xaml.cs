using System.Windows;
using CorpIS.Task1.Lib.Messages;
using CorpIS.Task1.WpfClient.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Practices.ServiceLocation;

namespace CorpIS.Task1.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TcpSocketManager>();
            SimpleIoc.Default.GetInstance<TcpSocketManager>().SendMessage(new GetCustomersRequest() { CustomerIDs = null });
        }
    }
}
