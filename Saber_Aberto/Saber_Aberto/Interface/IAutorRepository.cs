using CodeBook.Models;

public interface IAutorRepository
{
    List<Autor> Listar();
    void Cadastrar(Autor autor);
    void Atualizar(Autor autor);
    void Deletar(Guid id);
   Autor BuscarPorId(Guid id);

}