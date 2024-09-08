using System.Globalization;
using System.Threading.Tasks;
using testUwp.Core;
using testUwp.Model;
using testUwp.Repository;
using testUwp.Services.Operations;
using testUwp.Services.Transaction;

namespace testUwp.Services.Help
{
    public class TransactionService : ITransactionService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IOperationService _operationService;

        public TransactionService()
        {
            _currencyRepository = ServiceLocator.Instance.Get<ICurrencyRepository>();
            _operationService = ServiceLocator.Instance.Get<IOperationService>();
        }

        public async Task<TransactionDto> ConvertToTransactionDto(Model.Transaction transaction)
        {
            var transactionDto = new TransactionDto();
            var currency = await _currencyRepository.GetAsync(transaction.Currency);
            var cultureInfo = new CultureInfo(currency.CurrencyFormat);
            cultureInfo.NumberFormat.CurrencyPositivePattern = 0;
            var operation = await _operationService.GetOperation(transaction.TransactionType);

            transactionDto.Date = transaction.CreatedOn.ToShortDateString();
            transactionDto.Amount = transaction.Amount.ToString(transaction.Amount % 1 == 0 ? "C0" : "C", cultureInfo);
            transactionDto.Valute = currency.Title;
            transactionDto.OperationType = operation.Description;

            return transactionDto;
        }
    }
}
