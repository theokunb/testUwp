using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Quotes
{
    public class QuotesService : IQuoteService
    {
        private readonly string url;

        public QuotesService()
        {
            url = "https://www.cbr-xml-daily.ru/daily_json.js";
        }

        public async Task<Quote> GetCurrentQuoteAsync(CancellationToken cancellationToken = default)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new System.Exception("error in quote request");
                    }

                    var quote = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Quote>(quote);
                } 
            }
        }
    }
}
