using LocadoraCarros.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using static LocadoraCarros.Domain.Enums.TiposLocacoes;

namespace LocadoraCarros.Services.Services
{
    public class LocacaoService
    {
        public LocacaoService()
        {
        }

        public static async Task<IEnumerable<Locacao>> VerificarCarroMaisBarato(IFormFile arquivo)
        {
            var locacoes = ObterInformacoesLocacaoDoArquivo(new StreamReader(arquivo.OpenReadStream()));
            VerificarDisponibilidadeCarros(locacoes);
            return locacoes;
        }

        private static void VerificarDisponibilidadeCarros(IEnumerable<Locacao> locacoes)
        {
            throw new NotImplementedException();
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
    }
}
