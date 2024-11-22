using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Requests;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaoController : ControllerBase
	{
		private readonly ICaoService _caoService;
		
		public CaoController(ICaoService caoService)
		{
			_caoService = caoService;
		}

		// GETALL
		[HttpGet]
        public async Task<IActionResult> GetAll()
		{
            var result = _caoService.GetAll();
            if (result == null)
                return NoContent();

            return Ok(result.Select(x => new CaoViewModel
			{
                DogId = x.CaoId,
                Breed = x.Raca,
                Age = x.Idade,
				City = x.Cidade,
                Description = x.Descricao,
                Gender = x.Genero.ToString(),
                Name = x.Nome,
                Photos = x.Fotos.Select(f => new PhotoViewModel
				{
					CaminhoArquivo = f.CaminhoArquivo,
                    Descricao = f.Descricao,
                }).ToList(),
				Size = x.Tamanho.ToString(),
                State = x.Estado, 
				UniqueCharacteristics = x.CaracteristicasUnicas,
            }));
        }

        // GETBYID - DETALHES com Historico de Saude
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _caoService.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(new CaoDetalhesViewModel
            {
                DogId = result.CaoId,
                Breed = result.Raca,
                Age = result.Idade,
                City = result.Cidade,
                Description = result.Descricao,
                Gender = result.Genero.ToString(),
                Name = result.Nome,
                Photos = result.Fotos.Select(f => new PhotoViewModel
                {
                    CaminhoArquivo = f.CaminhoArquivo,
                    Descricao = f.Descricao,
                }).ToList(),
                Size = result.Tamanho.ToString(),
                State = result.Estado,
                UniqueCharacteristics = result.CaracteristicasUnicas,
                HealthHistories = result.HistoricosDeSaude.Select(h => new HealthHistoryViewModel
                {
                    Exam = h.Exame,
                    Vaccine = h.Vacina,
                    ConditionsOfHealth = h.CondicoesDeSaude,
                    OwnerConsent = h.ConsentimentoDono,
                    ExamDate = h.DataExame,
                }).ToList(),
            });
        }
        // ALTERAR SOMENTE TAMANHO DO CAO
        // ALTERAR IDADE DO CAO
        [HttpPatch]
        [Route("{id}/idade")]
        public async Task<IActionResult> AlterarIdadeCao(Guid id, [FromBody] AlterarIdadeCaoRequest request)
        {
            if (request == null)
                return BadRequest();

            var result = _caoService.AlterarIdade(id, request.Idade);

            return Ok();
        }

        // APROVAR CAO STATUS = APROVADO
        [HttpPost]
        [Route("{id}/aprovar")]
        public async Task<IActionResult> AprovarCao(Guid id)
        {
            var result = _caoService.AprovarCao(id);

            return Ok();
        }
        // REJEITAR CAO STATUS = REJEITAR

    }
}
