using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Domain.Entities
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }

        public ICollection<Transacao>? Transacoes { get; set; }
    }
}
