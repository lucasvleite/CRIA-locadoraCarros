using AutoMapper;
using LocadoraCarros.Domain;
using LocadoraCarros.Services.Interfaces;
using LocadoraCarros.Services.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static LocadoraCarros.Domain.Enums.EmpresasEnum;
using static LocadoraCarros.Domain.Enums.TiposLocacoesEnum;

namespace LocadoraCarros.Services.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly IMapper _mapper;

        public LocacaoService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<LocacaoViewModel> VerificarCarroMaisBarato(IFormFile arquivo)
        {
            var retorno = new List<LocacaoViewModel>();

            var locacoes = ObterInformacoesLocacaoDoArquivo(new StreamReader(arquivo.OpenReadStream()));
            foreach (var item in locacoes)
            {
                item.Locadora = BuscarLocadoraComMenorValor(item.TipoLocacao, item.Datas, item.NumeroPassageiros);
                retorno.Add(_mapper.Map<LocacaoViewModel>(item));
            }
            
            return retorno;
        }

        private static IEnumerable<Locacao> ObterInformacoesLocacaoDoArquivo(StreamReader lerArquivo)
        {
            var locacoes = new List<Locacao>();
            while (!lerArquivo.EndOfStream)
            {
                string[] linha = lerArquivo.ReadLine().Split(':');
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
                listaDatas.Add(DateTime.Parse(item.Trim().Remove(9)));

            return listaDatas;
        }

        private static Empresa BuscarLocadoraComMenorValor(TipoLocacao tipo ,IEnumerable<DateTime> datas, int numeroPassageiros)
        {
            List<Empresa> empresas = new List<Empresa>();
            foreach (var item in (Loja[])Enum.GetValues(typeof(Loja)))
                empresas.Add(new Empresa(item, tipo, datas));

            var menorValor = empresas
                .Where(e => e.NumeroMaximoPassageiros >= numeroPassageiros)
                .Select(e => e.ValorLocacao).Min();

            return empresas.Where(e => e.ValorLocacao == menorValor).FirstOrDefault();
        }
    }
}
