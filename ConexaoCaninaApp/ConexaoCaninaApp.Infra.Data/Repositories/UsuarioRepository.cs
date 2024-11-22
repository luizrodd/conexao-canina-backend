using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Context;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConexaoCaninaApp.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario, Guid>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Usuario GetByEmail(string email)
        {
            return _entity
                .Include(x => x.Caes).ThenInclude(x => x.Fotos)
                .Include(x => x.Caes).ThenInclude(x => x.HistoricosDeSaude)
                .Include(x => x.Favoritos).ThenInclude(x => x.Cao)
                .Include(x => x.Sugestoes)
                .FirstOrDefault(x => x.Email == email);
        }
    }
}
