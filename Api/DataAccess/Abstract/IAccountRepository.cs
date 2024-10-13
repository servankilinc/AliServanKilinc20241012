using DataAccess.Abstract.RepositoryBase;
using Model.Entities;

namespace DataAccess.Abstract;

public interface IAccountRepository: IRepository<Account>, IRepositoryAsync<Account> 
{
    Task<ICollection<Account>> GetUserAccountsWithLastTransfersAsync(Guid userId, CancellationToken cancellationToken);
}
