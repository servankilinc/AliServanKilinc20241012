using DataAccess.Abstract;
using DataAccess.Concrete.RepositoryBase;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Models.Transfer_;

namespace DataAccess.Concrete;

public class TransferRepository : EFRepositoryBase<Transfer, AppBaseDbContext>, ITransferRepository
{
    public TransferRepository(AppBaseDbContext context) : base(context)
    {
    }
     
    public async Task ApplyTransferAsync(Transfer transfer, Account senderAccount, Account recipientAccount, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            senderAccount.Balance -= transfer.Amount;
            recipientAccount.Balance += transfer.Amount;

            _context.Entry(transfer).State = EntityState.Added;
            _context.Entry(senderAccount).State = EntityState.Modified;
            _context.Entry(recipientAccount).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task RejectTransferAsync(Transfer transfer, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            Account senderAccount = await _context.Set<Account>().FirstAsync(a => a.Id == transfer.SenderAccountId, cancellationToken);
            Account recipientAccount = await _context.Set<Account>().FirstAsync(a => a.Id == transfer.RecipientAccountId, cancellationToken);
            senderAccount.Balance += transfer.Amount;
            recipientAccount.Balance -= transfer.Amount;

            _context.Entry(transfer).State = EntityState.Modified;
            _context.Entry(senderAccount).State = EntityState.Modified;
            _context.Entry(recipientAccount).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}
