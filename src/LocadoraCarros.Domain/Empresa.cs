using System;
using System.Collections.Generic;
using System.Linq;
using static LocadoraCarros.Domain.Enums.EmpresasEnum;
using static LocadoraCarros.Domain.Enums.TiposCarroEnum;
using static LocadoraCarros.Domain.Enums.TiposLocacoesEnum;

namespace LocadoraCarros.Domain
{
    public class Empresa
    {
        public Loja Locadora{ get; set; }
        public TiposCarro TipoCarro { get; set; }
        public int ValorSemana { get; set; }
        public int ValorFimSemana { get; set; }
        public int NumeroMaximoPassageiros { get; set; }
        public decimal ValorLocacao { get; set; }

        public Empresa(Loja loja, TipoLocacao tipo, IEnumerable<DateTime> datas)
        {
            Locadora = loja;

            if (Loja.SouthCar.Equals(loja))
                ObterSouthCar(tipo);
            else if (Loja.WestCar.Equals(loja))
                ObterWestCar(tipo);
            else if (Loja.NorthCar.Equals(loja))
                ObterNorthCar(tipo);

            ValorLocacao = CalcularValor(datas);
        }

        private decimal CalcularValor(IEnumerable<DateTime> datas)
        {
            int diasFimSemana = datas.Select(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday).Count();
            int diasSemana = datas.Select(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday).Count();

            return diasSemana * ValorSemana + diasFimSemana * ValorFimSemana;
        }

        private void ObterSouthCar(TipoLocacao tipo)
        {
            TipoCarro = TiposCarro.Compacto;
            NumeroMaximoPassageiros = 4;

            if (TipoLocacao.Premium.Equals(tipo))
            {
                this.ValorSemana = 150;
                this.ValorFimSemana = 90;
            }
            else
            {
                this.ValorSemana = 210;
                this.ValorFimSemana = 200;
            }
        }

        private void ObterWestCar(TipoLocacao tipo)
        {
            TipoCarro = TiposCarro.Esportivo;
            NumeroMaximoPassageiros = 2;

            if (TipoLocacao.Premium.Equals(tipo))
            {
                this.ValorSemana = 150;
                this.ValorFimSemana = 90;
            }
            else
            {
                this.ValorSemana = 530;
                this.ValorFimSemana = 200;
            }
        }

        private void ObterNorthCar(TipoLocacao tipo)
        {
            TipoCarro = TiposCarro.SUV;
            NumeroMaximoPassageiros = 7;

            if (TipoLocacao.Premium.Equals(tipo))
            {
                this.ValorSemana = 580;
                this.ValorFimSemana = 590;
            }
            else
            {
                this.ValorSemana = 630;
                this.ValorFimSemana = 600;
            }
        }
    }
}
