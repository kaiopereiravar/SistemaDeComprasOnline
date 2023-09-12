using Microsoft.AspNetCore.Mvc;
using SistemaDeComprasOnline.Application.Compras;
using SistemaDeComprasOnline.Application.ContaCorrente;
using SistemaDeComprasOnline.Repository;

namespace SistemaDeComprasOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public ContaCorrenteController(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }


        /// <summary>
        ///  Rota responsavel por Inserir uma conta corrente
        /// </summary>
        /// <reponse code='200'>Retorna que a conta foi inserida com sucesso</reponse>
        /// <response code='400'>Retorna que a conta nao pode ser inserida</response>
        [HttpPost]
        public IActionResult InserirContaCorrente([FromBody] ContaCorrenteRequest request)
        {
            var novaContaCorrente = new ContaCorrenteService(_context);
            var contaCorrente = novaContaCorrente.InserirContaCorrente(request);

            if (contaCorrente == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///  Rota responsavel por buscar o saldo da conta corrente
        /// </summary>
        /// <reponse code='200'>Retorna saldo apresentado com sucesso</reponse>
        /// <response code='400'>Retorna que o saldo não pode ser acessado</response>
        [HttpGet("cartao")]
        public IActionResult BuscarSaldoConta(ComprasRequest request)
        {
            var BuscarSaldo = new ContaCorrenteService(_context);
            var saldo = BuscarSaldo.BuscarSaldoConta(request);
            if (saldo == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok(saldo);
            }
        }

        /// <summary>
        ///  Rota responsavel por buscar as contas correntes existentes
        /// </summary>
        /// <reponse code='200'>Retorna contas apresentadas com sucesso</reponse>
        /// <response code='400'>Retorna que as contas não podem ser apresentadas</response>
        [HttpGet("buscarConta")]
        public IActionResult BuscarContasExistentes()
        {
            var BuscarContas = new ContaCorrenteService(_context);
            var ContasExistentes = BuscarContas.BuscarContasExistentes();

            if(ContasExistentes == null)
            {
                return null;
            }
            else
            {
                return Ok(ContasExistentes);
            }
        }

        /// <summary>
        ///  Rota responsavel por atualizar o saldo das contas correntes
        /// </summary>
        /// <reponse code='200'>Retorna saldo atualizado com sucesso</reponse>
        /// <response code='400'>Retorna que o saldo não pode ser atualizado</response>
        [HttpPut]
        public IActionResult AtualizarSaldoConta([FromBody] SaldoContaRequest request)
        {
            var AtualizarSaldoConta = new ContaCorrenteService(_context);
            var saldoContaAtualizado = AtualizarSaldoConta.AtualizarSaldoConta(request);

            if (saldoContaAtualizado == false)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
