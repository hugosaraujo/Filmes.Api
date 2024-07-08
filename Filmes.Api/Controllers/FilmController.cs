using Filmes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private static List<Film> _films = new List<Film>();

    [HttpPost]
    public void AddFilm(Film film)
    {
        _films.Add(film);
        Console.WriteLine("Filme Adicionado!");
    }
}
