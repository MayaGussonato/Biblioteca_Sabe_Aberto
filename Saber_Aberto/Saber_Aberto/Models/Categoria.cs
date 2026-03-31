using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeBook.Models;

public partial class Categorium
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } 

    [InverseProperty("IdCategoriaNavigation")]
    [JsonIgnore]
    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
