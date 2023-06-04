using AutoMapper;
using logisticsApi.Models.Dtos;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace logisticsApi.Controllers
{
    [Route("api/bodegas")]
    [ApiController]
    public class BodegasController : ControllerBase
    {
        private readonly IBodegaRepositorio _bodegaRepositorio;
        private readonly IMapper _mapper;

        public BodegasController(IBodegaRepositorio bodegaRepositorio, IMapper mapper)
        {
            _bodegaRepositorio = bodegaRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBodegas()
        {
            var listaBodegas = _bodegaRepositorio.GetBodegas();

            var listaBodegasDto = new List<BodegasDto>();

            foreach (var lista in listaBodegas)
            {
                listaBodegasDto.Add(_mapper.Map<BodegasDto>(lista));
            }
            return Ok(listaBodegasDto);
        }

        [HttpGet("{bodegaId:int}", Name = "GetBodega")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBodega(int bodegaId)
        {
            var itemBodega = _bodegaRepositorio.GetBodega(bodegaId);

            if (itemBodega == null)
                return NotFound();

            var itemBodegaDto = _mapper.Map<BodegasDto>(itemBodega);

            return Ok(itemBodegaDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BodegasDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearBodega([FromBody] BodegasDto crearBodegasDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearBodegasDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_bodegaRepositorio.ExisteBodega(crearBodegasDto.Nombre))
            {
                ModelState.AddModelError("", "La bodega ya existe");
                return StatusCode(404, ModelState);
            }

            var bodega = _mapper.Map<Bodegas>(crearBodegasDto);
            if (!_bodegaRepositorio.CrearBodega(bodega))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {bodega.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetBodega", new { BodegaId = bodega.BodegaID }, bodega);

        }

        [HttpPatch("{bodegaId:int}", Name = "ActualizarPatchBodega")]
        [ProducesResponseType(201, Type = typeof(BodegasDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchBodega(int bodegaId, [FromBody] BodegasDto bodegasDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (bodegasDto == null || bodegaId != bodegasDto.BodegaID)
            {
                return BadRequest(ModelState);
            }

            var bodega = _mapper.Map<Bodegas>(bodegasDto);
            if (!_bodegaRepositorio.ActualizarBodega(bodega))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {bodega.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{bodegaId:int}", Name = "BorrarBodega")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarBodega(int bodegaId)
        {
            if (!_bodegaRepositorio.ExisteBodega(bodegaId))
            {
                return NotFound();
            }

            var Bodega = _bodegaRepositorio.GetBodega(bodegaId);
            if (!_bodegaRepositorio.BorrarBodega(Bodega))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {Bodega.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
