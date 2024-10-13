using Model.Dtos.Account_;

namespace Model.Models.Account_;

public class AccountByLastTransfersModel
{
    public AccountResponseDto? Account { get; set; }
    public List<TransferBasicModel>? Transfers { get; set; } // if transfer status was false then not returning in the list
}

public class TransferBasicModel
{
    public Guid Id { get; set; }
    public bool IsUserSender { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
}
