using CorpIS.Task1.Lib.Messages;
using CorpIS.Task1.Lib.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace CorpIS.Task1.WpfClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CustomerViewModel : ViewModelBase
    {
        private readonly Customer _customer;
        private readonly TcpSocketManager _socketManager;

        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// </summary>
        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
            _socketManager = SimpleIoc.Default.GetInstance<TcpSocketManager>();
            AddMoneyCommand = new RelayCommand<float>(AddMoney);
            TakeMoneyCommand = new RelayCommand<float>(TakeMoney);
        }

        public long Id
        {
            get { return _customer.Id; }
        }

        public string Name
        {
            get { return _customer.Name; }
            set
            {
                if (_customer.Name != value)
                {
                    _customer.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public float Balance
        {
            get { return _customer.Balance; }
            set
            {
                if (_customer.Balance != value)
                {
                    _customer.Balance = value;
                    RaisePropertyChanged(() => Balance);
                }
            }
        }

        public RelayCommand<float> AddMoneyCommand { get; private set; }
        public RelayCommand<float> TakeMoneyCommand { get; private set; }

        private void AddMoney(float value)
        {
            //Balance += value;
            _socketManager.SendMessage(new ChangeBalanceRequest() { Difference = 50, CustomerID = _customer.Id});
        }

        private void TakeMoney(float value)
        {
            //Balance -= value;
            _socketManager.SendMessage(new ChangeBalanceRequest() { Difference = -50, CustomerID = _customer.Id });
        }
    }
}