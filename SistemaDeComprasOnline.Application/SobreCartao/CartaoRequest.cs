using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Application.SobreCartao
{
    public class CartaoRequest
    {
        public string NumeroCartao { get; set; }

        public string Forma_Pagamento { get; set; }

        public DateTime DataValidade { get; set; }

        public string CVV { get; set; }

        public decimal ValorCompra { get; set; }

        public int ContaCorrente { get; set; }
    }
}
