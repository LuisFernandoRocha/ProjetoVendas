using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} obrigatório ")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} O tamanho do nome deve estar entre {2} a {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        [EmailAddress(ErrorMessage = "Entre com Email valido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        [Display(Name = "Data Nascimento.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser de {1} a {2}.")]
        [Display(Name = "Salario Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SalarioBase { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public ICollection<RegistroDeVendas> Vendas { get; set; } = new List<RegistroDeVendas>();

        public Vendedor()
        {

        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AddVendas(RegistroDeVendas sr)
        {
            Vendas.Add(sr);
        }

        public void RemoveVendas(RegistroDeVendas sr)
        {
            Vendas.Remove(sr);
        }

        public double TotalVendas(DateTime initial, DateTime final)
        {
            return Vendas.Where(sr => sr.Data >= initial && sr.Data <= final).Sum(sr => sr.Montante);
        }
    }
}
