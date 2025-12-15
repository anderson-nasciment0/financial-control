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
    public class PessoaService : IPessoaService
    {
        //Repositório de pessoas e mapper para conversão entre entidade e DTO
        private IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaService(IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        //Retorno das pessoas cadastradas
        public async Task<IEnumerable<PessoaDTO>> GetPessoas()
        {
            try
            {
                var pessoas = await _pessoaRepository.GetPessoasAsync();
                var pessoasDto = _mapper.Map<IEnumerable<PessoaDTO>>(pessoas);
                return pessoasDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retorno de uma pessoa pelo id
        public async Task<PessoaDTO> GetById(int? id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            return _mapper.Map<PessoaDTO>(pessoa);
        }

        //Método que gera relatório de pessoas
        public async Task<RelatorioPessoasDTO> GetRelatorioPessoas()
        {
            var pessoas = await _pessoaRepository.GetPessoasComTransacoesAsync();
            var pessoasRelatorio = pessoas.Select(p =>
            {
                var receitas = p.Transacoes.Where(t => t.TipoTransacao == TipoTransacao.Receita).Sum(t => t.Valor);

                var despesas = p.Transacoes.Where(t => t.TipoTransacao == TipoTransacao.Despesa).Sum(t => t.Valor);

                var saldo = receitas - despesas;

                return new PessoaDespesasDTO
                {
                    PessoaId = p.PessoaId,
                    Nome = p.Nome,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas,
                    Saldo = saldo
                };
            }).ToList();

            var totalReceitasGeral = pessoasRelatorio.Sum(t => t.TotalReceitas);
            var totalDespesasGeral = pessoasRelatorio.Sum(t => t.TotalDespesas);

            return new RelatorioPessoasDTO
            {
                Pessoas = pessoasRelatorio,
                TotalReceitasGeral = totalReceitasGeral,
                TotalDespesasGeral = totalDespesasGeral,
                SaldoGeral = totalReceitasGeral - totalDespesasGeral
            };
        }

        //Método que cadastra pessoas
        public async Task Add(PessoaDTO pessoaDto)
        {
            var pessoa = _mapper.Map<Pessoa>(pessoaDto);
            await _pessoaRepository.CreateAsync(pessoa);
        }

        //Método que altera pessoas
        public async Task Update(PessoaDTO pessoaDto)
        {
            var pessoa = _mapper.Map<Pessoa>(pessoaDto);
            await _pessoaRepository.UpdateAsync(pessoa);
        }

        //Método que exclui pessoas
        public async Task Delete(int? id)
        {
            var pessoa = _pessoaRepository.GetByIdAsync(id).Result;
            await _pessoaRepository.RemoveAsync(pessoa);
        }
    }
}
