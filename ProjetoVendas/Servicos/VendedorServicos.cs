using ProjetoVendas.Data;
using ProjetoVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Servicos
{
    public class VendedorServices
    {
        private readonly ProjetoVendasContext _context;

        public VendedorServices(ProjetoVendasContext context)
        {
            _context = context;      
        }

        public List<Vendedor> FindAll(){
            return _context.Vendedor.ToList();
        }
        public void Insert(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

    }
}
