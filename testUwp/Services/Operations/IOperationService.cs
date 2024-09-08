using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Operations
{
    public interface IOperationService : IService
    {
        Task<IEnumerable<OperationType>> GetOperationsAsync(CancellationToken cancellationToken = default);
        Task<OperationType> GetOperationByDescriptionAsync(string description, CancellationToken cancellationToken = default);
        Task<OperationType> GetOperation(TransactionType transactionType, CancellationToken cancellationToken = default);
    }
}
