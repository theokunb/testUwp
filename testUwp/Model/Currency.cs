using SQLite;

namespace testUwp.Model
{
    public class Currency
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public CurrencyType Type { get; set; }
        public string Title { get; set; }
        public string CurrencyFormat { get; set; }
    }

    public enum CurrencyType
    {
        RUB,
        USD,
        EUR
    }

    public static class CurrencyTypeExtensin
    {
        public static string GetFromat(this CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.RUB:
                    return "ru-RU";
                case CurrencyType.USD:
                    return "en-US";
                case CurrencyType.EUR:
                    return "fr-FR";
                default:
                    return string.Empty;
            }
        }
    }
}
