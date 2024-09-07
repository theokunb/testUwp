using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Quotes
{
    public interface IQuoteService : IService
    {
        Task<Quote> GetCurrentQuoteAsync(CancellationToken cancellationToken = default);
    }
}
