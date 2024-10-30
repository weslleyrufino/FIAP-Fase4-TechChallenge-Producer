using GestorContatos.Core.Entities.Base;

namespace GestorContatos.Core.Entities;
public class Contato : EntityBase
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public Regiao Regiao { get; set; }
}
