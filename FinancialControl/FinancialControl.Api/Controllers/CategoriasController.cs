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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //Método para mostrar todas as categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            try
            {
                var categorias = await _categoriaService.GetCategorias();
                return Ok(categorias);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Método para motrar categoria com base em seu id
        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        //Método para montar um relatório de despesas das categorias
        [HttpGet("RelatorioDespesasCategorias")]
        public async Task<ActionResult<IEnumerable<RelatorioCategoriasDTO>>> GetDespesasRelatorio()
        {
            try
            {
                var relatorio = await _categoriaService.GetRelatorioCategorias();
                return Ok(relatorio);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Método de cadastro de categoria
        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _categoriaService.Add(categoriaDto);
            return new CreatedAtRouteResult("GetCategoria", new { id = categoriaDto.CategoriaId }, categoriaDto);
        }

        //Método para alteração de uma categoria
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
            await _categoriaService.Update(categoriaDto);
            return Ok(categoriaDto);
        }

        //Método para excluir uma categoria
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
    var categoriaDto = await _categoriaService.GetById(id);
            if (categoriaDto == null)
            {
                return NotFound();
            }

            await _categoriaService.Delete(id);
            return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest("Não é possivel deletar categoria pois possuem transações em que ela é utilizada");
            }
        
        }
    }
}
