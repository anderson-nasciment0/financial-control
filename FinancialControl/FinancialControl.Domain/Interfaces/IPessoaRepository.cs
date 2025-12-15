using FinancialControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetPessoasAsync();
        Task<Pessoa> GetByIdAsync(int? id);
        Task<IEnumerable<Pessoa>> GetPessoasComTransacoesAsync();
        Task<Pessoa> CreateAsync(Pessoa pessoa);
        Task<Pessoa> UpdateAsync(Pessoa pessoa);
        Task<Pessoa> RemoveAsync(Pessoa pessoa);
    }
}
