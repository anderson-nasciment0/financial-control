using FinancialControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<TransacaoDTO>> GetTransacoes();
        Task<TransacaoDTO> GetById(int? id);
        Task Add(TransacaoDTO transacaoDto);
        Task Update(TransacaoDTO transacaoDto);
        Task Delete(int? id);
    }
}
