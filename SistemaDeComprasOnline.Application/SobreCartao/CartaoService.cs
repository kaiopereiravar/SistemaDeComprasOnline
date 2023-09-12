using SistemaDeComprasOnline.Application.Compras;
using SistemaDeComprasOnline.Repository;
using SistemaDeComprasOnline.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SistemaDeComprasOnline.Application.SobreCartao
{
    public class CartaoService
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public CartaoService(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }

        public List<TabCartao> BuscarCartoesExistentes()
        {
            try
            {
                var cartoesExistentes = _context.tabCartao.ToList();
                return cartoesExistentes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AtualizarDadosCartao(CartaoRequest request)
        {
            try
            {
                var cartao = _context.tabCartao.FirstOrDefault(x => x.CVV == request.CVV && x.NumeroCartao == request.NumeroCartao);// && x.Id == x.Id
                if (cartao == null)
                {
                    return false;
                }

                cartao.NumeroCartao = request.NumeroCartao;
                cartao.Forma_Pagamento = request.Forma_Pagamento;
                cartao.DataValidade = request.DataValidade;
                cartao.CVV = request.CVV;
                cartao.ContaCorrente = request.ContaCorrente;

                _context.Update(cartao);
                _context.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                return false;
            } 
        }

        public bool InserirCartao(CartaoRequest request)
        {
            try
            {
                var NovoCartao = new TabCartao()
                {
                    NumeroCartao = request.NumeroCartao,
                    Forma_Pagamento = request.Forma_Pagamento,
                    DataValidade = request.DataValidade,
                    CVV = request.CVV,
                    ContaCorrente = request.ContaCorrente,
                };

                var camposExistente = _context.tabCartao.FirstOrDefault(x => x.NumeroCartao == request.NumeroCartao && x.CVV == request.CVV);
                if(camposExistente != null)
                {
                    return false;
                }

                var saldoConta = new TabSaldo()
                {
                    SaldoConta = 0,
                    ContaCorrente = request.ContaCorrente
                }; 

                _context.TabSaldo.Add(saldoConta);

                _context.Add(NovoCartao);
                _context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }  
        }

        public bool deletarCartao(int id)
        {
            try
            {
                var cartao = _context.tabCartao.FirstOrDefault(x => x.Id == id);
                if (cartao == null)
                    return false;

                _context.tabCartao.Remove(cartao);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
