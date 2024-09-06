using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;
using testUwp.Services;

namespace testUwp.Repository
{
    public interface ICurrencyRepository : IService
    {
        Task<int> CreateAsync(CurrencyType currencyType, string title, CancellationToken cancellationToken = default);
        Task<Currency> GetAsync(CurrencyType currencyType, CancellationToken cancellationToken = default);
        Task<int> RemoveAsync(CurrencyType currencyType, CancellationToken cancellationToken = default);
        Task<IEnumerable<Currency>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
