using FinancialControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetCategorias();
        Task<CategoriaDTO> GetById(int? id);
        Task<RelatorioCategoriasDTO> GetRelatorioCategorias();
        Task Add(CategoriaDTO categoriaDto);
        Task Update(CategoriaDTO categoriaDto);
        Task Delete(int? id);
    }
}
