using System.ComponentModel.DataAnnotations;

namespace GestorContatos.API.ViewModel;

public class UpdateContatoViewModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [RegularExpression(@"^\d{8,9}$", ErrorMessage = "Telefone deve conter entre 8 e 9 dígitos.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo RegiaoId é obrigatório.")]
    public int RegiaoId { get; set; }
}
