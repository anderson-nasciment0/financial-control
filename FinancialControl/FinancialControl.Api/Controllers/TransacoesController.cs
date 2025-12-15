using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Application.Services;
using FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        //Método para mostrar todas as trancações
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoDTO>>> Get()
        {
            try
            {
                var transacao = await _transacaoService.GetTransacoes();
                return Ok(transacao);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Método para motrar transação com base em seu id

        [HttpGet("{id}", Name = "GetTransacao")]
        public async Task<ActionResult<Transacao>> GetById(int id)
        {
            var transacao = await _transacaoService.GetById(id);
            if (transacao == null)
            {
                return NotFound();
            }
            return Ok(transacao);
        }

        //Método de cadastro de transação
        [HttpPost]
        public async Task<ActionResult> Post(TransacaoDTO transacaoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _transacaoService.Add(transacaoDto);
                return new CreatedAtRouteResult("GetPessoa", new { id = transacaoDto.TransacaoId }, transacaoDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Método para alteração de uma transação
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TransacaoDTO transacaoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != transacaoDto.TransacaoId)
                {
                    return BadRequest();
                }
                await _transacaoService.Update(transacaoDto);
                return Ok(transacaoDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Método para excluir uma transação
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var transacaoDto = await _transacaoService.GetById(id);
            if (transacaoDto == null)
            {
                return NotFound();
            }

            await _transacaoService.Delete(id);
            return NoContent();
        }
    }
}
