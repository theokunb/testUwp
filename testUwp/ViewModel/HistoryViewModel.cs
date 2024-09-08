using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using testUwp.Core;
using testUwp.Model;
using testUwp.Repository;
using testUwp.Services.Transaction;

namespace testUwp.ViewModel
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionService _transactionService;

        public HistoryViewModel()
        {
            _transactionRepository = ServiceLocator.Instance.Get<ITransactionRepository>();
            _transactionService = ServiceLocator.Instance.Get<ITransactionService>();

            Transactions = new ObservableCollection<TransactionDto>();
            CommandNavigateBack = new RelayCommand(param => OnBackNavigate(param));
            CommandAutoGenerateColumns = new RelayCommand(param => OnAutoGenerateColumns(param));

            Init();
        }

        public ObservableCollection<TransactionDto> Transactions { get; private set; }
        public ICommand CommandNavigateBack { get; private set; }
        public ICommand CommandAutoGenerateColumns { get; private set; }

        private void OnBackNavigate(object param)
        {
            Frame.GoBack();
        }

        private async void Init()
        {
            var transactions = await _transactionRepository.GetTransactionsAsync();

            foreach (var transaction in transactions)
            {
                var dto = await _transactionService.ConvertToTransactionDto(transaction);
                Transactions.Add(dto);
            }
        }

        private void OnAutoGenerateColumns(object param)
        {
            var e = param as DataGridAutoGeneratingColumnEventArgs;

            switch (e.Column.Header.ToString())
            {
                case "Date":
                    {
                        e.Column.Header = "Дата";
                        break;
                    }
                case "Amount":
                    {
                        e.Column.Header = "Сумма";
                        break;
                    }
                case "Valute":
                    {
                        e.Column.Header = "Валюта";
                        break;
                    }
                case "OperationType":
                    {
                        e.Column.Header = "Тип операции";
                        break;
                    }
                default:
                    {
                        e.Cancel = true;
                        break;
                    }
            }
        }
    }
}
