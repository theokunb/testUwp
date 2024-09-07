using SQLite;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Core;
using testUwp.Model;

namespace testUwp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public AccountRepository(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Account>().Wait();

            _ = new NotifyTaskCompletion<int>(CreateAsync(CurrencyType.RUB));
        }

        public async Task<int> CreateAsync(CurrencyType currencyType, CancellationToken cancellationToken = default)
        {
            var account = await _connection.Table<Account>().FirstOrDefaultAsync(element => element.Currency == currencyType);

            if (account != null)
            {
                return account.Id;
            }

            account = new Account()
            {
                Currency = currencyType,
                Amount = 1412420.45343
            };

            await _connection.InsertAsync(account);
            return account.Id;
        }

        public async Task<Account> GetAsync(int accountId, CancellationToken cancellationToken = default) =>
            await _connection.Table<Account>().FirstOrDefaultAsync(element => element.Id == accountId);
    }
}
