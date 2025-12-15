using AutoMapper;
using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        //Repositório de categorias e mapper para conversão entre entidade e DTO
        private ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        //Retorno das categorias cadastradas
        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            try
            {
                var categorias = await _categoriaRepository.GetCategoriasAsync();
                var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
                return categoriasDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retorno de uma categoria pelo id
        public async Task<CategoriaDTO> GetById(int? id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        //Método que gera relatório de categorias
        public async Task<RelatorioCategoriasDTO> GetRelatorioCategorias()
        {
            var categorias = await _categoriaRepository.GetCategoriaComTransacoesAsync();
            var CategoriasRelatorio = categorias.Select(c =>
            {
                var receitas = c.Transacoes.Where(t => t.TipoTransacao == TipoTransacao.Receita).Sum(t => t.Valor);

                var despesas = c.Transacoes.Where(t => t.TipoTransacao == TipoTransacao.Despesa).Sum(t => t.Valor);

                var saldo = receitas - despesas;

                return new CategoriaDespesasDTO
                {
                    CategoriaId = c.CategoriaId,
                    Descricao = c.Descricao,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas,
                    Saldo = saldo
                };
            }).ToList();

            var totalReceitasGeral = CategoriasRelatorio.Sum(t => t.TotalReceitas);
            var totalDespesasGeral = CategoriasRelatorio.Sum(t => t.TotalDespesas);

            return new RelatorioCategoriasDTO
            {
                Categorias = CategoriasRelatorio,
                TotalReceitasGeral = totalReceitasGeral,
                TotalDespesasGeral = totalDespesasGeral,
                SaldoGeral = totalReceitasGeral - totalDespesasGeral
            };
        }

        //Método que cadastra categorias
        public async Task Add(CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.CreateAsync(categoria);
        }

        //Método que altera categorias
        public async Task Update(CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.UpdateAsync(categoria);
        }

        //Método que exclui categorias
        public async Task Delete(int? id)
        {
            var categoria = _categoriaRepository.GetByIdAsync(id).Result;
            await _categoriaRepository.RemoveAsync(categoria);
        }
    }
}
