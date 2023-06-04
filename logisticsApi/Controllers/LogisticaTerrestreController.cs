using AutoMapper;
using logisticsApi.Models.Dtos;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace logisticsApi.Controllers
{
    [Route("api/logisticaTerrestre")]
    [ApiController]
    public class LogisticaTerrestreController : ControllerBase
    {
        private readonly ILogisticaTerrestreRepositorio _logisticaTerrestreRepositorio;
        private readonly IMapper _mapper;

        public LogisticaTerrestreController(ILogisticaTerrestreRepositorio logisticaTerrestreRepositorio, IMapper mapper)
        {
            _logisticaTerrestreRepositorio = logisticaTerrestreRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLogisticaTerrestres()
        {
            var listaLogisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestres();

            var listaLogisticaTerrestreDto = new List<LogisticaTerrestreDto>();

            foreach (var lista in listaLogisticaTerrestre)
            {
                listaLogisticaTerrestreDto.Add(_mapper.Map<LogisticaTerrestreDto>(lista));
            }
            return Ok(listaLogisticaTerrestreDto);
        }

        [HttpGet("{logisticaTerrestreId:int}", Name = "GetLogisticaTerrestre")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLogisticaTerrestre(int logisticaTerrestreId)
        {
            var itemLogisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestre(logisticaTerrestreId);

            if (itemLogisticaTerrestre == null)
                return NotFound();

            var itemLogisticaTerrestreDto = _mapper.Map<LogisticaTerrestreDto>(itemLogisticaTerrestre);

            return Ok(itemLogisticaTerrestreDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LogisticaTerrestreDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearLogisticaTerrestre([FromBody] LogisticaTerrestreDto crearLogisticaTerrestreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearLogisticaTerrestreDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_logisticaTerrestreRepositorio.ExisteLogisticaTerrestre(crearLogisticaTerrestreDto.NumeroGuia))
            {
                ModelState.AddModelError("", "La Logistica terrestre ya existe");
                return StatusCode(404, ModelState);
            }

            var logisticaTerrestre = _mapper.Map<LogisticaTerrestre>(crearLogisticaTerrestreDto);
            if (!_logisticaTerrestreRepositorio.CrearLogisticaTerrestre(logisticaTerrestre))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {logisticaTerrestre.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetLogisticaTerrestre", new { LogisticaTerrestreId = logisticaTerrestre.LogisticaTerrestreID }, logisticaTerrestre);

        }

        [HttpPatch("{logisticaTerrestreId:int}", Name = "ActualizarPatchLogisticaTerrestre")]
        [ProducesResponseType(201, Type = typeof(LogisticaTerrestreDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchLogisticaTerrestre(int logisticaTerrestreId, [FromBody] LogisticaTerrestreDto logisticaTerrestreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (logisticaTerrestreDto == null || logisticaTerrestreId != logisticaTerrestreDto.LogisticaTerrestreID)
            {
                return BadRequest(ModelState);
            }

            var LogisticaTerrestre = _mapper.Map<LogisticaTerrestre>(logisticaTerrestreDto);
            if (!_logisticaTerrestreRepositorio.ActualizarLogisticaTerrestre(LogisticaTerrestre))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {LogisticaTerrestre.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{logisticaTerrestreId:int}", Name = "BorrarLogisticaTerrestre")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarLogisticaTerrestre(int logisticaTerrestreId)
        {
            if (!_logisticaTerrestreRepositorio.ExisteLogisticaTerrestre(logisticaTerrestreId))
            {
                return NotFound();
            }

            var logisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestre(logisticaTerrestreId);
            if (!_logisticaTerrestreRepositorio.BorrarLogisticaTerrestre(logisticaTerrestre))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {logisticaTerrestre.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpGet("GetLogisticaTerrestresPorCliente/{clienteId:int}")]
        public IActionResult GetLogisticaTerrestresPorCliente(int clienteId)
        {
            var listaLogisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestresPorCliente(clienteId);

            if(listaLogisticaTerrestre == null)
            {
                return NotFound();
            }

            var itemLogisticaTerrestreDto = new List<LogisticaTerrestreDto>();

            foreach (var item in listaLogisticaTerrestre)
            {
                itemLogisticaTerrestreDto.Add(_mapper.Map<LogisticaTerrestreDto>(item));
            }
            return Ok(itemLogisticaTerrestreDto);
        }

        [HttpGet("GetLogisticaTerrestresPorBodega/{bodegaId:int}")]
        public IActionResult GetLogisticaTerrestresPorBodega(int bodegaId)
        {
            var listaLogisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestresPorBodega(bodegaId);

            if (listaLogisticaTerrestre == null)
            {
                return NotFound();
            }

            var itemLogisticaTerrestreDto = new List<LogisticaTerrestreDto>();

            foreach (var item in listaLogisticaTerrestre)
            {
                itemLogisticaTerrestreDto.Add(_mapper.Map<LogisticaTerrestreDto>(item));
            }
            return Ok(itemLogisticaTerrestreDto);
        }

        [HttpGet("GetLogisticaTerrestresPorTipoProducto/{tipoProductoId:int}")]
        public IActionResult GetLogisticaTerrestresPorTipoProducto(int tipoProductoId)
        {
            var listaLogisticaTerrestre = _logisticaTerrestreRepositorio.GetLogisticaTerrestresPorTipoProducto(tipoProductoId);

            if (listaLogisticaTerrestre == null)
            {
                return NotFound();
            }

            var itemLogisticaTerrestreDto = new List<LogisticaTerrestreDto>();

            foreach (var item in listaLogisticaTerrestre)
            {
                itemLogisticaTerrestreDto.Add(_mapper.Map<LogisticaTerrestreDto>(item));
            }
            return Ok(itemLogisticaTerrestreDto);
        }
    }
}
