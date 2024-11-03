using GestorContatos.Application.ViewModel.Base;

namespace GestorContatos.Application.ViewModel;

public class ContatoViewModel : ViewModelBase
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public RegiaoViewModel Regiao { get; set; }
}
