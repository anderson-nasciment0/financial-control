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
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _transacaoContext;

        public TransacaoRepository(AppDbContext transacaoContext)
        {
            _transacaoContext = transacaoContext;
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesAsync()
        {
            return await _transacaoContext.Transacoes.ToListAsync();
        }

        public async Task<Transacao> GetByIdAsync(int? id)
        {
            return await _transacaoContext.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .SingleOrDefaultAsync(t => t.TransacaoId == id);
        }

        public async Task<Transacao> CreateAsync(Transacao transacao)
        {
            _transacaoContext.Add(transacao);
            await _transacaoContext.SaveChangesAsync();
            return transacao;
        }
        public async Task<Transacao> UpdateAsync(Transacao transacao)
        {
            _transacaoContext.Update(transacao);
            await _transacaoContext.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> RemoveAsync(Transacao transacao)
        {
            _transacaoContext.Remove(transacao);
            await _transacaoContext.SaveChangesAsync();
            return transacao;   
        }


    }
}
