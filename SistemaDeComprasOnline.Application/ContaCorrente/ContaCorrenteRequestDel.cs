using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Application.ContaCorrente
{
    public class ContaCorrenteRequestDel
    {

        public string CVV { get; set; }

        public int ContaCorrente { get; set; }

        public string Forma_Pagamento { get; set; }

        public DateTime DataValidade { get; set; }

        public string NumeroCartao { get; set; }
    }
}
