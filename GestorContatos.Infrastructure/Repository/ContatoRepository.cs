using GestorContatos.Core.Entities;
using GestorContatos.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GestorContatos.Infrastructure.Repository;

public class ContatoRepository(ApplicationDbContext context) : EFRepository<Contato>(context), IContatoRepository
{
    public IEnumerable<Contato> GetContatosPorDDD(int ddd) 
        => _dbSet.Include(contato => contato.Regiao).Where(entity => entity.Regiao.DDD == ddd).ToList();

    public IEnumerable<Contato> GetTodosContatosMesclandoComDDD()
    {
        return _dbSet.Include(contato => contato.Regiao).ToList();
    }
}
