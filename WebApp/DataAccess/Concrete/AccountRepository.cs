using DataAccess.Abstract;
using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DataAccess.Concrete;

public class AccountRepository : EFRepositoryBase<Account, AppBaseDbContext>, IAccountRepository
{
    public AccountRepository(AppBaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Account>> GetUserAccountsWithLastTransfersAsync(Guid userId, CancellationToken cancellationToken)
    {
        var temp = await _context.Set<Account>()
            .Where(a => a.UserId == userId)
            .Include(a => a.AccountType)
            .Select(a => new
                {
                    Account = a,
                    Transfers = (a.ShippingProcesses ?? Enumerable.Empty<Transfer>())
                        .Union(a.PurchaseProcesses ?? Enumerable.Empty<Transfer>())
                        .OrderByDescending(t => t.Date)
                        .Take(5)
                        .ToList()
                }
            ).ToListAsync(cancellationToken);

        return temp.Select(a => {
            a.Account.ShippingProcesses = a.Transfers;
            return a.Account;
        }).ToList();
    }
}