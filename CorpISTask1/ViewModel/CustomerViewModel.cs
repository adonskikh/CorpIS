using CorpISTask1.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace CorpISTask1.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CustomerViewModel : ViewModelBase
    {
        private readonly CUSTOMER _customer;
        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// </summary>
        public CustomerViewModel(CUSTOMER customer)
        {
            _customer = customer;
            AddMoneyCommand = new RelayCommand<float>(AddMoney);
            TakeMoneyCommand = new RelayCommand<float>(TakeMoney);
        }

        public string Name
        {
            get { return _customer.NAME; }
            set
            {
                if (_customer.NAME != value)
                {
                    _customer.NAME = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public float Balance
        {
            get { return _customer.BALANCE; }
            set
            {
                if (_customer.BALANCE != value)
                {
                    _customer.BALANCE = value;
                    RaisePropertyChanged(() => Balance);
                }
            }
        }

        public RelayCommand<float> AddMoneyCommand { get; private set; }
        public RelayCommand<float> TakeMoneyCommand { get; private set; }

        private void AddMoney(float value)
        {
            Balance += value;
            ServiceLocator.Current.GetInstance<IContextManager>().Context.SaveChanges();
        }

        private void TakeMoney(float value)
        {
            Balance -= value;
            ServiceLocator.Current.GetInstance<IContextManager>().Context.SaveChanges();
        }
    }
}