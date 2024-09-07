using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;
using testUwp.Services;

namespace testUwp.Repository
{
    public interface IAccountRepository : IService
    {
        Task<int> CreateAsync(CurrencyType currencyType, CancellationToken cancellationToken = default);
        Task<Account> GetAsync(int accountId, CancellationToken cancellationToken = default);
    }
}
