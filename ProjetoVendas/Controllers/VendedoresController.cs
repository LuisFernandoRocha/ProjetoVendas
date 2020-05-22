using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using ProjetoVendas.Servicos;
namespace ProjetoVendas.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorServices _vendedorServices;

        public VendedoresController(VendedorServices vendedorServices)
        {
            _vendedorServices = vendedorServices;
        }
        public IActionResult Index()
        {
            var list = _vendedorServices.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedorServices.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}