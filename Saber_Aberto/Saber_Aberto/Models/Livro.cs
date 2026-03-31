using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodeBook.Models;

[Table("Livro")]
public partial class Livro
{
    [Key]
    public Guid IdLivro { get; set; }

    [StringLength(150)]
    [Unicode(false)]

    
    public string Titulo { get; set; } 

    public int? AnoPublicacao { get; set; }

    public int? Quantidade { get; set; }

    public Guid? IdAutor { get; set; }

    public Guid? IdCategoria { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagemCapa { get; set; }

    [ForeignKey("IdAutor")]
    [InverseProperty("Livros")]
    public virtual Autor? IdAutorNavigation { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Livros")]
    public virtual Categorium? IdCategoriaNavigation { get; set; }


}
