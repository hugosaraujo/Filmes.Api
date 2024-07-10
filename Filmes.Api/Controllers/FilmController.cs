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

    /// <summary>
    /// Adiciona filme no banco de dados
    /// </summary>
    /// <param name="filmDto">Recebe um valor que deve ser um filmeDTO</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a requisição tenha exito</response>
    [HttpPost]
    public IActionResult AddFilm([FromBody] CreateFilmDTO filmDto)
    {
        Film film = _mapper.Map<Film>(filmDto);
        _context.Films.Add(film);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, filmDto);
    }

    /// <summary>
    /// Retorna todos os filmes inseridos em um banco de dados
    /// </summary>
    /// <param name="skip">Recebe um valor de início para mostrar o resultado da busca, para mostrar ao usuário os filme a partir do número passado como início da busca</param>
    /// <param name="take">Recebe um valor para mostrar uma quantidade definida de resultados a partir do que foi passado no valor skip</param>
    /// <returns>IEnumerable de ReadFilmDTO </returns>
    /// <response code="200">Caso a requisição tenha exito</response>
    [HttpGet]
    public IEnumerable<ReadFilmDTO> GetFilms(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 25)
    {
        return _mapper.Map<List<ReadFilmDTO>>(_context.Films.Skip(skip).Take(take));
    }


    /// <summary>
    /// Retorna um filme pelo Id dele
    /// </summary>
    /// <param name="id">recebe um inteiro com um número de Id</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a requisição tenha êxito</response>
    [HttpGet("{id}")]
    public IActionResult GetFilmById(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        var filmDto = _mapper.Map<ReadFilmDTO>(film);
        return Ok(filmDto);
    }

    /// <summary>
    /// Altera os dados de um filme
    /// </summary>
    /// <param name="id">recebe um inteiro para identificar o filme</param>
    /// <param name="filmDto">recebe um filme do tipo UpdateFilmDTO para alteração</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a requisição tenha êxito</response>
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

    /// <summary>
    /// Altera o campo de um filme
    /// </summary>
    /// <param name="id">recebe um inteiro para buscar pelo filme no banco de dados</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a requisição tenha êxito</response>
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

    /// <summary>
    /// Apaga um filme do banco de dados
    /// </summary>
    /// <param name="id">Recebe um inteiro para buscar o filme a ser apagado do banco de dados</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso o filme seja apagado do banco</response>
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
