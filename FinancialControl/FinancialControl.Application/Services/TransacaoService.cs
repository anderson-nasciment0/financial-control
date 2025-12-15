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
    public class TransacaoService : ITransacaoService
    {
        //Repositório de transações, pessoa e categorias, e mapper para conversão entre entidade e DTO

        private ITransacaoRepository _transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public TransacaoService(ITransacaoRepository transacaoRepository, IMapper mapper, IPessoaRepository pessoaRepository, ICategoriaRepository categoriaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
            _categoriaRepository = categoriaRepository;
        }

        //Retorno das transações cadastradas

        public async Task<IEnumerable<TransacaoDTO>> GetTransacoes()
        {
            try
            {
                var transacoes = await _transacaoRepository.GetTransacoesAsync();
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoDTO>>(transacoes);
                return transacoesDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retorno de uma transação pelo id

        public async Task<TransacaoDTO> GetById(int? id)
        {
            var transacao = await _transacaoRepository.GetByIdAsync(id);
            return _mapper.Map<TransacaoDTO>(transacao);
        }

        //Método que cadastra transações e limita as regras para os tipos de transação

        public async Task Add(TransacaoDTO transacaoDto)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(transacaoDto.PessoaId);
            if(pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa não encontrada");
            }
            if (pessoa.Idade < 18 && transacaoDto.TipoTransacao == TipoTransacao.Receita)
            {
                throw new InvalidOperationException("Pessoa menor de 18 não pode cadastrar receita");
            }

            var categoria = await _categoriaRepository.GetByIdAsync(transacaoDto.CategoriaId);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada");
            }

            if (categoria.Finalidade != FinalidadeCategoria.Ambas &&
                (FinalidadeCategoria)transacaoDto.TipoTransacao != categoria.Finalidade)
            {
                throw new InvalidOperationException("A categoria não pode ser de um tipo diferente da transação");
            }

            var transacao = _mapper.Map<Transacao>(transacaoDto);
            await _transacaoRepository.CreateAsync(transacao);
        }

        //Método que altera transações

        public async Task Update(TransacaoDTO transacaoDto)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(transacaoDto.PessoaId);
            if (pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa não encontrada");
            }
            if (pessoa.Idade < 18 && transacaoDto.TipoTransacao == TipoTransacao.Receita)
            {
                throw new InvalidOperationException("Pessoa menor de 18 não pode cadastrar receita");
            }

            var categoria = await _categoriaRepository.GetByIdAsync(transacaoDto.CategoriaId);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada");
            }

            if (categoria.Finalidade != FinalidadeCategoria.Ambas &&
                (FinalidadeCategoria)transacaoDto.TipoTransacao != categoria.Finalidade)
            {
                throw new InvalidOperationException("A categoria não pode ser de um tipo diferente da transação");
            }

            var transacao = _mapper.Map<Transacao>(transacaoDto);
            await _transacaoRepository.UpdateAsync(transacao);
        }

        //Método que exclui transações
        public async Task Delete(int? id)
        {
            var transacao = _transacaoRepository.GetByIdAsync(id).Result;
            await _transacaoRepository.RemoveAsync(transacao);
        }
    }
}
