using CodeBook.Models;

public interface ICategoriaRepository
{
    List<Categorium> Listar();
    void Cadastrar(Categorium categorium);
    void Deletar(Guid idCategoria);
    void Atualizar(Guid idCategoria, Categorium categorium);
    List<Categorium> ListarPorNome(string nome);
    List<Categorium> ListarPorId(Guid idCategoria);
    
}