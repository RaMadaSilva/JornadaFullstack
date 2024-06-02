using System.ComponentModel.DataAnnotations;

namespace Finances.Core.Requests.Categories; 
public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Titulo invalido")]
    [MaxLength(80, ErrorMessage = "O Titulo deve conter no maximo 80 Caracteres")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Descrição invalida")]
    public string Description { get; set; } = string.Empty;
}
