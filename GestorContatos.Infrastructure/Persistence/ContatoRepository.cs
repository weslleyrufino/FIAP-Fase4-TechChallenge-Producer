using GestorContatos.Core.Entities;
using GestorContatos.Core.Interfaces.Repository;

namespace GestorContatos.Infrastructure.Persistence;
public class ContatoRepository : IContatoRepository
{
    public void DeleteContato(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ContatoModel> GetContatos()
    {
        throw new NotImplementedException();
    }

    public ContatoModel? GetContatosPorDDD(int ddd)
    {
        throw new NotImplementedException();
    }

    public ContatoModel PostInserirContato(ContatoModel contato)
    {
        throw new NotImplementedException();
    }

    public void PutAlterarContato(int id, ContatoModel contato)
    {
        throw new NotImplementedException();
    }
}
