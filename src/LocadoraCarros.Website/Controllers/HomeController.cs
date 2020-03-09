using AutoMapper;
using LocadoraCarros.Services.Interfaces;
using LocadoraCarros.Services.Services;
using LocadoraCarros.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace LocadoraCarros.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly ILocacaoService locacaoService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            this.locacaoService = new LocacaoService(_mapper);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Locacoes(IFormCollection dadosFormulario)
        {
            try
            {
                if (dadosFormulario.Files.Count.Equals(0) | dadosFormulario.Files == null)
                {
                    ViewData["Erro"] = "Arquivo não selecionado!";
                    return View("Index");
                }

                var locacoes = locacaoService.VerificarCarroMaisBarato(dadosFormulario.Files.First());

                return View(locacoes);
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = string.Concat("Erro: ", ex.Message);
                return View("Index");
            }
        }
    }
}
