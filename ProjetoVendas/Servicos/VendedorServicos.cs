using ProjetoVendas.Data;
using ProjetoVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Servicos.Exceptions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoVendas.Servicos
{
    public class VendedorServices
    {
        private readonly VendasWebMvcContext _context;

        public VendedorServices(VendasWebMvcContext context)
        {
            _context = context;      
        }

        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.Include(obj => obj.Departamento).ToListAsync();
        }
        public async Task InsertAsync(Vendedor obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Vendedor> LocalizarPorIdAsync(int id)
        {
            return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não é possível excluir o vendedor porque ele / ela possui vendas");
            }
        }

        public async Task UpdateAsync(Vendedor obj)
        {
            bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
