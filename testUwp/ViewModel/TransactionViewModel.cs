using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using testUwp.Core;
using testUwp.Model;
using testUwp.Repository;
using testUwp.Services.Account;
using testUwp.Services.Help;
using testUwp.Services.Operations;

namespace testUwp.ViewModel
{
    public class TransactionViewModel : BaseViewModel
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IOperationService _operationService;
        private readonly IAccountService _accountService;

        public TransactionViewModel()
        {
            _currencyRepository = ServiceLocator.Instance.Get<ICurrencyRepository>();
            _operationService = ServiceLocator.Instance.Get<IOperationService>();
            _accountService = ServiceLocator.Instance.Get<IAccountService>();

            Currencies = new ObservableCollection<string>();
            Operations = new ObservableCollection<string>();

            CommandNavigateBack = new RelayCommand(param => OnBackNavigate(param));
            CommandOperationValueChanged = new RelayCommand(param => OnOperationValueChanged(param));
            CommandWriteOperation = new RelayCommand(param => OnWriteOperation(param));

            Init();
        }

        private double _operationValue;
        private string _operationValueStr;
        private int _selectionText;
        private int _selectionLenght;
        private int _selectedCurrencyIndex;
        private int _selectedOperationType;

        public string OperationValueStr
        {
            get => _operationValueStr;
            set
            {
                _operationValueStr = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(WriteButtonActive));
            }
        }
        public int SelectionText
        {
            get => _selectionText;
            set
            {
                _selectionText = value;
                OnPropertyChanged();
            }
        }
        public int SelectionLenght
        {
            get => _selectionLenght;
            set
            {
                _selectionLenght = value;
                OnPropertyChanged();
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
        public int SelectedOperationType
        {
            get => _selectedOperationType;
            set
            {
                _selectedOperationType = value;
                OnPropertyChanged();
            }
        }
        public bool WriteButtonActive => OperationValueStr != null && OperationValueStr.Length > 0;

        public ObservableCollection<string> Currencies { get; private set; }
        public ObservableCollection<string> Operations { get; private set; }
        public ICommand CommandNavigateBack { get; private set; }
        public ICommand CommandOperationValueChanged { get; private set; }
        public ICommand CommandWriteOperation { get; private set; }

        private void OnBackNavigate(object param)
        {
            Frame.GoBack();
        }

        private void OnOperationValueChanged(object param)
        {
            if (DigitHelper.IsDigit(OperationValueStr))
            {
                _operationValue = Convert.ToDouble(DigitHelper.MakeDigit(OperationValueStr));
            }
            else
            {
                OperationValueStr = DigitHelper.MakeDigit(OperationValueStr);
                SelectionText = OperationValueStr.Length;
                SelectionLenght = 0;
            }
        }

        private async void Init()
        {
            //валюты
            var currencies = await _currencyRepository.GetListAsync();
            foreach (var currency in currencies)
            {
                Currencies.Add(currency.Title);
            }
            var rubIndex = Currencies.IndexOf(CurrencyType.RUB.ToString());
            SelectedCurrencyIndex = rubIndex != -1 ? rubIndex : 0;

            //операции
            var operations = await _operationService.GetOperationsAsync();
            foreach (var operation in operations)
            {
                Operations.Add(operation.Description);
            }
            SelectedOperationType = 0;
        }

        private async void OnWriteOperation(object param)
        {
            var operation = await _operationService.GetOperationByDescriptionAsync(Operations[SelectedOperationType]);
            var currencyCode = Currencies[SelectedCurrencyIndex];

            await _accountService.ApplyOperationAsync(1, operation, _operationValue, currencyCode);
        }
    }
}
