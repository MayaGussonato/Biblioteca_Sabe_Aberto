namespace CodeBook.DTO;

public class LivroDTO
{
    public Guid IdLivro { get; set; }
    public IFormFile? Imagem { get; set; }
    public string Titulo { get; set; }
    public int? AnoPublicacao { get; set; }
    public int? Quantidade { get; set; }
    public Guid? IdAutor { get;  set; }
    public Guid? IdCategoria { get; set; }
    public string? NomeAutor { get; set; }
    public string? NomeCategoria { get; set; }
    public string ImagemUrl { get; internal set; }
    
}