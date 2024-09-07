using System.Threading;
using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Account
{
    public interface IAccountService : IService
    {
        Task ApplyOperationAsync(int accountId, OperationType operationType, double amount, string currecyCode, CancellationToken cancellationToken = default);
    }
}
