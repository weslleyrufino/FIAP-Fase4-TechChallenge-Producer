using GestorContatos.Core.Entities;
using GestorContatos.Core.Interfaces.Repository;
using GestorContatos.Core.Interfaces.Services;

namespace GestorContatos.Core.Services;
public class ContatoService(IContatoRepository contatoRepository) : IContatoService
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public void DeleteContato(int id)
    {
        _contatoRepository.Deletar(id);
    }

    public IEnumerable<Contato> GetContatos() 
        => _contatoRepository.GetTodosContatosMesclandoComDDD();

    public IEnumerable<Contato> GetContatosPorDDD(int ddd) 
        => _contatoRepository.GetContatosPorDDD(ddd);

    public Contato ObterPorId(int id) 
        => _contatoRepository.ObterPorId(id);

    public void PostInserirContato(Contato contato)
    {
        _contatoRepository.Cadastrar(contato);
    }

    public void PutAlterarContato(Contato contato)
    {
        _contatoRepository.Alterar(contato);
    }

}
