using Microsoft.EntityFrameworkCore;
using CodeBook.Models;
using CodeBook.BdContextConnct;

public class LivroRepository : ILivroRepository
{
    private readonly BibliotecaContext _context;

    public LivroRepository(BibliotecaContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza os dados do Livros
    /// </summary>
    /// <param name="id">Identificador único do livro</param>
    /// <param name="livro">Objeto contendo os novos dados do Livro</param>
    public void Atualizar(Guid id, Livro livro)
    {
        Livro? LivroBuscado = _context.Livros.Find(id);

        if (LivroBuscado != null)
        {
            LivroBuscado.Titulo = livro.Titulo;
            LivroBuscado.AnoPublicacao = livro.AnoPublicacao;
            LivroBuscado.Quantidade = livro.Quantidade;
            LivroBuscado.IdAutor = livro.IdAutor;
            LivroBuscado.IdCategoria = livro.IdCategoria;

            _context.Livros.Update(LivroBuscado);
            _context.SaveChanges();

        }
    }

    /// <summary>
    /// Busca um livro pelo seu identificador
    /// </summary>
    /// <param name="id">Id unico do livro</param>
    /// <returns>Livro encontrato ou nulo caso nao exista</returns>
    public Livro BuscarPorId(Guid id)
    {
        return _context.Livros
            .Include(l => l.IdAutorNavigation)
            .Include(l => l.IdCategoriaNavigation)
            .FirstOrDefault(l => l.IdLivro == id);
    }


    /// <summary>
    /// Cadastra um novo Livro
    /// </summary>
    /// <param name="livro">Objeto tendo os dados do Livro</param>
    public void Cadastrar(Livro livro)
    {
      _context.Livros.Add(livro);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um Livro existente
    /// </summary>
    /// <param name="id">id unico do livro</param>
    public void Deletar(Guid id)
    {
        Livro? livroBuscado = _context.Livros.Find(id);
        if (livroBuscado != null)
        {
            _context.Livros.Remove(livroBuscado);
            _context.SaveChanges();

        }
    }

    /// <summary>
    /// Lista todos os livros cadastrados
    /// </summary>
    /// <returns></returns>
    public List<Livro> Listar()
    {
        return _context.Livros
            .Include(l => l.IdAutorNavigation)
            .Include(l => l.IdCategoriaNavigation)
            .ToList();
    }
    /// <summary>
    /// lista os livros por um autor específico
    /// </summary>
    /// <param name="idAutor"></param>
    /// <returns></returns>
    public List<Livro> ListarPorAutor(Guid idAutor)
    {
       return _context.Livros
            .Include(l => l.IdAutorNavigation)
            .Include(l => l.IdCategoriaNavigation)
            .Where(l => l.IdAutor == idAutor)
            .ToList();
    }
    /// <summary>
    /// lista os livros por uma categoria específica
    /// </summary>
    /// <param name="idCategoria"></param>
    /// <returns></returns>
    public List<Livro> ListarPorCategoria(Guid idCategoria)
    {
        return _context.Livros
            .Include(l => l.IdAutorNavigation)
            .Include(l => l.IdCategoriaNavigation)
            .Where(l => l.IdCategoria == idCategoria)
            .ToList();
    }
    /// <summary>
    /// lista os livros por título
    /// </summary>
    /// <param name="titulo"></param>
    /// <returns></returns>
    public List<Livro> ListarPorTitulo(string titulo)
    {
       return _context.Livros
            .Include(l => l.IdAutorNavigation)
            .Include(l => l.IdCategoriaNavigation)
            .Where(l => l.Titulo.Contains(titulo))
            .ToList();
    }
}

