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
        public Peliculas_SeriesController(IPeliculas_SeriesRepository peliculas_seriesRepositoryRepository)
        {
            _peliculas_seriesRepositoryRepository = peliculas_seriesRepositoryRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Peliculas_SeriesGetRequestViewModel Pelicula)
        {
            IEnumerable<Pelicula_Serie> tmp = _peliculas_seriesRepositoryRepository.GetAllEntities();
            if (Pelicula.name != null) tmp = tmp.Where(x => x.Titulo == Pelicula.name).ToList();
            if (Pelicula.genre != 0) tmp = tmp.Where(x => x.Genero.Id == Pelicula.genre).ToList();
            tmp = Pelicula.order != "DESC" ? tmp.OrderBy(x => x.FechaDeCreacion) : tmp.OrderByDescending(x => x.FechaDeCreacion);
            if (!tmp.Any()) return BadRequest();
            List<Peliculas_SeriesResponseViewModel> PersonajeResponse = new();
            foreach (Pelicula_Serie i in tmp)
            {
                PersonajeResponse.Add(new Peliculas_SeriesResponseViewModel() { Imagen = i.Imagen, Titulo = i.Titulo, Fecha = i.FechaDeCreacion });
            }
            return Ok(PersonajeResponse);
        }

        [Route("Detalles")]
        [HttpGet]
        public IActionResult Get([FromQuery] int id)
        {
            Pelicula_Serie Pelicula_Serie = _peliculas_seriesRepositoryRepository.GetPelicula_Serie(id);
            if (Pelicula_Serie == null) return BadRequest();
            Peliculas_SeriesDetallesResponseViewModel P = new()
            {
                Id = Pelicula_Serie.Id,
                Personajes = Pelicula_Serie.Personajes.Any() ? Pelicula_Serie.Personajes.Select(x => x.Nombre).ToList() : null,
                Calificacion = Pelicula_Serie.Calificacion,
                FechaDeCreacion = Pelicula_Serie.FechaDeCreacion,
                Genero = Pelicula_Serie.Genero != null ? Pelicula_Serie.Genero.Nombre : null,
                Imagen = Pelicula_Serie.Imagen,
                Titulo = Pelicula_Serie.Titulo
            };
            return Ok(P);
        }

        [HttpPost]
        public IActionResult Post(Peliculas_SeriesPostRequestViewModel Pelicula)
        {
            _peliculas_seriesRepositoryRepository.Add(new Pelicula_Serie()
            {
                Titulo = Pelicula.Titulo,
                Imagen = Pelicula.Imagen,
                FechaDeCreacion = Pelicula.FechaDeCreacion,
                Calificacion = Pelicula.Calificacion
            });
            return Ok(Pelicula);
        }

        [HttpPut]
        public IActionResult Put(Peliculas_SeriePutRequestViewModel Pelicula)
        {
            if (_peliculas_seriesRepositoryRepository.GetAllEntities().FirstOrDefault(x => x.Id == Pelicula.Id) == null) return BadRequest();
            return Ok(_peliculas_seriesRepositoryRepository.Update(new Pelicula_Serie()
            {
                Id = Pelicula.Id,
                Titulo = Pelicula.Titulo,
                Imagen = Pelicula.Imagen,
                FechaDeCreacion = Pelicula.FechaDeCreacion,
                Calificacion = Pelicula.Calificacion
            }));
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
