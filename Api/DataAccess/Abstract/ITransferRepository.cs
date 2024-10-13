using DataAccess.Abstract.RepositoryBase;
using Model.Entities;
using Model.Models.Transfer_;

namespace DataAccess.Abstract;

public interface ITransferRepository : IRepository<Transfer>, IRepositoryAsync<Transfer>
{
    Task ApplyTransferAsync(Transfer transfer, Account senderAccount, Account recipientAccount, CancellationToken cancellationToken);
    Task RejectTransferAsync(Transfer transfer, CancellationToken cancellationToken);
}
