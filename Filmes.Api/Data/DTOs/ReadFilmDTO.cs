using System.ComponentModel.DataAnnotations;

namespace Filmes.Api.Data.DTOs;

public class ReadFilmDTO
{
    public string Title { get; set; }
    public int RunningTime { get; set; }
    public string Genre { get; set; }
    public DateTime QueryTime { get; set; } = DateTime.Now;
}
