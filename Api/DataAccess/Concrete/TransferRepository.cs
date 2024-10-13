using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class TransferRepository : EFRepositoryBase<Transfer, AppBaseDbContext>
{
    public TransferRepository(AppBaseDbContext context) : base(context)
    {
    }
}
