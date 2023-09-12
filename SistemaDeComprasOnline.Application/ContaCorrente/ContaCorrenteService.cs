using SistemaDeComprasOnline.Application.Compras;
using SistemaDeComprasOnline.Repository;
using SistemaDeComprasOnline.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDeComprasOnline.Application.ContaCorrente
{
    public class ContaCorrenteService
    {
        private readonly SistemaDeComprasOnlineContext _context;

        public ContaCorrenteService(SistemaDeComprasOnlineContext context)
        {
            _context = context;
        }

        public bool InserirContaCorrente(ContaCorrenteRequest request)
        {
            try
            {
                var NovaContaCorrente = new TabContaCorrente()
                {
                    Agencia = request.Agencia,
                    ContaCliente = request.ContaCliente,
                };

                _context.TabContaCorrente.Add(NovaContaCorrente);
                _context.SaveChanges();

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public decimal BuscarSaldoConta(ComprasRequest request)
        {
            try
            {
                if (_context.tabCartao.Any(s => s.CVV == request.CVV && s.NumeroCartao == request.NumeroCartao))
                {
                    var cartao = _context.tabCartao.FirstOrDefault(s => s.CVV == request.CVV && s.NumeroCartao == request.NumeroCartao);
                    var saldo = _context.TabSaldo.FirstOrDefault(x => x.ContaCorrente == cartao.ContaCorrente);

                    return saldo.SaldoConta;
                }

                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<TabContaCorrente> BuscarContasExistentes()
        {
            try
            {
                var contasExistentes = _context.TabContaCorrente.ToList();
                return contasExistentes;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool AtualizarSaldoConta(SaldoContaRequest request)
        {
            try
            {
                var tabConta = new TabSaldo()
                {
                    SaldoConta = request.SaldoConta,
                    ContaCorrente = request.ContaCorrente,
                };
                if (!_context.TabSaldo.Any(x => x.ContaCorrente == request.ContaCorrente))
                {
                    var Saldo = new TabSaldo()
                    {
                        SaldoConta = 0,
                        ContaCorrente = request.ContaCorrente,
                    };

                    _context.TabSaldo.Update(Saldo);
                }
                

                var Conta = _context.TabSaldo.FirstOrDefault(x => x.ContaCorrente == request.ContaCorrente);
                if (Conta == null)
                    return false;

                Conta.SaldoConta = request.SaldoConta;
                Conta.ContaCorrente = request.ContaCorrente;

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
