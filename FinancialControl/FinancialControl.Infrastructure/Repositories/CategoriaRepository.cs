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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _categoriaContext;

        public CategoriaRepository(AppDbContext categoriaContext)
        {
            _categoriaContext = categoriaContext;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _categoriaContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(int? id)
        {
            return await _categoriaContext.Categorias.Include(c => c.Transacoes)
                .SingleOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriaComTransacoesAsync()
        {
            return  await _categoriaContext.Categorias.Include(c => c.Transacoes).ToListAsync();
        }

        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            _categoriaContext.Add(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            _categoriaContext.Update(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> RemoveAsync(Categoria categoria)
        {
            _categoriaContext.Remove(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }


    }
}
