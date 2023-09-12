using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Repository.Models
{
    public class TabSaldo
    {
        public int Id { get; set; }

        public decimal SaldoConta { get; set; }

        public int ContaCorrente { get; set; }
    }
}
