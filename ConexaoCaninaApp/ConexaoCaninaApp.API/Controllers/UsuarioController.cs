using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Requests;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        //GET USUARIO LOGADO, COM FAVORITOS, TODOS OS CACHORROS, E SUGESTOES
        [HttpGet]
        [Route("loggedUser/{email}")]
        public async Task<IActionResult> GetByLoggedUser(string email)
        {
            if(email == null)
                return BadRequest();

            var result = _usuarioService.GetByLoggedUser(email);
            if (result == null)
                return NoContent();

            return Ok(new UserViewModel
            {
                UsuarioId = result.UsuarioId,
                Email = result.Email,
                Nome = result.Nome,
                Telefone = result.Telefone,
                IsAdmin = result.IsAdmin,
                Favoritos = result.Favoritos.Select(x => new FavoritoViewModel
                {
                    FavoritoId = x.FavoritoId,
                    Cao = new CaoDetalhesViewModel
                    {
                        Age = x.Cao.Idade,
                        Breed = x.Cao.Raca,
                        City = x.Cao.Cidade,
                        Description = x.Cao.Descricao,
                        DogId = x.Cao.CaoId,
                        Gender = x.Cao.Genero.ToString(),
                        Name = x.Cao.Nome,
                        Size = x.Cao.Tamanho.ToString(),
                        State = x.Cao.Estado,
                        UniqueCharacteristics = x.Cao.CaracteristicasUnicas,
                    },
                    Data = x.Data,
                }).ToList(),
                
                Sugestoes = result.Sugestoes.Select(x => new SugestaoViewModel
                {
                    DataEnvio = x.DataEnvio,
                    Descricao = x.Descricao,
                    Status = x.Status,
                    Feedback = x.Feedback,
                    SugestaoId = x.SugestaoId
                }).ToList(),
                Caes = result.Caes.Select(x => new CaoDetalhesViewModel
                {
                    Age = x.Idade,
                    Breed = x.Raca,
                    City = x.Cidade,
                    Description = x.Descricao,
                    DogId = x.CaoId,
                    Gender = x.Genero.ToString(),
                    HealthHistories = x.HistoricosDeSaude.Select(y => new HealthHistoryViewModel
                    {
                        ExamDate = y.DataExame,
                        Exam = y.Exame,
                        Vaccine = y.Vacina,
                        OwnerConsent = y.ConsentimentoDono,
                        ConditionsOfHealth = y.CondicoesDeSaude,
                        HealthHistoryId = y.HistoricoSaudeId
                    }).ToList(),
                    Name = x.Nome,
                    Size = x.Tamanho.ToString(),
                    State = x.Estado,
                    UniqueCharacteristics = x.CaracteristicasUnicas,
                    Photos = x.Fotos.Select(y => new PhotoViewModel
                    {
                        CaminhoArquivo = y.CaminhoArquivo,
                        Descricao = y.Descricao
                    }).ToList(),
                }).ToList()
            });
        }

        // CRIAR USUARIO JUNTO COM O FIREBASE
        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest request)
        {
            if (request == null)
                return BadRequest();

            var result = _usuarioService.Create(new CriarUsuarioDTO
            {
                Email = request.Email,
                Nome = request.Nome,
                Telefone = request.Telefone,
                Senha = request.Senha
            });

            if (!result) return BadRequest();

            return Ok();
        }
        // EXCLUIR USUARIO
        // PATCH - EDITAR SENHA


        // ADICIONAR SUGESTOES

        // ADICIONAR CAO
        [HttpPost]
        [Route("{userId}/adicionarCao")]
        public async Task<IActionResult> AdicionarCao(Guid userId, [FromBody] AdicionarCaoRequest request)
        {
            if (request == null)
                return BadRequest();

            var result = _usuarioService.AddCao(userId, new AdicionarCaoDTO
            {
                CaracteristicasUnicas = request.CaracteristicasUnicas,
                Cidade = request.Cidade,
                Descricao = request.Descricao,
                Estado = request.Estado,
                Fotos = request.Fotos.Select(x => new FotoDTO
                {
                    CaminhoArquivo = x.CaminhoArquivo,
                    Descricao = x.Descricao
                }).ToList(),
                Genero = request.Genero,
                Idade = request.Idade,
                Nome = request.Nome,
                Raca = request.Raca,
                Tamanho = request.Tamanho
            });

            if (!result) return BadRequest();

            return Ok();
        }

        // REMOVER CAO
        [HttpDelete]
        [Route("{userId}/removerCao/{caoId}")]
        public async Task<IActionResult> RemoverCao(Guid userId, Guid caoId)
        {
            var result = _usuarioService.RemoveCao(userId, caoId);

            if (!result) return BadRequest();

            return Ok();
        }

        // ADICIONAR FAVORITOS
        [HttpPost]
        [Route("{userId}/adicionarFavoritos/{caoId}")]
        public async Task<IActionResult> AdicionarFavoritos(Guid userId, Guid caoId)
        {
            var result = _usuarioService.AddFavoritos(userId, caoId);

            if (!result) return BadRequest();

            return Ok();
        }
        // REMOVER FAVORITOS 

        // ADICIONAR FOTO
        // REMOVER FOTO

        // ADICIONAR HISTORICO DE SAUDE
        // REMOVER HISTORICO DE SAUDE

    }
}
