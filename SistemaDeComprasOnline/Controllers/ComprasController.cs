using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeComprasOnline.Application.Compras;
using SistemaDeComprasOnline.Repository;
using SistemaDeComprasOnline.Repository.Models;
using System.Collections;
using System.Collections.Generic;

namespace SistemaDeComprasOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public ComprasController(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }


        /// <summary>
        ///  Rota responsavel por realizar a compra de algum produto
        /// </summary>
        /// <returns>Ele retira o valor de uma conta corrente de acordo com o valor da compra</returns>
        /// <reponse code='200'>Retorna compra realizada com sucesso</reponse>
        /// <response code='400'>Retorna compra nao realizada</response>

        [HttpPost]
        public IActionResult Comprar([FromBody] ComprasRequest request)
        {
            var comprar = new ComprasService(_context);
            var response = comprar.Comprar(request);

            if (response.Sucesso)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        ///  Rota responsavel por buscar o historico de compras realizadas
        /// </summary>
        /// <reponse code='200'>Retorna historico de compras</reponse>
        /// <response code='400'>Retorna que o processo nao pode ser realizado</response>

        [HttpGet]
        public IActionResult BuscarHistoricoTransacao([FromBody] ComprasRequest request)
        {
            var comprasService = new ComprasService(_context);
            var compras = comprasService.BuscarHistoricoTransacao(request);
            if (compras != null)
            {
                return Ok(compras);
            }
            else
            {
                return BadRequest("Compra não encontrada");
            }
        }

    }
}
