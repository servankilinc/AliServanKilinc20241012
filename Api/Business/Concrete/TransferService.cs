using AutoMapper;
using Business.Abstract;
using Core.DataAccess.Pagination;
using Core.Exceptions;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Account_;
using Model.Dtos.TransferType_;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Account_;
using Model.Models.Transfer_;

namespace Business.Concrete;
public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public TransferService(ITransferRepository transferRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        _transferRepository= transferRepository;
        _accountRepository= accountRepository;
        _mapper = mapper;
    }

    public async Task<Paginate<TransferDetailModel>> GetAccountHistoryAsync(AccountHistoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var paginatedList = await _transferRepository.GetPaginatedListAsync(
            filter: requestModel.Filter,
            sort: requestModel.Sort,
            include: t => t
                .Include(t => t.TransferType!)
                .Include(t => t.SenderAccount!)
                .Include(t => t.ReceivingAccount!)
                .Include(t => t.SenderUser!)
                .Include(t => t.ReceivingUser!),
            page: requestModel.PagingRequest!.Page,
            pageSize: requestModel.PagingRequest.PageSize,
            cancellationToken: cancellationToken);

        var mappedData = paginatedList.Data.Select(transfer => new TransferDetailModel
        {
            Id = transfer.Id,
            TransferTypeId = transfer.TransferTypeId,
            SenderAccountId = transfer.SenderAccountId,
            RecipientAccountId = transfer.RecipientAccountId,
            SenderUserId = transfer.SenderUserId,
            RecipientUserId = transfer.RecipientUserId,
            Date = transfer.Date,
            Status = transfer.Status,
            RejectionDetailDescription = transfer.RejectionDetailDescription,
            Amount = transfer.Amount,
            Description = transfer.Description,

            TransferType = _mapper.Map<TransferTypeResponseDto>(transfer.TransferType),
            SenderAccount = _mapper.Map<AccountResponseDto>(transfer.SenderAccount),
            ReceivingAccount = _mapper.Map<AccountResponseDto>(transfer.ReceivingAccount),
            SenderUser = _mapper.Map<UserResponseDto>(transfer.SenderUser),
            ReceivingUser = _mapper.Map<UserResponseDto>(transfer.ReceivingUser)
        }).ToList();
        
        return new Paginate<TransferDetailModel> { 
            Data = mappedData,
            DataCount = paginatedList.DataCount,
            Page = paginatedList.Page,
            PageSize = paginatedList.PageSize,
            PageCount = paginatedList.PageCount
        };
    }
     
    public async Task SendTransferRequestAsync(TransferRequestModel requestModel, CancellationToken cancellationToken)
    {
        Account recipientAccount = await _accountRepository.GetAsync(filter: a => a.AccountNo == requestModel.RecipientAccountNo, cancellationToken: cancellationToken);
        if (recipientAccount == null) throw new BusinessException("Alıcı Hesap Bulunamadı!");
        
        Account senderAccount = await _accountRepository.GetAsync(filter: a => a.Id == requestModel.SenderAccountId, cancellationToken: cancellationToken);
        if (senderAccount == null) throw new BusinessException("Gönderici Hesap Bulunamadı!");

        // include: a => a.Include(a => a.User!),
        // if (recipientAccount.User.FirstName == null) throw new BusinessException("Alıcı Hesap Bulunamadı!");  // To Do: Name Condition

        if (senderAccount.Balance < requestModel.Amount) throw new BusinessException("İşlem İçin Bakiye Yetersiz!");

        Transfer transferToInsert = new()
        {
            TransferTypeId = requestModel.TransferTypeId,
            SenderAccountId = requestModel.SenderAccountId,
            RecipientAccountId = recipientAccount.Id,
            SenderUserId = requestModel.SenderUserId,
            RecipientUserId = recipientAccount.UserId,
            Date = DateTime.Now,
            Status = true,
            Amount = requestModel.Amount,
            Description = requestModel.Description,
        };
        await _transferRepository.ApplyTransferAsync(transferToInsert, senderAccount, recipientAccount, cancellationToken); 
    }

    public async Task RejectTransferAsync(TransferRejectRequestModel rejectModel, CancellationToken cancellationToken)
    {
        Transfer transfer = await _transferRepository.GetAsync(filter: t => t.Id == rejectModel.TransferId, cancellationToken: cancellationToken);
        if (transfer == null) throw new BusinessException("İşlem Bulunamadı!");
        
        transfer.Status = false;
        transfer.RejectionDetailDescription = rejectModel.Description;

        await _transferRepository.RejectTransferAsync(transfer, cancellationToken);
    }
}
