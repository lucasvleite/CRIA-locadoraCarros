using System;
using System.Collections.Generic;
using static LocadoraCarros.Domain.Enums.TiposLocacoesEnum;

namespace LocadoraCarros.Domain
{
    public class Locacao
    {
        public IEnumerable<DateTime> Datas { get; set; }
        public TipoLocacao TipoLocacao { get; set; }
        public Empresa Locadora { get; set; }
        public int NumeroPassageiros { get; set; }
        public decimal Valor { get; set; }
    }
}
