using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.DTOs
{
    public class PessoaDTO
    {
        public int PessoaId { get; set; }
        [Required(ErrorMessage = "Informe o nome da pessoa")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Informe a idade da pessoa")]
        public int Idade { get; set; }
    }

    public class PessoaDespesasDTO
    {
        public int PessoaId { get; set; }
        public string? Nome { get; set; }

        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }

    public class RelatorioPessoasDTO
    {
        public IEnumerable<PessoaDespesasDTO>? Pessoas { get; set; }

        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral { get; set; }
    }

}
