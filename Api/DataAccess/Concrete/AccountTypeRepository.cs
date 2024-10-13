using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class AccountTypeRepository : EFRepositoryBase<AccountType, AppBaseDbContext>
{
    public AccountTypeRepository(AppBaseDbContext context) : base(context)
    {
    }
}
