using Filmes.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.Api.Data;

public class FilmContext : DbContext
{
    public FilmContext(DbContextOptions<FilmContext> opts): base(opts)
    {
        
    }
    //isso aqui é o nome da tabela!
    public DbSet<Film> Films { get; set; }
}
