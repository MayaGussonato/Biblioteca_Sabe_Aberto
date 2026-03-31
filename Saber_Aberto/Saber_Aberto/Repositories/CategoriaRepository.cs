using CodeBook.BdContextConnct;
using CodeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBook.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BibliotecaContext _context;
        public CategoriaRepository(BibliotecaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Atualiza uma categoria existente 
        /// </summary>
        /// <param name="idCategoria">Id de uma categoria</param>
        /// <param name="categorium">objeto contento os novos dados</param>
        public void Atualizar(Guid idCategoria, Categorium categorium)
        {
            Categorium? CategoriaBuscada = _context.Categoria.Find(idCategoria);
            if (CategoriaBuscada != null)
            {
                CategoriaBuscada.Nome = categorium.Nome;
                _context.Categoria.Update(CategoriaBuscada);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Cadastra uma nova Categoria
        /// </summary>
        /// <param name="categorium">objeto tendo os dados da categoria</param>
        public void Cadastrar(Categorium categorium)
        {
            categorium.IdCategoria = Guid.NewGuid(); // 👈 ESSENCIAL

            _context.Categoria.Add(categorium);
            _context.SaveChanges();
        }

        /// <summary>
        /// fDeleta uma categoria existente a partir do id
        /// </summary>
        /// <param name="idCategoria"></param>
        public void Deletar(Guid idCategoria)
        {
            Categorium? CategoriaBuscada = _context.Categoria.Find(idCategoria);
            if (CategoriaBuscada != null)
            {
                _context.Categoria.Remove(CategoriaBuscada);
                _context.SaveChanges();
            }

        }

        /// <summary>
        /// Lista todas as categorias cadastradas no banco de dados
        /// </summary>
        /// <returns></returns>
        public List<Categorium> Listar()
        {
            return _context.Categoria.ToList();
        }
        /// <summary>
        /// lista as categorias a partir do id, caso haja mais de uma categoria com o mesmo id, todas serão listadas
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        List<Categorium> ICategoriaRepository.ListarPorId(Guid idCategoria)
        {
           return _context.Categoria.Where(c => c.IdCategoria == idCategoria).ToList();
        }
        /// <summary>
        /// lista as categorias a partir do nome, caso haja mais de uma categoria com o mesmo nome, todas serão listadas
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        List<Categorium> ICategoriaRepository.ListarPorNome(string nome)
        {
            return _context.Categoria.Where(c => c.Nome.Contains(nome)).ToList();
        }
    }
}
