using System;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Core;
using testUwp.Model;
using testUwp.Repository;
using testUwp.Services.CurrencyConvert;

namespace testUwp.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ICurrencyConvertService _currencyConvertService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public AccountService()
        {
            _currencyConvertService = ServiceLocator.Instance.Get<ICurrencyConvertService>();
            _accountRepository = ServiceLocator.Instance.Get<IAccountRepository>();
            _transactionRepository = ServiceLocator.Instance.Get<ITransactionRepository>();
            _currencyRepository = ServiceLocator.Instance.Get<ICurrencyRepository>();
        }

        public async Task ApplyOperationAsync(int accountId, OperationType operationType, double amount, string currecyCode, CancellationToken cancellationToken = default)
        {
            var rubValue = await _currencyConvertService.ConvertToRubAsync(amount, currecyCode, cancellationToken);
            var account = await _accountRepository.GetAsync(accountId);
            var currency = await _currencyRepository.GetByCodeAsync(currecyCode, cancellationToken);

            var transaction = new Model.Transaction
            {
                Amount = amount,
                CreatedOn = DateTime.Now,
                Currency = currency.Type,
                TransactionType = operationType.TransactionType
            };

            await _transactionRepository.CreateAsync(transaction);
            await ChangeAccountAmount(account, rubValue, operationType.TransactionType);
        }

        private Task<int> ChangeAccountAmount(Model.Account account, double rubValue, TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Deposit:
                    account.Amount += rubValue;
                    break;
                case TransactionType.Withdrawal:
                    account.Amount -= rubValue;
                    break;
            }

            return _accountRepository.UpdateAsync(account);
        }
    }
}
