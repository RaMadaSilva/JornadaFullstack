using Finances.Core.Enums;

namespace Finances.Core.Models; 

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdwaw; 
    public decimal Amount { get; set; }
    public long CategoreyId { get; set; }
    public Category Category { get; set; } = null!; 
    public string UserId { get; set; } = string.Empty; 
}
