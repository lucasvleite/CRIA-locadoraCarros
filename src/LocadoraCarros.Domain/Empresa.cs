using System;
using System.Collections.Generic;
using System.Linq;
using static LocadoraCarros.Domain.Enums.Empresas;
using static LocadoraCarros.Domain.Enums.TiposLocacoes;

namespace LocadoraCarros.Domain
{
    public class Empresa
    {
        public Loja Locadora{ get; set; }
        public int ValorSemana { get; set; }
        public int ValorFimSemana { get; set; }
        public int NumeroMaximoPassageiros { get; set; }

        public Empresa(Loja loja, TipoLocacao tipo)
        {
            Locadora = loja;

            if (loja.Equals(0))
                ObterSouthCar(tipo);
            else if (loja.Equals(1))
                ObterWestCar(tipo);
            else if (loja.Equals(2))
                ObterNorthCar(tipo);
        }

        public decimal CalcularMenorValor(List<DateTime> datas, int numeroPassageiros)
        {
            int diasFimSemana = datas.Select(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday).Count();
            int diasSemana = datas.Select(d => d.DayOfWeek != DayOfWeek.Saturday || d.DayOfWeek != DayOfWeek.Sunday).Count();

            return 0;
        }

        private void ObterSouthCar(TipoLocacao tipo)
        {
            NumeroMaximoPassageiros = 4;

            if (tipo.Equals(1))
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
            NumeroMaximoPassageiros = 2;

            if (tipo.Equals(1))
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
            NumeroMaximoPassageiros = 7;

            if (tipo.Equals(1))
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
