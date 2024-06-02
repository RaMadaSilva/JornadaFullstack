using System.ComponentModel.DataAnnotations;

namespace Finances.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage ="Titulo invalido")]
    [MaxLength(80, ErrorMessage ="O Titulo deve conter no maximo 80 Caracteres")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage ="Descrição invalida")]
    public string Description { get; set; } = string.Empty; 
}
