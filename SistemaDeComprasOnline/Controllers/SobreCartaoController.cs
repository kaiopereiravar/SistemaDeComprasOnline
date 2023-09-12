using Microsoft.AspNetCore.Mvc;
using SistemaDeComprasOnline.Application.Compras;
using SistemaDeComprasOnline.Application.SobreCartao;
using SistemaDeComprasOnline.Repository;

namespace SistemaDeComprasOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SobreCartaoController : ControllerBase
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public SobreCartaoController(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Rota responsavel por buscar os cartões existentes nas contas correntes
        /// </summary>
        /// <reponse code='200'>Retorna cartões apresentados com sucesso</reponse>
        /// <response code='400'>Retorna que o saldo não pode ser apresentado</response>
        [HttpGet]
        public IActionResult BuscarCartoesExistentes()
        {
            var buscarCartao = new CartaoService(_context);
            var todosCartoes = buscarCartao.BuscarCartoesExistentes();

            if(todosCartoes == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(todosCartoes);
            }

        }

        /// <summary>
        ///  Rota responsavel por deletar os cartões existentes nas contas correntes
        /// </summary>
        /// <reponse code='200'>Retorna cartões apresentados com sucesso</reponse>
        /// <response code='400'>Retorna que o saldo não pode ser apresentado</response>
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeletarCartao([FromRoute] int id)
        {
            var deletecartao = new CartaoService(_context);
            var sucesso = deletecartao.deletarCartao(id);
            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///  Rota responsavel por atualizar as informações dos cartões existentes nas contas correntes
        /// </summary>
        /// <reponse code='200'>Retorna cartões atualizados com sucesso</reponse>
        /// <response code='400'>Retorna que o cartão não pode ser atualizado</response>
        [HttpPut]
        public IActionResult AtualizarDadosCartao([FromBody]CartaoRequest request)
        {
            var atualizarCartao = new CartaoService(_context);
            var cartaoAtualizado = atualizarCartao.AtualizarDadosCartao(request);

            if(cartaoAtualizado == true)
            {
                return NoContent();
            }

            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///  Rota responsavel por inserir os cartões nas contas correntes
        /// </summary>
        /// <reponse code='200'>Retorna que os cartões foram inseridos com sucesso</reponse>
        /// <response code='400'>Retorna que o cartão não pode ser inserido</response>
        [HttpPost]
        public IActionResult InserirCartao([FromBody]CartaoRequest request)
        {
            var inserirCartao = new CartaoService(_context);
            var novoCartao = inserirCartao.InserirCartao(request);

            if (novoCartao == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
