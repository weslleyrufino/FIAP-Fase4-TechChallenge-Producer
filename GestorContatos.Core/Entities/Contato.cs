using GestorContatos.Core.Entities.Base;

namespace GestorContatos.Core.Entities;
public class Contato : EntityBase
{
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public required int RegiaoId { get; set; }
    public Regiao Regiao { get; set; }
}
