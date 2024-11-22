using ConexaoCaninaApp.Domain.Models;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, Guid>
    {
        Usuario GetByEmail(string email);
    }
}
