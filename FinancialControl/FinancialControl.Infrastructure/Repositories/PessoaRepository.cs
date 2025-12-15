using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Infrastructure.Repositories
{
    //Métodos que são chamados pelos services
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _pessoaContext;

        public PessoaRepository(AppDbContext pessoaContext)
        {
            _pessoaContext = pessoaContext;
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasAsync()
        {
            return await _pessoaContext.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> GetByIdAsync(int? id)
        {
            return await _pessoaContext.Pessoas.Include(p => p.Transacoes)
                 .SingleOrDefaultAsync(p => p.PessoaId == id);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasComTransacoesAsync()
        {
            return await _pessoaContext.Pessoas.Include(p => p.Transacoes).ToListAsync();
        }

        public async Task<Pessoa> CreateAsync(Pessoa pessoa)
        {
            _pessoaContext.Add(pessoa);
            await _pessoaContext.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            _pessoaContext.Update(pessoa);
            await _pessoaContext.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> RemoveAsync(Pessoa pessoa)
        {
            _pessoaContext.Remove(pessoa);
            await _pessoaContext.SaveChangesAsync();
            return pessoa;
        }
    }
}
