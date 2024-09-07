using SQLite;
using System;

namespace testUwp.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public double Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
