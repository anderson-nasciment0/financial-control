using FinancialControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Domain.Entities
{
    public class Categoria
    {
        public int CategoriaId{ get; set; }
        public string? Descricao { get; set; }
        public FinalidadeCategoria? Finalidade { get; set; }

        public ICollection<Transacao>? Transacoes { get; set; }
    }
}

