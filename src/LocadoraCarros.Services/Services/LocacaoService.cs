using LocadoraCarros.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static LocadoraCarros.Domain.Enums.EmpresasEnum;
using static LocadoraCarros.Domain.Enums.TiposLocacoesEnum;

namespace LocadoraCarros.Services.Services
{
    public class LocacaoService
    {
        public LocacaoService()
        {
        }

        public static IEnumerable<Locacao> VerificarCarroMaisBarato(IFormFile arquivo)
        {
            var locacoes = ObterInformacoesLocacaoDoArquivo(new StreamReader(arquivo.OpenReadStream()));
            foreach (var item in locacoes)
                item.Locadora = BuscarEmpresaComMenorValor(item.TipoLocacao, item.Datas, item.NumeroPassageiros);
            return locacoes;
        }

        private static IEnumerable<Locacao> ObterInformacoesLocacaoDoArquivo(StreamReader lerArquivo)
        {
            var locacoes = new List<Locacao>();
            while (!lerArquivo.EndOfStream)
            {
                var linha = lerArquivo.ReadLine().Split(':');
                locacoes.Add(new Locacao
                {
                    Datas = PegarListaDatas(linha[2].Split(',')),
                    NumeroPassageiros = int.Parse(linha[1]),
                    TipoLocacao = Enum.Parse<TipoLocacao>(linha[0])
                });
            }

            return locacoes;
        }

        private static IEnumerable<DateTime> PegarListaDatas(string[] datas)
        {
            List<DateTime> listaDatas = new List<DateTime>();

            foreach (string item in datas)
                listaDatas.Add(DateTime.ParseExact(item.Trim(), "ddMMMyyyy(ddd)", CultureInfo.InvariantCulture));

            return listaDatas;
        }

        public static Empresa BuscarEmpresaComMenorValor(TipoLocacao tipo ,IEnumerable<DateTime> datas, int numeroPassageiros)
        {
            List<Empresa> empresas = new List<Empresa>();
            foreach (var item in (Loja[])Enum.GetValues(typeof(Loja)))
                empresas.Add(new Empresa(item, tipo, datas));

            empresas = empresas.Where(e => e.NumeroMaximoPassageiros <= numeroPassageiros).ToList();

            var menorValor = empresas.Select(e => e.ValorLocacao).Min();

            return empresas.Where(e => e.ValorLocacao == menorValor).FirstOrDefault();
        }

    }
}
