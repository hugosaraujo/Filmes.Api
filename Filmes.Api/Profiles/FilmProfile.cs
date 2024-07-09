using AutoMapper;
using Filmes.Api.Data.DTOs;
using Filmes.Api.Models;

namespace Filmes.Api.Profiles;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<CreateFilmDTO, Film>();
        CreateMap<UpdateFilmDTO, Film>();
        CreateMap<Film, UpdateFilmDTO>();
        CreateMap<Film, ReadFilmDTO>();
    }
}
