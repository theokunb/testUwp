using System.Windows.Input;
using testUwp.Core;
using testUwp.Repository;
using testUwp.View;

namespace testUwp.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;

        public AccountViewModel()
        {
            Balance = 12722.58f;
            _transactionRepository = ServiceLocator.Instance.Get<ITransactionRepository>();

            NavigateTransaction = new RelayCommand((param) => OnNavigateTransaction(param));
            NavigateHistory = new RelayCommand(param => OnNavigateHistory(param));
        }

        private float _balance;

        public float Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        public ICommand NavigateTransaction { get; private set; }
        public ICommand NavigateHistory { get; private set; }

        public void OnNavigateTransaction(object param)
        {
            Frame.Navigate(typeof(TransactionView));
        }

        public void OnNavigateHistory(object param)
        {
            Frame.Navigate(typeof(HistoryView));
        }
    }
}
