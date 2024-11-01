using GestorContatos.API.ViewModel.Base;

namespace GestorContatos.API.ViewModel;

public class ContatoViewModel : ViewModelBase
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public RegiaoViewModel Regiao { get; set; }
}
