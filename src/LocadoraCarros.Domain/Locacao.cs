using System;
using System.Collections.Generic;
using static LocadoraCarros.Domain.Enums.Empresas;
using static LocadoraCarros.Domain.Enums.TiposLocacoes;

namespace LocadoraCarros.Domain
{
    public class Locacao
    {
        public IEnumerable<DateTime> Datas { get; set; }
        public TipoLocacao TipoLocacao { get; set; }
        public Loja Locadora { get; set; }
        public int NumeroPassageiros { get; set; }
        public decimal Valor { get; set; }
    }
}
