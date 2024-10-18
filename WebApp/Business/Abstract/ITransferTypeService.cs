using Model.Dtos.TransferType_;

namespace Business.Abstract;

public interface ITransferTypeService
{
    Task<List<TransferTypeResponseDto>> GetTransferTypeListAsync(CancellationToken cancellationToken);
    Task<TransferTypeResponseDto> CreateTransferTypeAsync(TransferTypeCreateDto transferTypeCreateDto, CancellationToken cancellationToken = default);
    Task<TransferTypeResponseDto> UpdateTransferTypeAsync(TransferTypeUpdateDto transferTypeUpdateDto, CancellationToken cancellationToken = default);
}
