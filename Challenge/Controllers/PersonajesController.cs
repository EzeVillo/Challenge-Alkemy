using Challenge.Contexts;
using Challenge.Entities;
using Challenge.Helpers;
using Challenge.Interfaces;
using Challenge.ViewModels.Personajes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [ApiController]
    [Route(template: "/characters")]

    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajesRepository _personajesRepository;
        private readonly IPeliculas_SeriesRepository _peliculas_SeriesRepository;
        public PersonajesController(IPersonajesRepository personajesRepository, IPeliculas_SeriesRepository peliculas_SeriesRepository)
        {
            _personajesRepository = personajesRepository;
            _peliculas_SeriesRepository = peliculas_SeriesRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PersonajesGetRequestViewModel Personaje)
        {
            QueryParameters<Personaje> Filter = new(Personaje.PageNumber, Personaje.PageSize);
            Filter.Where = x => x.Nombre == Personaje.name &&
            x.Edad == Personaje.age &&
            x.Peliculas_Series.FirstOrDefault(y => y.Id == Personaje.idMovie) != null;
            return Ok(_personajesRepository.FindBy(Filter));


            /*
            List<Personaje> Personajes = _personajesRepository.GetPersonajes();
            if (!Personajes.Any()) return BadRequest();
            List<PersonajesResponseViewModel> PersonajeResponse = new();
            foreach (Personaje i in Personajes)
            {
                PersonajeResponse.Add(new PersonajesResponseViewModel() { Imagen = i.Imagen, Nombre = i.Nombre });
            }
            var ret = PagedList<PersonajesResponseViewModel>.Create(PersonajeResponse, Personaje.PageNumber, Personaje.PageSize);
            if (!ret.Any()) return BadRequest();
            return Ok(ret);*/
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Get(int Id)
        {
            Personaje PersonajeInterno = _personajesRepository.GetPersonaje(Id);
            if (PersonajeInterno == null) return BadRequest();
            return Ok(new PersonajesDetallesResponseViewModel()
            {
                Id = PersonajeInterno.Id,
                Peliculas_Series = PersonajeInterno.Peliculas_Series.Any() ? PersonajeInterno.Peliculas_Series.Select(x => x.Titulo).ToList() : null,
                Edad = PersonajeInterno.Edad,
                Historia = PersonajeInterno.Historia,
                Imagen = PersonajeInterno.Imagen,
                Nombre = PersonajeInterno.Nombre,
                Peso = PersonajeInterno.Peso
            });
        }

        [HttpPost]
        public IActionResult Post(PersonajesPostRequestViewModel Personaje)
        {
            if (_personajesRepository.GetAllEntities().FirstOrDefault(x => x.Nombre == Personaje.Nombre) != null) return BadRequest();
            List<Pelicula_Serie> Peliculas = _peliculas_SeriesRepository.GetAllEntities();
            Personaje p = new Personaje()
            {
                Nombre = Personaje.Nombre,
                Edad = Personaje.Edad,
                Historia = Personaje.Historia,
                Imagen = Personaje.Imagen,
                Peso = Personaje.Peso
            };
            foreach (int i in Personaje.PeliculasId)
            {
                if (Peliculas.Where(x => x.Id == i).Any()) p.Peliculas_Series.Add(Peliculas.FirstOrDefault(x => x.Id == i));
            }
            _personajesRepository.Add(p);
            return Ok(Personaje);
        }

        [HttpPut]
        public IActionResult Put(PersonajesPutRequestViewModel Personaje)
        {
            Personaje PersonajeInterno = _personajesRepository.GetPersonaje(Personaje.Id);
            if (PersonajeInterno == null) return BadRequest();
            PersonajeInterno.Nombre = Personaje.Nombre;
            PersonajeInterno.Imagen = Personaje.Imagen;
            PersonajeInterno.Historia = Personaje.Historia;
            PersonajeInterno.Peso = Personaje.Peso;
            PersonajeInterno.Edad = Personaje.Edad;
            PersonajeInterno.Peliculas_Series.Clear();
            List<Pelicula_Serie> Peliculas = _peliculas_SeriesRepository.GetAllEntities();
            foreach (int i in Personaje.PeliculasId)
            {
                if (Peliculas.Where(x => x.Id == i).Any()) PersonajeInterno.Peliculas_Series.Add(Peliculas.FirstOrDefault(x => x.Id == i));
            }
            _personajesRepository.Update(PersonajeInterno);
            return Ok(_personajesRepository.Update(PersonajeInterno));
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (_personajesRepository.GetAllEntities().FirstOrDefault(x => x.Id == Id) == null) return BadRequest();
            return Ok(_personajesRepository.Delete(Id));
        }
    }
}
