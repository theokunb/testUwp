using System.Threading.Tasks;
using testUwp.Model;

namespace testUwp.Services.Transaction
{
    public interface ITransactionService : IService
    {
        Task<TransactionDto> ConvertToTransactionDto(Model.Transaction transaction);
    }
}
