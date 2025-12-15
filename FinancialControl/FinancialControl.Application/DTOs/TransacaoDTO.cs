using FinancialControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.DTOs
{
    public class TransacaoDTO
    {
        public int TransacaoId { get; set; }
        [Required(ErrorMessage = "Informe a descrição")]
        [MinLength(5)]
        [MaxLength(250)]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Informe o tipo da transação")]
        public TipoTransacao TipoTransacao { get; set; }
        public int PessoaId { get; set; }
        public int CategoriaId { get; set; }
    }
}
