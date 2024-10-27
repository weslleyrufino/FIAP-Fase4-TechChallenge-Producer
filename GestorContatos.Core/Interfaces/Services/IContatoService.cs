using GestorContatos.Core.Entities;

namespace GestorContatos.Core.Interfaces.Services;
public interface IContatoService
{
    IEnumerable<ContatoModel> GetContatos();
    ContatoModel? GetContatosPorDDD(int ddd);
    ContatoModel PostInserirContato(ContatoModel contato);
    void PutAlterarContato(int id, ContatoModel contato);
    void DeleteContato(int id);
}
