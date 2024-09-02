using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;
using testUwp.Services;

namespace testUwp.Repository
{
    public interface ITransactionRepository : IService
    {
        Task<int> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task<Transaction> GetTransactionAsync(int transactionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default);
    }
}
