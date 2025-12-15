using FinancialControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Informe a descrição")]
        [MinLength(5)]
        [MaxLength(250)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "Informe a finalidade")]
        public FinalidadeCategoria Finalidade { get; set; }
    }

    public class CategoriaDespesasDTO
    {
        public int CategoriaId { get; set; }
        public string? Descricao { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }

    public class RelatorioCategoriasDTO
    {
        public IEnumerable<CategoriaDespesasDTO>? Categorias { get; set; }

        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral { get; set; }
    }
}
