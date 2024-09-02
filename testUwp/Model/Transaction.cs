using SQLite;
using System;

namespace testUwp.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
