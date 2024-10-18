using DataAccess.Abstract;
using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class TransferTypeRepository : EFRepositoryBase<TransferType, AppBaseDbContext>, ITransferTypeRepository
{
    public TransferTypeRepository(AppBaseDbContext context) : base(context)
    {
    }
}
