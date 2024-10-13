using DataAccess.Abstract.RepositoryBase;
using Model.Entities;

namespace DataAccess.Abstract;

public interface IAccountTypeRepository : IRepository<AccountType>, IRepositoryAsync<AccountType>
{
}
