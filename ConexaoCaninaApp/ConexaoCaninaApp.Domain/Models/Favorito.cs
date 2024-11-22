namespace ConexaoCaninaApp.Domain.Models
{
    public class Favorito
	{
        private Favorito()
        {

        }
        public Favorito(Guid caoId)
        {
            FavoritoId = Guid.NewGuid();
            _caoId = caoId;
            Data = DateTime.UtcNow;
        }

        public Guid FavoritoId { get; set; }
        public Cao Cao { get; set; }
        private Guid _caoId;
        public DateTime Data { get; set; }
	}
}
