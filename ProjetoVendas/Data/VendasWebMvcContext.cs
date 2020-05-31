using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Data
{
    public class VendasWebMvcContext : DbContext
    {
        public VendasWebMvcContext(DbContextOptions<VendasWebMvcContext> options)
           : base(options)
        {
        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<RegistroDeVendas> RegistroDeVendas { get; set; }

    }
}
