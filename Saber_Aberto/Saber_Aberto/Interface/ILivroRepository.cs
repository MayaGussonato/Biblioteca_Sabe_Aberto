using CodeBook.Models;

public interface ILivroRepository
{
    List<Livro> Listar();
    Livro BuscarPorId(Guid id);
    void Cadastrar(Livro livro);
    void Atualizar(Guid id, Livro livro);
    void Deletar(Guid id);
    List<Livro> ListarPorTitulo(string titulo);
    List<Livro> ListarPorCategoria(Guid idCategoria);
    List<Livro> ListarPorAutor(Guid idAutor);
}