using GestorContatos.Core.Entities;
using GestorContatos.Core.Interfaces.Repository;
using GestorContatos.Core.Interfaces.Services;

namespace GestorContatos.Core.Services;
public class ContatoService : IContatoService
{
    private readonly IContatoRepository _contatoRepository;

    public ContatoService(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public void DeleteContato(int id)
    {
        _contatoRepository.DeleteContato(id);
    }

    public IEnumerable<ContatoModel> GetContatos()
    {
        return _contatoRepository.GetContatos();
    }

    public ContatoModel? GetContatosPorDDD(int ddd)
    {
        return _contatoRepository.GetContatosPorDDD(ddd);
    }

    public ContatoModel PostInserirContato(ContatoModel contato)
    {
        return _contatoRepository.PostInserirContato(contato);
    }

    public void PutAlterarContato(int id, ContatoModel contato)
    {
        _contatoRepository.PutAlterarContato(id, contato);
    }

}
