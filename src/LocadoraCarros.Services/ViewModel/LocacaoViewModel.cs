using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraCarros.Services.ViewModel
{
    public class LocacaoViewModel
    {
        public string TipoCliente { get; set; }
        public int QuantidadePassageiros { get; set; }
        public DateTime MenorData { get; set; }
        public DateTime MaiorData { get; set; }
        public string TipoCarro { get; set; }
        public string NomeEmpresa { get; set; }
        public decimal Valor { get; set; }
    }
}
