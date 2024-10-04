using Microsoft.AspNetCore.Mvc;
using RepasoParcial.Models;
using RepasoParcial.Repositories;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepasoParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {

        private IPeliculaRepository _repository;
        public PeliculasController(IPeliculaRepository repository)
        {
            _repository = repository;
        }


        // GET: api/<PeliculasController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<PeliculasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var pelicula = _repository.GetById(id); 
                if (pelicula != null)
                {
                    return Ok(pelicula);
                }
                else
                {
                    return NotFound($"No existe una película con el id: [{id}]"); 
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<PeliculasController>/5
        [HttpGet("con filtros")]
        public IActionResult GetByNameCost(string? director, string? titulo)
        {
            var peliculas = _repository.GetByDirectorTitle(director, titulo);

            if (peliculas == null || !peliculas.Any())
            {
                return NotFound("No se encontraron peliculas que coincidan con los criterios.");
            }

            return Ok(peliculas);
        }


        // POST api/<PeliculasController>
        [HttpPost]
        public IActionResult Post([FromBody] Pelicula pelicula)
        {
            try
            {
                if (IsValid(pelicula))
                {
                    if (_repository.Create(pelicula)) ;
                    {
                        return Ok("Pelicula creada con exito");
                    }
                }
                else
                {
                    return BadRequest("Debe completar todos los campos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

            // PUT api/<PeliculasController>/5
            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] Pelicula peliculaActualizada)
            {
                {
                    try
                    {
                    if (IsValid(peliculaActualizada))
                    {
                        _repository.Update(peliculaActualizada);
                        return Ok($"Pelicula con id [{id}] actualizada con éxito");
                    }
                        else
                        {
                            return BadRequest("Datos no válidos");
                        }

                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error Interno: {ex.Message}");
                    }
                }
            }

            // DELETE api/<PeliculasController>/5
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                {
                    try
                    {
                        var peliculaDeleted = _repository.Delete(id);
                        if (peliculaDeleted)
                        {
                            return Ok("Servicio eliminado correctamente");
                        }
                        else
                        {
                            return NotFound($"No existe un servicio con el id: [{id}]");
                        }
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Ha ocurrido un error interno");
                    }
                }
            } 
        private bool IsValid(Pelicula pelicula)
        {
            return !string.IsNullOrEmpty(pelicula.Titulo) && !string.IsNullOrEmpty(pelicula.Director) && pelicula.Anio != 0 && pelicula.IdGenero > 0;
        }


    }

}
