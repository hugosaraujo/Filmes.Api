using Filmes.Api.Data;
using Filmes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private FilmContext _context;
    
    public FilmController(FilmContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddFilm([FromBody] Film film)
    {
        _context.Films.Add(film);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
    }

    [HttpGet]
    public IEnumerable<Film> GetFilms(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 5)
    {
        return _context.Films.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetFilmById(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        return Ok(film);
    }
}
