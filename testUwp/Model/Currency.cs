using SQLite;

namespace testUwp.Model
{
    public class Currency
    {
        [PrimaryKey, AutoIncrement] public int? Id { get; set; }
        public CurrencyType Type { get; set; }
        public string Title { get; set; }
    }

    public enum CurrencyType
    {
        RUB,
        USD,
        EUR
    }
}
