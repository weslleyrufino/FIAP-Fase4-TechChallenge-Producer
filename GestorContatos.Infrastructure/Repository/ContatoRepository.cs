using GestorContatos.Core.Entities;
using GestorContatos.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GestorContatos.Infrastructure.Repository;

public class ContatoRepository : EFRepository<Contato>, IContatoRepository
{
    public ContatoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<Contato> GetContatosPorDDD(int ddd) 
        => _dbSet.Include(contato => contato.Regiao).Where(entity => entity.Regiao.DDD == ddd).ToList();

    public IEnumerable<Contato> GetTodosContatosMesclandoComDDD()
    {
        return _dbSet.Include(contato => contato.Regiao).ToList();
    }
}
