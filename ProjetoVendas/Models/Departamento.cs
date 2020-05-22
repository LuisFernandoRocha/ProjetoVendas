﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }



        public void AddVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double TotalVendas(DateTime inicial, DateTime final)
        {
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(inicial, final));
   
        }

        //public void AddSeller(Seller seller)
        //{
        //    Sellers.Add(seller);
        //}
        //public double TotalSales(DateTime initial, DateTime final)
        //{
        //    return Sellers.Sum(seller => seller.TotalSales(initial, final));

        //}
    }
}
