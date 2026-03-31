using CodeBook.DTO;
using CodeBook.Models;
using CodeBook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeBook.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    /// <summary>
    /// listar todas as categorias
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        var categorias = _categoriaRepository.Listar();

        var categoriasDTO = categorias.Select(c => new CategoriaDTO
        {
            IdCategoria = c.IdCategoria,
            Nome = c.Nome
        });

        return Ok(categoriasDTO);
    }
    /// <summary>
    /// listar categoria por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(_categoriaRepository.ListarPorId(id));
    }
    /// <summary>
    /// cadastrar nova categoria
    /// </summary>
    /// <param name="categoria"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post(CategoriaCreateDTO dto)
    {
        Categorium categoria = new Categorium
        {
            IdCategoria = Guid.NewGuid(), //  gera ID único
            Nome = dto.Nome
        };

        _categoriaRepository.Cadastrar(categoria);

        return Ok();
    }
    /// <summary>
    /// atualiza categoria por id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoria"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, Categorium categoria)
    {
        _categoriaRepository.Atualizar(id, categoria);
        return Ok();
    }
    /// <summary>
    /// deletar categoria por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _categoriaRepository.Deletar(id);
        return Ok();
    }
}