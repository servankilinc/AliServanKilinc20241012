﻿using AutoMapper;
using Business.Abstract;
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
    private readonly ITransferTypeRepository _transferTypeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public TransferService(ITransferRepository transferRepository, ITransferTypeRepository transferTypeRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        _transferRepository= transferRepository;
        _transferTypeRepository= transferTypeRepository;
        _accountRepository= accountRepository;
        _mapper = mapper;
    }

    public async Task<List<TransferDetailModel>> GetAccountHistoryAsync(AccountHistoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var list = await _transferRepository.GetAllAsync(
            filter: t => 
                (requestModel.StartDate != null ? DateOnly.FromDateTime(t.Date) >= requestModel.StartDate : true) &&
                (requestModel.EndDate != null ? DateOnly.FromDateTime(t.Date) <= requestModel.EndDate : true) && 
                (t.Status == requestModel.ProcessStatus),
            orderBy: t => 
                requestModel.SortBy == 1 ? t.OrderByDescending(t => t.Date) : 
                requestModel.SortBy == 2 ? t.OrderBy(t => t.Date) : 
                requestModel.SortBy == 3 ? t.OrderByDescending(t => t.Amount) : 
                requestModel.SortBy == 4 ? t.OrderBy(t => t.Amount) :
                t.OrderByDescending(t => t.Date),
            include: t => t
                .Include(t => t.TransferType!)
                .Include(t => t.SenderAccount!)
                .Include(t => t.ReceivingAccount!)
                .Include(t => t.SenderUser!)
                .Include(t => t.ReceivingUser!),
            cancellationToken: cancellationToken);

        return list.Select(transfer => new TransferDetailModel
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
    }

    public async Task<List<TransferTypeResponseDto>> GetTransferTypeListAsync(CancellationToken cancellationToken)
    {
        var list = await _transferTypeRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(_mapper.Map<TransferTypeResponseDto>).ToList();
    }


    public async Task SendTransferRequestAsync(TransferRequestModel requestModel, CancellationToken cancellationToken)
    {
        Account recipientAccount = await _accountRepository.GetAsync(filter: a => a.AccountNo == requestModel.RecipientAccountNo, cancellationToken: cancellationToken);
        if (recipientAccount == null) throw new Exception("Alıcı Hesap Bulunamadı!");
        
        Account senderAccount = await _accountRepository.GetAsync(filter: a => a.Id == requestModel.SenderAccountId, cancellationToken: cancellationToken);
        if (senderAccount == null) throw new Exception("Gönderici Hesap Bulunamadı!");

        // include: a => a.Include(a => a.User!),
        // if (recipientAccount.User.FirstName == null) throw new Exception("Alıcı Hesap Bulunamadı!");  // To Do: Name Condition

        if (senderAccount.Balance < requestModel.Amount) throw new Exception("İşlem İçin Bakiye Yetersiz!");

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

    public async Task RejectTransferAsync(TransferRejectModel rejectModel, CancellationToken cancellationToken)
    {
        Transfer transfer = await _transferRepository.GetAsync(filter: t => t.Id == rejectModel.TransferId, cancellationToken: cancellationToken);
        if (transfer == null) throw new Exception("İşlem Bulunamadı!");
        
        transfer.Status = false;
        transfer.RejectionDetailDescription = rejectModel.Description;

        await _transferRepository.RejectTransferAsync(transfer, cancellationToken);
    }
}
