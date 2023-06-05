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
    [Route("api/tiposProductos")]
    [ApiController]
    public class TiposProductosController : ControllerBase
    {
        private readonly ITipoProductoRepositorio _tipoProductoRepositorio;
        private readonly IMapper _mapper;

        public TiposProductosController(ITipoProductoRepositorio tipoProductoRepositorio, IMapper mapper)
        {
            _tipoProductoRepositorio = tipoProductoRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTipoProductos()
        {
            var listaTipoProductos = _tipoProductoRepositorio.GetTipoProductos();

            var listaTipoProductosDto = new List<TipoProductosDto>();

            foreach (var lista in listaTipoProductos)
            {
                listaTipoProductosDto.Add(_mapper.Map<TipoProductosDto>(lista));
            }
            return Ok(listaTipoProductosDto);
        }

        [HttpGet("{tipoProductoId:int}", Name = "GetTipoProducto")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTipoProducto(int tipoProductoId)
        {
            var itemTipoProducto = _tipoProductoRepositorio.GetTipoProducto(tipoProductoId);

            if (itemTipoProducto == null)
                return NotFound();

            var itemTipoProductoDto = _mapper.Map<TipoProductosDto>(itemTipoProducto);

            return Ok(itemTipoProductoDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TipoProductosDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearTipoProducto([FromBody] TipoProductosDto crearTipoProductosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearTipoProductosDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_tipoProductoRepositorio.ExisteTipoProducto(crearTipoProductosDto.Descripcion))
            {
                ModelState.AddModelError("", "El TipoProducto ya existe");
                return StatusCode(404, ModelState);
            }

            var tipoProducto = _mapper.Map<TiposProductos>(crearTipoProductosDto);
            if (!_tipoProductoRepositorio.CrearTipoProducto(tipoProducto))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {tipoProducto.Descripcion}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTipoProducto", new { TipoProductoId = tipoProducto.TipoProductoID }, tipoProducto);

        }

        [HttpPatch("{tipoProductoId:int}", Name = "ActualizarPatchTipoProducto")]
        [ProducesResponseType(201, Type = typeof(TipoProductosDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchTipoProducto(int tipoProductoId, [FromBody] TipoProductosDto tipoProductosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tipoProductosDto == null || tipoProductoId != tipoProductosDto.TipoProductoID)
            {
                return BadRequest(ModelState);
            }

            var tipoProducto = _mapper.Map<TiposProductos>(tipoProductosDto);
            if (!_tipoProductoRepositorio.ActualizarTipoProducto(tipoProducto))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {tipoProducto.Descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{tipoProductoId:int}", Name = "BorrarTipoProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarTipoProducto(int TipoProductoId)
        {
            if (!_tipoProductoRepositorio.ExisteTipoProducto(TipoProductoId))
            {
                return NotFound();
            }

            var tipoProducto = _tipoProductoRepositorio.GetTipoProducto(TipoProductoId);
            if (!_tipoProductoRepositorio.BorrarTipoProducto(tipoProducto))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {tipoProducto.Descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
