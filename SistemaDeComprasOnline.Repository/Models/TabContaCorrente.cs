using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Repository.Models
{
    public class TabContaCorrente
    {
        public int Id { get; set; }

        public string Agencia { get; set; }

        public int ContaCliente { get; set; }
    }
}
