using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Operations
{
    public class OperationService : IOperationService
    {
        private readonly IEnumerable<OperationType> _operations;

        public OperationService()
        {
            _operations = new List<OperationType>()
            {
                new OperationType()
                {
                    TransactionType = TransactionType.Deposit,
                    Description = "Зачисление"
                },
                new OperationType()
                {
                    TransactionType = TransactionType.Withdrawal,
                    Description = "Снятие"
                }
            };
        }

        public async Task<OperationType> GetOperation(TransactionType transactionType, CancellationToken cancellationToken = default) =>
            _operations.FirstOrDefault(element => element.TransactionType == transactionType);

        public async Task<OperationType> GetOperationByDescriptionAsync(string description, CancellationToken cancellationToken = default) =>
            _operations.FirstOrDefault(element => element.Description == description);

        public async Task<IEnumerable<OperationType>> GetOperationsAsync(CancellationToken cancellationToken = default) =>
            _operations;
    }
}
