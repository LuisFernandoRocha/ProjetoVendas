using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using ProjetoVendas.Models.ViewModels;
using ProjetoVendas.Servicos;
using ProjetoVendas.Servicos.Exceptions;

namespace ProjetoVendas.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorServices _vendedorServices;
        private readonly DepartamentoServicos _departamentoServicos;
        public VendedoresController(VendedorServices vendedorServices, DepartamentoServicos departamentoServicos)
        {
            _vendedorServices = vendedorServices;
            _departamentoServicos = departamentoServicos;
        }
        public async Task<IActionResult> Index()
        {                     
            var list = await _vendedorServices.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoServicos.FindAllAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        { 

                if (!ModelState.IsValid)
            {
                  var departmentos = await _departamentoServicos.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departmentos };
                return View(vendedor);
            }
        
            await _vendedorServices.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _vendedorServices.LocalizarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
                //return NotFound();
            }
 
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorServices.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _vendedorServices.LocalizarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Error), new { message = "Id not Provided" });
            }
            var obj = await _vendedorServices.LocalizarPorIdAsync(id.Value);
            if (obj == null)
            {
                RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Departamento> departamentos = await _departamentoServicos.FindAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departmentos = await _departamentoServicos.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departmentos };
                return View(vendedor);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _vendedorServices.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
        
            }

            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch(DbConcurrencyException e)
            {
                return  RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
