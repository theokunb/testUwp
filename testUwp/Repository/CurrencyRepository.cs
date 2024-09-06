using SQLite;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public CurrencyRepository(string path)
        {
            _connection = new SQLiteAsyncConnection(path);

            CreateCurrecies();
        }

        private async void CreateCurrecies()
        {
            await _connection.CreateTableAsync<Currency>();

            await CreateAsync(CurrencyType.USD, "Доллар");
            await CreateAsync(CurrencyType.EUR, "Евро");
            await CreateAsync(CurrencyType.RUB, "Рубль");
        }

        public async Task<int> CreateAsync(CurrencyType currencyType, string title, CancellationToken cancellationToken = default)
        {
            var existedCurrency = await _connection.Table<Currency>().FirstOrDefaultAsync(element => element.Type == currencyType);

            if(existedCurrency != null)
            {
                return 1;
            }

            var createCurrency = new Currency()
            {
                Type = currencyType,
                Title = title
            };

            return await _connection.InsertAsync(createCurrency);
        }

        public async Task<Currency> GetAsync(CurrencyType currencyType, CancellationToken cancellationToken = default)
        {
            return await _connection.Table<Currency>().Where(element => element.Type == currencyType).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Currency>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _connection.Table<Currency>().ToListAsync();
        }

        public async Task<int> RemoveAsync(CurrencyType currencyType, CancellationToken cancellationToken = default)
        {
            var currency = await _connection.Table<Currency>().Where(element => element.Type == currencyType).FirstOrDefaultAsync();

            if(currency != null)
            {
                return await _connection.DeleteAsync(currency);
            }

            return 1;
        }
    }
}
