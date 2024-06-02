namespace Finances.Core.Requests.Transactions;

public class GetTransactionByIdRequest : Request
{
    public long Id { get; set; }
}
