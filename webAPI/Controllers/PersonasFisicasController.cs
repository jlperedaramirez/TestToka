#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models;
using webAPI.Helpers;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class PersonasFisicasController : ControllerBase
    {
        private readonly webAPIContext _context;
        PersonaFisicaData perFisica = new PersonaFisicaData();

        public PersonasFisicasController(webAPIContext context)
        {
            _context = context;
        }

        // GET: api/PersonasFisicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasFisicas>>> GetPersonasFisicasModel()
        {
            return await _context.PersonasFisicas.ToListAsync();
        }

        // GET: api/PersonasFisicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonasFisicas>> GetPersonasFisicas(int id)
        {
            var personasFisicas = await _context.PersonasFisicas.FindAsync(id);

            if (personasFisicas == null)
            {
                return NotFound();
            }

            return personasFisicas;
        }

        // PUT: api/PersonasFisicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonasFisicas(int id, PersonasFisicas personasFisicas)
        {
            if (id != personasFisicas.Id)
            {
                return BadRequest();
            }

            _context.Entry(personasFisicas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonasFisicasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PersonasFisicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonasFisicas>> PostPersonasFisicas(PersonasFisicas personasFisicas)
        {
            _context.PersonasFisicas.Add(personasFisicas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonasFisicas", new { id = personasFisicas.Id }, personasFisicas);
        }

        // DELETE: api/PersonasFisicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonasFisicas(int id)
        {
            var personasFisicas = await _context.PersonasFisicas.FindAsync(id);
            if (personasFisicas == null)
            {
                return NotFound();
            }

            _context.PersonasFisicas.Remove(personasFisicas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonasFisicasExists(int id)
        {
            return _context.PersonasFisicas.Any(e => e.Id == id);
        }

        // GET: api/PersonasFisicas
        // Lista de personas fisicas
        [HttpGet("lstPersonasFis")]
        public List<PersonasFisicas> listPersonasFisicasModel()
        {
            List <PersonasFisicas> list = perFisica.listaPersonasFisicas();
            return list;
        }

        // POST: api/buscarPersonasFis
        // Buscar personas por nombre, appellido paterno, apellido materno
        [HttpPost("buscarPersonasFis")]
        public List<PersonasFisicas> buscarPersonasFisicas(PersonasFisicas buscarPersona)
        {
            List<PersonasFisicas> list = perFisica.buscarPersonasFisicas(buscarPersona);
            return list;
        }

        // POST: api/guardarPersonasFis
        // Guarda persona fisica
        [HttpPost("guardarPersonasFis")]
        public IActionResult savePersonasFisicas(PersonasFisicas persona)
        {
            if(persona.Nombre.Equals(null))
            {
                return BadRequest(new { message = "El Nombre de la persona no puede ser null." });
            }
            else if (persona.ApellidoPaterno.Equals(null))
            {
                return BadRequest(new { message = "El Apellido Paterno de la persona no puede ser null." });
            }
            else if (persona.ApellidoMaterno.Equals(null))
            {
                return BadRequest(new { message = "El Apellido Materno de la persona no puede ser null." });
            } else
            {
                ObjRespuesta ret = perFisica.guardarPersonasFisicas(persona);
                if(ret.TipoRespuesta.Equals(Tipo.ERROR))
                {
                    return BadRequest(new { message = ret.Mensaje });
                } else
                {
                    return Ok(ret.Mensaje);
                }
                
            }
            
        }

        // DELETE: api/PersonasFisicas/5
        // Borrado logico de persona fisica
        [HttpDelete("borrarPersonaFis/{id}")]
        public IActionResult deletePersonasFisicas(int id)
        {
            if(id == null)
            {
                return BadRequest(new { message = "El id no puede ser null." });
            } else
            {
                ObjRespuesta ret = perFisica.borrarPersonasFisicas(id);
                if (ret.TipoRespuesta.Equals(Tipo.ERROR))
                {
                    return BadRequest(new { message = ret.Mensaje });
                }
                else
                {
                    return Ok(ret.Mensaje);
                }
            }
            
        }

        // PUT: api/updatePersonasFis
        [HttpPut("editarPersonasFis")]
        public IActionResult updatePersonasFisicas(PersonasFisicas persona)
        {
            if (persona.Nombre.Equals(null))
            {
                return BadRequest(new { message = "El Nombre de la persona no puede ser null." });
            }
            else if (persona.ApellidoPaterno.Equals(null))
            {
                return BadRequest(new { message = "El Apellido Paterno de la persona no puede ser null." });
            }
            else if (persona.ApellidoMaterno.Equals(null))
            {
                return BadRequest(new { message = "El Apellido Materno de la persona no puede ser null." });
            }
            else
            {
                ObjRespuesta ret = perFisica.editarPersonasFisicas(persona);
                if (ret.TipoRespuesta.Equals(Tipo.ERROR))
                {
                    return BadRequest(new { message = ret.Mensaje });
                }
                else
                {
                    return Ok(ret.Mensaje);
                }

            }
        }

        

        [HttpPut("activarPersonaFis/{id}")]
        public IActionResult activarPersonasFisicas(int id)
        {
            if (id == null)
            {
                return BadRequest(new { message = "El id no puede ser null." });
            }
            else
            {
                ObjRespuesta ret = perFisica.activarPersonasFisicas(id);
                if (ret.TipoRespuesta.Equals(Tipo.ERROR))
                {
                    return BadRequest(new { message = ret.Mensaje });
                }
                else
                {
                    return Ok(ret.Mensaje);
                }
            }
        }

        [HttpGet("buscarPersonaFis/{Id}")]
        public PersonasFisicas buscarPersonaFisica(int Id)
        {
            PersonasFisicas ret = perFisica.buscarPersonaFisica(Id);
            return ret;
        }
    }
}
