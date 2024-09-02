using SQLite;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public TransactionRepository(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Transaction>().Wait();
        }

        public async Task<int> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default) =>
            await _connection.InsertAsync(transaction);

        public async Task<Transaction> GetTransactionAsync(int transactionId, CancellationToken cancellationToken = default) =>
            await _connection.Table<Transaction>().Where(element => element.Id == transactionId).FirstOrDefaultAsync();

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default) =>
            await _connection.Table<Transaction>().ToListAsync();
    }
}
