using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class AccountRepository : EFRepositoryBase<Account, AppBaseDbContext>
{
    public AccountRepository(AppBaseDbContext context) : base(context)
    {
    }
}