using Microsoft.AspNetCore.Mvc;
using FornecedoresApi.Models;
using FornecedoresApi.Data;
using FornecedoresApi.Data.Dtos;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FornecedoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FornecedoresController : ControllerBase
{

    private SistemaDbContext _context;

    public FornecedoresController(SistemaDbContext context)
    {
        _context = context;
    }




    /// <summary>
    /// Adiciona um Fornecedor ao banco de dados
    /// </summary>
    /// <param name="CriarFornecedorDto">Objeto com os campos necessários para criação de um Fornecedor</param>
    /// <returns>Task de IActionResult</returns>
    /// <response code="201">Caso a criação seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CriarFornecedor([FromBody] CriarFornecedorDto criarFornecedorDto) 
    {
        var fornecedor = new Fornecedor()
        {
            RazaoSocial = criarFornecedorDto.RazaoSocial,
            Endereco = criarFornecedorDto.Endereco,
            Contatos = criarFornecedorDto.Contatos.Select((c) => new Contato()
            {
                Nome = c.Nome,
                Email = c.Email
            }).ToList()
        };
        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CriarFornecedor), new {id = fornecedor.Id});

    }

    /// <summary>
    /// Lista todos os Fornecedores que existem no banco de dados
    /// </summary>
    /// <param name="ListarFornecedorDto">Objeto com os campos necessários para listagem de um fornecedor</param>
    /// <returns>IActionResult de IEnumerable</returns>
    /// <response code="200">Caso a listagem seja feita com sucesso</response>
    [HttpGet]
    public ActionResult<IEnumerable<ListarFornecedorDto>> ListarFornecedores() 
    {
        var fornecedores = _context.Fornecedores.Include(x => x.Contatos).ToList();
        return fornecedores.Select((x) => new ListarFornecedorDto() 
        {
            Id = x.Id,
            RazaoSocial = x.RazaoSocial,
            Endereco = x.Endereco,
            Contatos = x.Contatos.Select((c) => new ListarContatoDto() {
                Nome = c.Nome,
                Email = c.Email,
            }).ToList()
        }).ToList();
    }

    /// <summary>
    /// Lista um Fornecedor através de um Id passado por parametro
    /// </summary>
    /// <param name="ListarFornecedorDto">Objeto com os campos necessários para listagem de um fornecedor</param>
    /// <returns>ActionResult de ListarFornecedorDto</returns>
    /// <response code="200">Caso a listagem seja feita com sucesso</response>
    [HttpGet("{id}")]
    public ActionResult<ListarFornecedorDto> ListarFornecedor(int id) 
    {
        var fornecedor = _context.Fornecedores.Include(x => x.Contatos).FirstOrDefault(fornecedor => fornecedor.Id == id);
        if (fornecedor == null) return NotFound();
        var listarFornecedorDto = new ListarFornecedorDto() {
            Id = fornecedor.Id,
            RazaoSocial = fornecedor.RazaoSocial,
            Endereco = fornecedor.Endereco,
            Contatos = fornecedor.Contatos.Select((c) => new ListarContatoDto() {
                Nome = c.Nome,
                Email = c.Email,
            }).ToList()
        };
        return Ok(listarFornecedorDto);
    }

    /// <summary>
    /// Atualiza um Fornecedor, através de um Id passado por parametro
    /// </summary>
    /// <param name="AlterarFornecedorDto">Objeto com os campos necessários para Alteração de um Fornecedor</param>
    /// <returns>Task de IActionResult</returns>
    /// <response code="200">Caso a Atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> AlterarFornecedor(int id, [FromBody] AlterarFornecedorDto alterarFornecedorDto) 
    {
        var fornecedor = _context.Fornecedores.Include(x => x.Contatos).FirstOrDefault(fornecedor => fornecedor.Id == id);
        if (fornecedor == null) return NotFound();

        fornecedor.RazaoSocial = alterarFornecedorDto.RazaoSocial;
        fornecedor.Endereco = alterarFornecedorDto.Endereco;
        fornecedor.Contatos = alterarFornecedorDto.Contatos.Select(x => new Contato()
        {
            Nome = x.Nome,
            Email = x.Email
        }).ToList();


        _context.Fornecedores.Update(fornecedor);
        _context.SaveChanges();

        return CreatedAtAction(nameof(AlterarFornecedor), new {id = fornecedor.Id});
    }

    /// <summary>
    /// Deleta um Fornecedor do banco de dados
    /// </summary>
    /// <param name="Fornecedor.Id">Objeto com os campos necessários para deleção de um Fornecedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a Deleção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeletarFornecedor(int id) 
    {
        var fornecedor = _context.Fornecedores.FirstOrDefault(
            fornecedor => fornecedor.Id == id);
        if (fornecedor == null) return NotFound();
        _context.Remove(fornecedor);
        _context.SaveChanges();
        return NoContent();
    }


}
