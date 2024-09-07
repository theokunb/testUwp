using System.Threading;
using System.Threading.Tasks;

namespace testUwp.Services.CurrencyConvert
{
    public interface ICurrencyConvertService : IService
    {
        Task<double> ConvertToAsync(double amount, string currencyCode, CancellationToken cancellationToken = default);
    }
}
