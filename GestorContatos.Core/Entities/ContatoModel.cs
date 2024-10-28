using GestorContatos.Core.Entities.Base;

namespace GestorContatos.Core.Entities;
public class ContatoModel : EntityBase
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public RegiaoModel Regiao { get; set; }

    public string TesteMigration { get; set; }
}
