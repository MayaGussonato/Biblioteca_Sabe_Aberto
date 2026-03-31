using CodeBook.BdContextConnct;
using CodeBook.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o Contexto do Banco de Dados
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<IAutorRepository, AutorRepository>();

// Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

//  Controllers
builder.Services.AddControllers();

//Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

