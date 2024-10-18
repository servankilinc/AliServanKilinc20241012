using AutoMapper;
using Business.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Model.Dtos.TransferType_;
using Model.Entities;

namespace Business.Concrete;

public class TransferTypeService : ITransferTypeService
{
    private readonly ITransferTypeRepository _transferTypeRepository;
    private readonly IMapper _mapper;
    public TransferTypeService(ITransferTypeRepository transferTypeRepository, IMapper mapper)
    {
        _transferTypeRepository = transferTypeRepository;
        _mapper = mapper;
    }


    public async Task<List<TransferTypeResponseDto>> GetTransferTypeListAsync(CancellationToken cancellationToken)
    {
        var list = await _transferTypeRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(_mapper.Map<TransferTypeResponseDto>).ToList();
    }

    public async Task<TransferTypeResponseDto> CreateTransferTypeAsync(TransferTypeCreateDto transferTypeCreateDto, CancellationToken cancellationToken = default)
    {
        TransferType dataToInsert = _mapper.Map<TransferType>(transferTypeCreateDto);
        TransferType insertedData = await _transferTypeRepository.AddAsync(dataToInsert, cancellationToken: cancellationToken);
        return _mapper.Map<TransferTypeResponseDto>(insertedData);
    }

    public async Task<TransferTypeResponseDto> UpdateTransferTypeAsync(TransferTypeUpdateDto transferTypeUpdateDto, CancellationToken cancellationToken = default)
    {
        TransferType existData = await _transferTypeRepository.GetAsync(filter: f => f.Id == transferTypeUpdateDto.Id, cancellationToken: cancellationToken);
        if (existData == null) throw new BusinessException("Günceleme işlemi gerçekleştirilemedi.");
        TransferType dataToUpdate = _mapper.Map(transferTypeUpdateDto, existData);
        TransferType updatedData = await _transferTypeRepository.UpdateAsync(dataToUpdate, cancellationToken: cancellationToken);
        return _mapper.Map<TransferTypeResponseDto>(updatedData);
    }
}
