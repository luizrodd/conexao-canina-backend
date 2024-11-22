using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;

namespace ConexaoCaninaApp.Application.Services
{
    public interface ICaoService
    {
        IEnumerable<CaoDto> GetAll();
        bool AlterarIdade(Guid id, int idade);
        CaoDto GetById(Guid id);
        bool AprovarCao(Guid id);
    }
    public class CaoService : ICaoService
    {
        private readonly ICaoRepository _caoRepository;
        public CaoService(ICaoRepository caoRepository)
        {
            _caoRepository = caoRepository;
        }

        public bool AlterarIdade(Guid id, int idade)
        {
            var cao = _caoRepository.GetById(id);
            if(cao == null)
                return false;

            cao.AlterarIdade(idade);
            _caoRepository.SaveChanges();

            return true;
        }

        public bool AprovarCao(Guid id)
        {
            var cao = _caoRepository.GetById(id);
            if (cao == null)
                return false;

            cao.Aprovar();
            _caoRepository.SaveChanges();
            return true;
        }

        public IEnumerable<CaoDto> GetAll()
        {
            var caos = _caoRepository.GetAll().Where(x => x.Status == StatusCao.Aprovado);
            if (caos == null)
                return null;

            return caos.Select(x => new CaoDto
            {
                CaoId = x.CaoId,
                CaracteristicasUnicas = x.CaracteristicasUnicas,
                Idade = x.Idade,
                Nome = x.Nome,
                Raca = x.Raca,
                Cidade = x.Cidade,
                Descricao = x.Descricao,
                Estado = x.Estado,
                Fotos = x.Fotos.Select(x => new FotoDTO
                {
                    FotoId = x.FotoId,
                    CaminhoArquivo = x.CaminhoArquivo,
                    Descricao = x.Descricao
                }).ToList(),
                Genero = x.Genero,
                Tamanho = x.Tamanho
            });
        }

        public CaoDto GetById(Guid id)
        {
            var cao = _caoRepository.GetById(id);
            if (cao == null)
                return null;

            return new CaoDto
            {
                CaoId = cao.CaoId,
                CaracteristicasUnicas = cao.CaracteristicasUnicas,
                Idade = cao.Idade,
                Nome = cao.Nome,
                Raca = cao.Raca,
                Cidade = cao.Cidade,
                Descricao = cao.Descricao,
                Estado = cao.Estado,
                Fotos = cao.Fotos.Select(x => new FotoDTO
                {
                    FotoId = x.FotoId,
                    CaminhoArquivo = x.CaminhoArquivo,
                    Descricao = x.Descricao
                }).ToList(),
                Genero = cao.Genero,
                Tamanho = cao.Tamanho,
                HistoricosDeSaude = cao.HistoricosDeSaude.Select(x => new HistoricoDeSaudeDTO
                {
                    CondicoesDeSaude = x.CondicoesDeSaude,
                    ConsentimentoDono = x.ConsentimentoDono,
                    DataExame = x.DataExame,
                    Exame = x.Exame,
                    HistoricoSaudeId = x.HistoricoSaudeId,
                    Vacina = x.Vacina
                })
            };
        }
    }
}
