using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CodeBook.Models;

[Table("Autor")]
public partial class Autor
{
    [Key]
    public Guid IdAutor { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } 

    [InverseProperty("IdAutorNavigation")]
    [JsonIgnore]
    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
