using Finances.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Finances.Core.Requests.Transactions; 

public class UpdateTransactionRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Titulo Invalido")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Tipo Invalido")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdwaw;
    [Required(ErrorMessage = "Valor Invalido")]
    public decimal Amount { get; set; }
    [Required(ErrorMessage = "Categoria Invalida")]
    public long CategoryId { get; set; }
    [Required(ErrorMessage = "Data Invalida")]
    public DateTime? PaidOrReceived { get; set; }
}
