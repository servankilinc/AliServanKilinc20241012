using DataAccess.Abstract.RepositoryBase;
using Model.Entities;

namespace DataAccess.Abstract;

public interface IUserRepository : IRepository<User>, IRepositoryAsync<User>
{
}