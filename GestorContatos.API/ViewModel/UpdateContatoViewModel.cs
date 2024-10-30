namespace GestorContatos.API.ViewModel;

public class UpdateContatoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
}
