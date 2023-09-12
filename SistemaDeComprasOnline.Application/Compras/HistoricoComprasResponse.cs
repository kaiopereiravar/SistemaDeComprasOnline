using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Application.Compras
{
    public class HistoricoComprasResponse
    {
        public int id { get; set; }

        public DateTime dataTransacao { get; set; }

        public decimal valorTransacao { get; set; }

        public string FormaPagamento { get; set; }

        public int ContaCorrente { get; set; }
    }
}
