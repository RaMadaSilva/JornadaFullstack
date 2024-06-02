namespace Finances.Core.Requests.Transactions; 

public class DeleteTransactionRequest : Request
{
    public long Id { get; set; }
}
