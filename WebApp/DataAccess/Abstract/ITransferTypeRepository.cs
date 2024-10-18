using DataAccess.Abstract.RepositoryBase;
using Model.Entities;

namespace DataAccess.Abstract;

public interface ITransferTypeRepository : IRepository<TransferType>, IRepositoryAsync<TransferType>
{
}
