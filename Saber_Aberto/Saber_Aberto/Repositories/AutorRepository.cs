using CodeBook.BdContextConnct;
using CodeBook.Models;

namespace CodeBook.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaContext _context;

        public AutorRepository(BibliotecaContext context)
        {
            _context = context;
        }

        // LISTAR
        public List<Autor> Listar()
        {
            return _context.Autors.ToList();
        }

        //  BUSCAR POR ID
        public Autor BuscarPorId(Guid id)
        {
            return _context.Autors.FirstOrDefault(a => a.IdAutor == id);
        }

        //  CADASTRAR
        public void Cadastrar(Autor autor)
        {
            autor.IdAutor = Guid.NewGuid(); // garante ID
            _context.Autors.Add(autor);
            _context.SaveChanges();
        }

        //  ATUALIZAR
        public void Atualizar(Autor autor)
        {
            Autor? autorBuscado = _context.Autors.Find(autor.IdAutor);

            if (autorBuscado != null)
            {
                autorBuscado.Nome = autor.Nome;

                _context.Autors.Update(autorBuscado);
                _context.SaveChanges();
            }
        }

        //  DELETAR
        public void Deletar(Guid id)
        {
            Autor? autorBuscado = _context.Autors.Find(id);

            if (autorBuscado != null)
            {
                _context.Autors.Remove(autorBuscado);
                _context.SaveChanges();
            }
        }
    }
}