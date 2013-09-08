using System.Collections.ObjectModel;
using CorpISTask1.Model;
using GalaSoft.MvvmLight;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace CorpISTask1.ViewModel
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var context = ServiceLocator.Current.GetInstance<IContextManager>().Context;
            Customers = new ObservableCollection<CustomerViewModel>();;
            foreach (var customer in context.CUSTOMERs)
            {
                Customers.Add(new CustomerViewModel(customer));
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}