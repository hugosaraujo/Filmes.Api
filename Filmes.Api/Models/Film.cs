using System.ComponentModel.DataAnnotations;

namespace Filmes.Api.Models;

public class Film
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    [MaxLength(100, ErrorMessage = "O tamanho do título deve ser menor que 100 caracteres")]
    public string Title { get; set; }
    [Required]
    [Range(60, 450, ErrorMessage = "A duração inserida é inválida. Tente algo entre 60 e 450")]
    public int RunningTime { get; set; }
    [Required(ErrorMessage = "É necessário inserir um gênero para o filme")]
    [MaxLength(50, ErrorMessage = "O nome do gênero precisa ser menor que 50 caracteres")]
    public string Genre { get; set; }
}
