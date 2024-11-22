using ConexaoCaninaApp.Domain.Models;

namespace ConexaoCaninaApp.Application.Dto
{
    public class FavoritoDTO
    {
        public Guid FavoritoId { get; set; }
        public CaoDto Cao { get; set; }
        public DateTime Data { get; set; }
    }
}
