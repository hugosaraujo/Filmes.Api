using AutoMapper;
using Filmes.Api.Data;
using Filmes.Api.Data.DTOs;
using Filmes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private FilmContext _context;
    private IMapper _mapper;
    
    public FilmController(FilmContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddFilm([FromBody] CreateFilmDTO filmDto)
    {
        Film film = _mapper.Map<Film>(filmDto);
        _context.Films.Add(film);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, filmDto);
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
