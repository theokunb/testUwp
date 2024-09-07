using System.Threading;
using System.Threading.Tasks;

namespace testUwp.Services.CurrencyConvert
{
    public interface ICurrencyConvertService : IService
    {
        Task<double> ConvertFromRubAsync(double amount, string currencyCode, CancellationToken cancellationToken = default);
        Task<double> ConvertToRubAsync(double amount, string currencyCode, CancellationToken cancellationToken = default);
    }
}
