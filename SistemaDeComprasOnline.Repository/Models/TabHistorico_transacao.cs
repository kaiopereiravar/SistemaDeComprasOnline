using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Repository.Models
{
    public class TabHistorico_transacao
    {
        public int id { get; set; }

        public DateTime dataTransacao { get; set; }

        public decimal valorTransacao { get; set; }

        public string FormaPagamento { get; set; }

        public int ContaCorrente { get; set; }
    }
}
