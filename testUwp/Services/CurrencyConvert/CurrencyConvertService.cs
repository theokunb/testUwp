using System.Threading;
using System.Threading.Tasks;
using testUwp.Core;
using testUwp.Model;
using testUwp.Services.Quotes;

namespace testUwp.Services.CurrencyConvert
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        private readonly IQuoteService _quoteService;

        public CurrencyConvertService()
        {
            _quoteService = ServiceLocator.Instance.Get<IQuoteService>();
        }

        public async Task<double> ConvertToAsync(double amount, string currencyCode, CancellationToken cancellationToken = default)
        {
            var quote = await _quoteService.GetCurrentQuoteAsync();
            ValuteValue valuteValue = null;

            switch (currencyCode)
            {
                case "USD":
                    {
                        valuteValue = quote.Valute.USD;
                        break;
                    }
                case "EUR":
                    {
                        valuteValue = quote.Valute.EUR;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return valuteValue != null ? amount / valuteValue.Value : amount;
        }
    }
}
