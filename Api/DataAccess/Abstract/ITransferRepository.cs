using DataAccess.Abstract.RepositoryBase;
using Model.Entities;

namespace DataAccess.Abstract;

public interface ITransferRepository : IRepository<Transfer>, IRepositoryAsync<Transfer>
{
}
