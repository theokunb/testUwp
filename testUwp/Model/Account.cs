using SQLite;

namespace testUwp.Model
{
    public class Account
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public float Amount { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
