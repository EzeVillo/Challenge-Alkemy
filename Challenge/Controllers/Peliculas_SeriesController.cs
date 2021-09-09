using Challenge.Contexts;
using Challenge.Entities;
using Challenge.Interfaces;
using Challenge.ViewModels.Peliculas_Series;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [ApiController]
    [Route(template: "/movies")]
    public class Peliculas_SeriesController : ControllerBase
    {
        private readonly IPeliculas_SeriesRepository _peliculas_seriesRepositoryRepository;
        private readonly IPersonajesRepository _personajesRepositoryRepository;
        private readonly IGenerosRepository _GenerosRepositoryRepository;
        public Peliculas_SeriesController(IPeliculas_SeriesRepository peliculas_seriesRepositoryRepository, IPersonajesRepository personajesRepository, IGenerosRepository generosRepository)
        {
            _peliculas_seriesRepositoryRepository = peliculas_seriesRepositoryRepository;
            _GenerosRepositoryRepository = generosRepository;
            _personajesRepositoryRepository = personajesRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Peliculas_SeriesGetRequestViewModel Pelicula)
        {
            if (Pelicula.Id != 0)
            {
                Pelicula_Serie PeliculaInterna = _peliculas_seriesRepositoryRepository.GetPelicula_Serie(Pelicula.Id);
                if (PeliculaInterna == null) return BadRequest();
                Peliculas_SeriesDetallesResponseViewModel p = new()
                {
                    Id = PeliculaInterna.Id,
                    Personajes = PeliculaInterna.Personajes.Any() ? PeliculaInterna.Personajes.Select(x => x.Nombre).ToList() : null,
                    Calificacion = PeliculaInterna.Calificacion,
                    FechaDeCreacion = PeliculaInterna.FechaDeCreacion,
                    Genero = PeliculaInterna.Genero != null ? PeliculaInterna.Genero.Nombre : null,
                    Imagen = PeliculaInterna.Imagen,
                    Titulo = PeliculaInterna.Titulo
                };
                return Ok(p);
            }
            else
            {
                IEnumerable<Pelicula_Serie> Peliculas = _peliculas_seriesRepositoryRepository.GetAllEntities();
                if (Pelicula.name != null) Peliculas = Peliculas.Where(x => x.Titulo == Pelicula.name).ToList();
                if (Pelicula.genre != 0) Peliculas = Peliculas.Where(x => x.Genero.Id == Pelicula.genre).ToList();
                Peliculas = Pelicula.order != "DESC" ? Peliculas.OrderBy(x => x.FechaDeCreacion) : Peliculas.OrderByDescending(x => x.FechaDeCreacion);
                if (!Peliculas.Any()) return BadRequest();
                List<Peliculas_SeriesResponseViewModel> PeliculaResponse = new();
                foreach (Pelicula_Serie i in Peliculas)
                {
                    PeliculaResponse.Add(new Peliculas_SeriesResponseViewModel() { Imagen = i.Imagen, Titulo = i.Titulo, FechaDeCreacion = i.FechaDeCreacion });
                }
                return Ok(PeliculaResponse);
            }
        }

        [HttpPost]
        public IActionResult Post(Peliculas_SeriesPostRequestViewModel Pelicula)
        {
            if (_peliculas_seriesRepositoryRepository.GetAllEntities().FirstOrDefault(x => x.Titulo == Pelicula.Titulo) != null) return BadRequest();
            Genero Genero = _GenerosRepositoryRepository.GetAllEntities().FirstOrDefault(x => x.Id == Pelicula.GeneroId);
            Pelicula_Serie p = new Pelicula_Serie()
            {
                Titulo = Pelicula.Titulo,
                Imagen = Pelicula.Imagen,
                FechaDeCreacion = Pelicula.FechaDeCreacion,
                Calificacion = Pelicula.Calificacion,
                Genero = Genero
            };
            _peliculas_seriesRepositoryRepository.Add(p);
            return Ok(Pelicula);
        }

        [HttpPut]
        public IActionResult Put(Peliculas_SeriesPutRequestViewModel Pelicula)
        {
            Pelicula_Serie PeliculaInterna = _peliculas_seriesRepositoryRepository.GetPelicula_Serie(Pelicula.Id);
            if (PeliculaInterna == null) return BadRequest();
            PeliculaInterna.Titulo = Pelicula.Titulo;
            PeliculaInterna.Imagen = Pelicula.Imagen;
            PeliculaInterna.FechaDeCreacion = Pelicula.FechaDeCreacion;
            PeliculaInterna.Calificacion = Pelicula.Calificacion;
            PeliculaInterna.Personajes.Clear();
            List<Personaje> Personajes = _personajesRepositoryRepository.GetAllEntities();
            foreach (int i in Pelicula.PersonajesId)
            {
                if (Personajes.Where(x => x.Id == i).Any()) PeliculaInterna.Personajes.Add(Personajes.FirstOrDefault(x => x.Id == i));
            }
            List<Genero> Generos = _GenerosRepositoryRepository.GetAllEntities();
            PeliculaInterna.Genero = Pelicula.GeneroId != 0 ? Generos.FirstOrDefault(x => x.Id == Pelicula.GeneroId) : null;
            _peliculas_seriesRepositoryRepository.Update(PeliculaInterna);
            return Ok(Pelicula);
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (_peliculas_seriesRepositoryRepository.GetAllEntities().FirstOrDefault(x => x.Id == Id) == null) return BadRequest();
            return Ok(_peliculas_seriesRepositoryRepository.Delete(Id));
        }
    }
}
