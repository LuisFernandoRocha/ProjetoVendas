using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Servicos.Exceptions;

namespace ProjetoVendas.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendasServico _registroVendasServico;

        public RegistroVendasController(RegistroVendasServico registroVendasServico)
        {
            _registroVendasServico = registroVendasServico;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasServico.PesquisaDateAsync(minDate, maxDate);
            return View(result);
        }
        //public IActionResult BuscaAgrupada()
        //{
        //    return View();
        //}
        public async Task<IActionResult> BuscaAgrupada(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasServico.PesquisaDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}