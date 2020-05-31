using ProjetoVendas.Data;
using ProjetoVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoVendas.Servicos
{
    public class DepartamentoServicos
    {


    private readonly VendasWebMvcContext _context;

    public DepartamentoServicos(VendasWebMvcContext context)
    {
        _context = context;
    }
        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x=>x.Nome).ToListAsync();
        }   
    }
}
