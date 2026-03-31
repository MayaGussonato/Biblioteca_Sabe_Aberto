using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CodeBook.Models;

namespace CodeBook.BdContextConnct;

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext()
    {
    }

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Biblioteca;Trusted_connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__DD33B031B6EF2E35");

            entity.Property(e => e.IdAutor).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A102C8F651D");

            entity.Property(e => e.IdCategoria).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.IdLivro).HasName("PK__Livro__3B69D85A628901D6");

            entity.Property(e => e.IdLivro).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Livros).HasConstraintName("FK__Livro__IdAutor__68487DD7");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Livros).HasConstraintName("FK__Livro__IdCategor__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
