using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoVendas.Models.Enums;

namespace ProjetoVendas.Models
{
    public class RegistroDeVendas
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Montante { get; set; }
        public StatusDasVendas Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistroDeVendas()
        {

        }

        public RegistroDeVendas(int id, DateTime data, double montante, StatusDasVendas status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Montante = montante;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
