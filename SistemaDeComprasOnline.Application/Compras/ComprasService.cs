
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaDeComprasOnline.Repository;
using SistemaDeComprasOnline.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SistemaDeComprasOnline.Application.Compras
{
    public class ComprasService
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public ComprasService(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }

        public ComprasResponse Comprar(ComprasRequest request)
        {
            var response = new ComprasResponse();

            try
            {
                if (_context.tabCartao.Any(s => s.CVV == request.CVV && s.NumeroCartao == request.NumeroCartao))
                {
                    var cartao = _context.tabCartao.FirstOrDefault(
                        s => s.CVV == request.CVV &&
                        s.NumeroCartao == request.NumeroCartao &&
                        s.Forma_Pagamento == request.Forma_Pagamento &&
                        s.DataValidade == request.DataValidade &&
                        s.ContaCorrente == request.ContaCorrente);

                    var saldo = _context.TabSaldo.FirstOrDefault(x => x.ContaCorrente == cartao.ContaCorrente);
                    if (saldo.SaldoConta < request.ValorCompra)
                    {
                        response.Sucesso = false;
                        response.Mensagem = "Saldo insuficiente.";
                        return response;
                    }

                    saldo.SaldoConta = saldo.SaldoConta - request.ValorCompra;
                    _context.TabSaldo.Update(saldo);

                    var historicoTransacao = new TabHistorico_transacao
                    {
                        ContaCorrente = cartao.ContaCorrente,
                        valorTransacao = request.ValorCompra,
                        dataTransacao = DateTime.Now,
                        FormaPagamento = request.Forma_Pagamento,
                    };

                    _context.TabHistorico_transacao.Add(historicoTransacao);

                    _context.SaveChanges();

                    response.Sucesso = true;
                    response.Mensagem = "Compra realizada com sucesso.";
                    return response;
                }

                response.Sucesso = false;
                response.Mensagem = "Cartão inválido.";
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = "Ocorreu um erro ao processar a compra: " + ex.Message;
                return response;
            }
        }

        public HistoricoComprasResponse BuscarHistoricoTransacao(ComprasRequest request)
        {
            try
            {
                var contaCorrente = _context.TabHistorico_transacao.FirstOrDefault(s => s.ContaCorrente == request.ContaCorrente);
                if (contaCorrente == null)
                    return null;

                var compras = new HistoricoComprasResponse()
                {
                    id = contaCorrente.id,
                    dataTransacao = contaCorrente.dataTransacao,
                    valorTransacao = contaCorrente.valorTransacao,
                    ContaCorrente = contaCorrente.ContaCorrente,
                    FormaPagamento = contaCorrente.FormaPagamento
                };

                return compras;
            }
            catch(Exception)
            {
                return null;
            }

            
        }
    }
}
