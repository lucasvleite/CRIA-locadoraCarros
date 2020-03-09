using LocadoraCarros.Services.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LocadoraCarros.Services.Interfaces
{
    public interface ILocacaoService
    {
        IEnumerable<LocacaoViewModel> VerificarCarroMaisBarato(IFormFile arquivo);
    }
}
