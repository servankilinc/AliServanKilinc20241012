using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Model.Entities;

namespace DataAccess.Concrete;

public class UserRepository : EFRepositoryBase<User, AppBaseDbContext>
{
    public UserRepository(AppBaseDbContext context) : base(context)
    {
    }
}
