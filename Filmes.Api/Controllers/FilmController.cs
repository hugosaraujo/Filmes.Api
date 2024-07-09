using AutoMapper;
using Filmes.Api.Data;
using Filmes.Api.Data.DTOs;
using Filmes.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    public IEnumerable<ReadFilmDTO> GetFilms(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 5)
    {
        return _mapper.Map<List<ReadFilmDTO>>(_context.Films.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetFilmById(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        var filmDto = _mapper.Map<ReadFilmDTO>(film);
        return Ok(filmDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilm(
        int id,
        [FromBody] UpdateFilmDTO filmDto)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        _mapper.Map(filmDto, film);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateFilmField(
        int id,
        JsonPatchDocument<UpdateFilmDTO> patch)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        var updatedFilm = _mapper.Map<UpdateFilmDTO>(film);
        patch.ApplyTo(updatedFilm, ModelState);
        if (!TryValidateModel(updatedFilm)) return ValidationProblem(ModelState);
        _mapper.Map(updatedFilm, film);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFilm(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        _context.Remove(film);
        _context.SaveChanges();
        return NoContent();
    }
}
