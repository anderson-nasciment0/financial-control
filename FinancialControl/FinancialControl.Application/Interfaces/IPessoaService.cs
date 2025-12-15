using FinancialControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<PessoaDTO>> GetPessoas();
        Task<PessoaDTO> GetById(int? id);
        Task<RelatorioPessoasDTO> GetRelatorioPessoas();
        Task Add(PessoaDTO pessoaDto);
        Task Update(PessoaDTO pessoaDto);
        Task Delete(int? id);
    }
}
