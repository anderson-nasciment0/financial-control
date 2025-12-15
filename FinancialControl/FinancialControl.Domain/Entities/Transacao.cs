using FinancialControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Domain.Entities
{
    public class Transacao
    {
        public int TransacaoId { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao? TipoTransacao { get; set; }

        //FK para pessoa
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }

        //FK para categoria
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
