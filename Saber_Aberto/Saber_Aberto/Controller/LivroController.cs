using CodeBook.DTO;
using CodeBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        /// <summary>
        /// Lista todos os livros
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var livros = _livroRepository.Listar();

            var livrosDTO = livros.Select(l => new LivroDTO
            {
                IdLivro = l.IdLivro,
                Titulo = l.Titulo,
                AnoPublicacao = l.AnoPublicacao,
                Quantidade = l.Quantidade,
                NomeAutor = l.IdAutorNavigation?.Nome,
                NomeCategoria = l.IdCategoriaNavigation?.Nome
            });

            return Ok(livrosDTO);
        }

        /// <summary>
        /// Buscar livro por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var l = _livroRepository.BuscarPorId(id);

            if (l == null)
                return NotFound("Livro não encontrado");

            var livroDTO = new LivroDTO
            {
                IdLivro = l.IdLivro,
                Titulo = l.Titulo,
                AnoPublicacao = l.AnoPublicacao,
                Quantidade = l.Quantidade,
                NomeAutor = l.IdAutorNavigation?.Nome,
                NomeCategoria = l.IdCategoriaNavigation?.Nome
            };

            return Ok(livroDTO);
        }

        /// <summary>
        /// Cadastra novo livro
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] LivroDTO livro)
        {
            if (string.IsNullOrWhiteSpace(livro.Titulo))
                return BadRequest("Título é obrigatório");

            Livro novoLivro = new Livro
            {
                Titulo = livro.Titulo,
                AnoPublicacao = livro.AnoPublicacao,
                Quantidade = livro.Quantidade,
                IdAutor = livro.IdAutor,
                IdCategoria = livro.IdCategoria
            };

            if (livro.Imagem != null && livro.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(livro.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                var caminho = Path.Combine(pasta, nomeArquivo);
                using (var stream = new FileStream(caminho, FileMode.Create))
                {
                    await livro.Imagem.CopyToAsync(stream);
                }

                novoLivro.ImagemCapa = nomeArquivo;
            }

            try
            {
                _livroRepository.Cadastrar(novoLivro);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza livro por ID
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] LivroDTO livroAtualizado)
        {
            var livroBuscado = _livroRepository.BuscarPorId(id);
            if (livroBuscado == null)
                return NotFound("Livro não encontrado");

            if (!string.IsNullOrWhiteSpace(livroAtualizado.Titulo))
                livroBuscado.Titulo = livroAtualizado.Titulo;

            livroBuscado.AnoPublicacao = livroAtualizado.AnoPublicacao;
            livroBuscado.Quantidade = livroAtualizado.Quantidade;
            livroBuscado.IdAutor = livroAtualizado.IdAutor;
            livroBuscado.IdCategoria = livroAtualizado.IdCategoria;

            if (livroAtualizado.Imagem != null && livroAtualizado.Imagem.Length > 0)
            {
                var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

                // Remove imagem antiga
                if (!string.IsNullOrEmpty(livroBuscado.ImagemCapa))
                {
                    var caminhoAntigo = Path.Combine(pasta, livroBuscado.ImagemCapa);
                    if (System.IO.File.Exists(caminhoAntigo))
                        System.IO.File.Delete(caminhoAntigo);
                }

                // Salva nova imagem
                var extensao = Path.GetExtension(livroAtualizado.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                var caminhoNovo = Path.Combine(pasta, nomeArquivo);
                using (var stream = new FileStream(caminhoNovo, FileMode.Create))
                {
                    await livroAtualizado.Imagem.CopyToAsync(stream);
                }

                livroBuscado.ImagemCapa = nomeArquivo;
            }

            try
            {
                _livroRepository.Atualizar(id, livroBuscado);
                return Ok("Livro atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deleta livro por ID
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var livro = _livroRepository.BuscarPorId(id);
            if (livro == null)
                return NotFound("Livro não encontrado");

            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

            if (!string.IsNullOrEmpty(livro.ImagemCapa))
            {
                var caminhoImagem = Path.Combine(pasta, livro.ImagemCapa);
                if (System.IO.File.Exists(caminhoImagem))
                    System.IO.File.Delete(caminhoImagem);
            }

            _livroRepository.Deletar(id);
            return Ok("Livro e imagem deletados!");
        }

        /// <summary>
        /// Buscar livro por título
        /// </summary>
        [HttpGet("titulo/{titulo}")]
        public IActionResult GetByTitulo(string titulo)
        {
            var livros = _livroRepository.ListarPorTitulo(titulo);

            var livrosDTO = livros.Select(l => new LivroDTO
            {
                IdLivro = l.IdLivro,
                Titulo = l.Titulo,
                NomeAutor = l.IdAutorNavigation?.Nome,
                NomeCategoria = l.IdCategoriaNavigation?.Nome
            });

            return Ok(livrosDTO);
        }
    }
}