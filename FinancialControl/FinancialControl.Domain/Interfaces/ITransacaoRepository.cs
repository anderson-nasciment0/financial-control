using FinancialControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> GetTransacoesAsync();
        Task<Transacao> GetByIdAsync(int? id);
        Task<Transacao> CreateAsync(Transacao transacao);
        Task<Transacao> UpdateAsync(Transacao transacao);
        Task<Transacao> RemoveAsync(Transacao transacao);
    }
}
