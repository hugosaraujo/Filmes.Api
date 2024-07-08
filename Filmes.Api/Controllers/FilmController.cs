using Filmes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private static List<Film> _films = new List<Film>();
    private static int _id = 1;

    [HttpPost]
    public IActionResult AddFilm([FromBody] Film film)
    {
        film.Id = _id++;
        _films.Add(film);
        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
    }

    [HttpGet]
    public IEnumerable<Film> GetFilms(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 5)
    {
        return _films.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetFilmById(int id)
    {
        var film = _films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        return Ok(film);
    }
}
