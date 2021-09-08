using Challenge.Contexts;
using Challenge.Entities;
using Challenge.Interfaces;
using Challenge.ViewModels.Personajes;
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
            List<Personaje> tmp = _personajesRepository.GetAllEntities();
            if (Personaje.name != null) tmp = tmp.Where(x => x.Nombre == Personaje.name).ToList();
            if (Personaje.age != 0) tmp = tmp.Where(x => x.Edad == Personaje.age).ToList();
            if (Personaje.idMovie != 0) tmp = tmp.Where(x => x.Peliculas_Series.FirstOrDefault(x => x.Id == Personaje.idMovie) != null).ToList();
            if (!tmp.Any()) return BadRequest();
            List<PersonajesResponseViewModel> PersonajeResponse = new();
            foreach (Personaje i in tmp)
            {
                PersonajeResponse.Add(new PersonajesResponseViewModel() { Imagen = i.Imagen, Nombre = i.Nombre });
            }
            return Ok(PersonajeResponse);
        }

        [Route("Detalles")]
        [HttpGet]
        public IActionResult Get([FromQuery] int id)
        {
            Personaje Personaje = _personajesRepository.GetPersonaje(id);
            if (Personaje == null) return BadRequest();
            PersonajesDetallesResponseViewModel P = new()
            {
                Id = Personaje.Id,
                Peliculas_Series = Personaje.Peliculas_Series.Any() ? Personaje.Peliculas_Series.Select(x => x.Titulo).ToList() : null,
                Edad = Personaje.Edad,
                Historia = Personaje.Historia,
                Imagen = Personaje.Imagen,
                Nombre = Personaje.Nombre,
                Peso = Personaje.Peso
            };
            return Ok(P);
        }

        [HttpPost]
        public IActionResult Post(PersonajesPostRequestViewModel Personaje)
        {
            _personajesRepository.Add(new Personaje()
            {
                Nombre = Personaje.Nombre,
                Edad = Personaje.Edad,
                Historia = Personaje.Historia,
                Imagen = Personaje.Imagen,
                Peso = Personaje.Peso
            });
            return Ok(Personaje);
        }

        [HttpPut]
        public IActionResult Put(PersonajesPutRequestViewModel Personaje)
        {
            if (_personajesRepository.GetAllEntities().FirstOrDefault(x => x.Id == Personaje.Id) == null) return BadRequest();
            return Ok(_personajesRepository.Update(new Personaje()
            {
                Id = Personaje.Id,
                Nombre = Personaje.Nombre,
                Edad = Personaje.Edad,
                Historia = Personaje.Historia,
                Imagen = Personaje.Imagen,
                Peso = Personaje.Peso
            }));
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
