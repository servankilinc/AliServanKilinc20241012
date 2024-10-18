using DataAccess.Abstract;
using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class AccountTypeRepository : EFRepositoryBase<AccountType, AppBaseDbContext>, IAccountTypeRepository
{
    public AccountTypeRepository(AppBaseDbContext context) : base(context)
    {
    }
}
