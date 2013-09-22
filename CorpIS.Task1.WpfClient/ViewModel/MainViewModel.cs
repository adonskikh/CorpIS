using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace CorpIS.Task1.WpfClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerViewModel> Customers { get; private set; }

        private TcpSocketManager _socketManager;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Customers = new ObservableCollection<CustomerViewModel>();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}