using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using testUwp.Core;
using testUwp.Repository;
using testUwp.Services.CurrencyConvert;
using testUwp.View;

namespace testUwp.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyConvertService _currencyConvertService;

        public AccountViewModel()
        {
            _currencyRepository = ServiceLocator.Instance.Get<ICurrencyRepository>();
            _accountRepository = ServiceLocator.Instance.Get<IAccountRepository>();
            _currencyConvertService = ServiceLocator.Instance.Get<ICurrencyConvertService>();

            Currencies = new ObservableCollection<string>();
            CommandNavigateTransaction = new RelayCommand((param) => OnNavigateTransaction(param));
            CommandNavigateHistory = new RelayCommand(param => OnNavigateHistory(param));
            CommandCurrencyChanged = new RelayCommand(param => OnCurrencyChanged(param));

            Init();
        }

        private async void Init()
        {
            var account = await _accountRepository.GetAsync(1);
            var currencies = await _currencyRepository.GetListAsync();

            foreach (var currency in currencies)
            {
                Currencies.Add(currency.Title);
            }

            Balance = account.Amount;
            var accountCurrency = currencies.Where(element => element.Type == account.Currency).FirstOrDefault();
            SelectedCurrencyIndex = accountCurrency.Id - 1;
        }

        private double _balance;
        private int _selectedCurrencyIndex;

        public string DisplayBalance => _balance.ToString("n2");

        public double Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
                OnPropertyChanged(nameof(DisplayBalance));
            }
        }
        public int SelectedCurrencyIndex
        {
            get => _selectedCurrencyIndex;
            set
            {
                _selectedCurrencyIndex = value;
                OnPropertyChanged(nameof(SelectedCurrencyIndex));
            }
        }

        public ObservableCollection<string> Currencies { get; private set; }
        public ICommand CommandNavigateTransaction { get; private set; }
        public ICommand CommandNavigateHistory { get; private set; }
        public ICommand CommandCurrencyChanged { get; private set; }

        public void OnNavigateTransaction(object param)
        {
            Frame.Navigate(typeof(TransactionView));
        }

        public void OnNavigateHistory(object param)
        {
            Frame.Navigate(typeof(HistoryView));
        }

        private async void OnCurrencyChanged(object param)
        {
            var account = await _accountRepository.GetAsync(1);

            Balance = await _currencyConvertService.ConvertToAsync(account.Amount, Currencies[SelectedCurrencyIndex]);
        }
    }
}
