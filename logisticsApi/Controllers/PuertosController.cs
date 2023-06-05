using AutoMapper;
using logisticsApi.Models.Dtos;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace logisticsApi.Controllers
{
    [Authorize]
    [Route("api/puertos")]
    [ApiController]
    public class PuertosController : ControllerBase
    {
        private readonly IPuertoRepositorio _puertoRepositorio;
        private readonly IMapper _mapper;

        public PuertosController(IPuertoRepositorio puertoRepositorio, IMapper mapper)
        {
            _puertoRepositorio = puertoRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPuertos()
        {
            var listaPuertos = _puertoRepositorio.GetPuertos();

            var listaPuertosDto = new List<PuertosDto>();

            foreach (var lista in listaPuertos)
            {
                listaPuertosDto.Add(_mapper.Map<PuertosDto>(lista));
            }
            return Ok(listaPuertosDto);
        }

        [HttpGet("{puertoId:int}", Name = "GetPuerto")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPuerto(int puertoId)
        {
            var itemPuerto = _puertoRepositorio.GetPuerto(puertoId);

            if (itemPuerto == null)
                return NotFound();

            var itemPuertoDto = _mapper.Map<PuertosDto>(itemPuerto);

            return Ok(itemPuertoDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PuertosDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearPuerto([FromBody] PuertosDto crearPuertosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearPuertosDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_puertoRepositorio.ExistePuerto(crearPuertosDto.Nombre))
            {
                ModelState.AddModelError("", "El Puerto ya existe");
                return StatusCode(404, ModelState);
            }

            var Puerto = _mapper.Map<Puertos>(crearPuertosDto);
            if (!_puertoRepositorio.CrearPuerto(Puerto))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {Puerto.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPuerto", new { PuertoId = Puerto.PuertoID }, Puerto);

        }

        [HttpPatch("{puertoId:int}", Name = "ActualizarPatchPuerto")]
        [ProducesResponseType(201, Type = typeof(PuertosDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchPuerto(int puertoId, [FromBody] PuertosDto puertosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (puertosDto == null || puertoId != puertosDto.PuertoID)
            {
                return BadRequest(ModelState);
            }

            var puerto = _mapper.Map<Puertos>(puertosDto);
            if (!_puertoRepositorio.ActualizarPuerto(puerto))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {puerto.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{puertoId:int}", Name = "BorrarPuerto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarPuerto(int puertoId)
        {
            if (!_puertoRepositorio.ExistePuerto(puertoId))
            {
                return NotFound();
            }

            var puerto = _puertoRepositorio.GetPuerto(puertoId);
            if (!_puertoRepositorio.BorrarPuerto(puerto))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {puerto.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
