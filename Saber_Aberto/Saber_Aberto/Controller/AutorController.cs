using CodeBook.DTO;
using CodeBook.Models;
using CodeBook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeBook.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorRepository _autorRepository;

    public AutorController(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }
    /// <summary>
    /// lista todos os autores cadastrados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        var autores = _autorRepository.Listar();

        var autoresDTO = autores.Select(a => new AutorDTO
        {
            IdAutor = a.IdAutor,
            Nome = a.Nome
        });

        return Ok(autoresDTO);
    }
    /// <summary>
    ///  lista um ator especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var a = _autorRepository.BuscarPorId(id);

        if (a == null)
            return NotFound();

        var autorDTO = new AutorDTO
        {
            IdAutor = a.IdAutor,
            Nome = a.Nome
        };

        return Ok(autorDTO);
    }
    /// <summary>
    /// cadastra um novo autor, o id é gerado automaticamente
    /// </summary>
    /// <param name="autor"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post(AutorCreateDTO dto)
    {
        Autor autor = new Autor
        {
            IdAutor = Guid.NewGuid(), //  gera automaticamente
            Nome = dto.Nome
        };

        _autorRepository.Cadastrar(autor);

        return Ok();
    }
    /// <summary>
    /// atualiza um autor existente
    /// </summary>
    /// <param name="autor"></param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult Put(Autor autor)
    {
        _autorRepository.Atualizar(autor);
        return Ok();
    }
    /// <summary>
    /// deleta um autor existente 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _autorRepository.Deletar(id);
        return Ok();
    }
}