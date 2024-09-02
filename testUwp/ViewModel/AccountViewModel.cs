using System.Windows.Input;
using testUwp.Core;
using testUwp.View;

namespace testUwp.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {
            Balance = 12722.58f;

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
