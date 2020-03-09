using AutoMapper;
using LocadoraCarros.Domain;
using LocadoraCarros.Services.ViewModel;
using System.Linq;

namespace LocadoraCarros.Services.Profiles
{
    public class LocacaoProfile : Profile
    {
        public LocacaoProfile()
        {
            CreateMap<Locacao, LocacaoViewModel>()
                .ForMember(dest => dest.MaiorData, opt => opt.MapFrom(src => src.Datas.Select(e => e.Date).Max()))
                .ForMember(dest => dest.MenorData, opt => opt.MapFrom(src => src.Datas.Select(e => e.Date).Min()))
                .ForMember(dest => dest.NomeEmpresa, opt => opt.MapFrom(src => src.Locadora.Locadora.ToString()))
                .ForMember(dest => dest.QuantidadePassageiros, opt => opt.MapFrom(src => src.NumeroPassageiros))
                .ForMember(dest => dest.TipoCarro, opt => opt.MapFrom(src => src.Locadora.TipoCarro.ToString()))
                .ForMember(dest => dest.TipoCliente, opt => opt.MapFrom(src => src.TipoLocacao.ToString()))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Locadora.ValorLocacao));
        }
    }
}
