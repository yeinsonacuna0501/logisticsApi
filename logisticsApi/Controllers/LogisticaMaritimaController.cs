using AutoMapper;
using logisticsApi.Models.Dtos;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using logisticsApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace logisticsApi.Controllers
{
    [Authorize]
    [Route("api/logisticaMaritima")]
    [ApiController]
    public class LogisticaMaritimaController : ControllerBase
    {
        private readonly ILogisticaMaritimaRepositorio _logisticaMaritimaRepositorio;
        private readonly IMapper _mapper;

        public LogisticaMaritimaController(ILogisticaMaritimaRepositorio logisticaMaritimaRepositorio, IMapper mapper)
        {
            _logisticaMaritimaRepositorio = logisticaMaritimaRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLogisticaMaritimas()
        {
            var listaLogisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritimas();

            var listaLogisticaMaritimaDto = new List<LogisticaMaritimaDto>();

            foreach (var lista in listaLogisticaMaritima)
            {
                listaLogisticaMaritimaDto.Add(_mapper.Map<LogisticaMaritimaDto>(lista));
            }
            return Ok(listaLogisticaMaritimaDto);
        }

        [HttpGet("{logisticaMaritimaId:int}", Name = "GetLogisticaMaritima")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLogisticaMaritima(int logisticaMaritimaId)
        {
            var itemLogisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritima(logisticaMaritimaId);

            if (itemLogisticaMaritima == null)
                return NotFound();

            var itemLogisticaMaritimaDto = _mapper.Map<LogisticaMaritimaDto>(itemLogisticaMaritima);

            return Ok(itemLogisticaMaritimaDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LogisticaMaritimaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearLogisticaMaritima([FromBody] LogisticaMaritimaDto crearLogisticaMaritimaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearLogisticaMaritimaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_logisticaMaritimaRepositorio.ExisteLogisticaMaritima(crearLogisticaMaritimaDto.NumeroGuia))
            {
                ModelState.AddModelError("", "La Logistica Maritima ya existe");
                return StatusCode(404, ModelState);
            }

            var LogisticaMaritima = _mapper.Map<LogisticaMaritima>(crearLogisticaMaritimaDto);
            if (!_logisticaMaritimaRepositorio.CrearLogisticaMaritima(LogisticaMaritima))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {LogisticaMaritima.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetLogisticaMaritima", new { LogisticaMaritimaId = LogisticaMaritima.LogisticaMaritimaID }, LogisticaMaritima);

        }

        [HttpPatch("{logisticaMaritimaId:int}", Name = "ActualizarPatchLogisticaMaritima")]
        [ProducesResponseType(201, Type = typeof(LogisticaMaritimaDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchLogisticaMaritima(int logisticaMaritimaId, [FromBody] LogisticaMaritimaDto logisticaMaritimaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (logisticaMaritimaDto == null || logisticaMaritimaId != logisticaMaritimaDto.LogisticaMaritimaID)
            {
                return BadRequest(ModelState);
            }

            var logisticaMaritima = _mapper.Map<LogisticaMaritima>(logisticaMaritimaDto);
            if (!_logisticaMaritimaRepositorio.ActualizarLogisticaMaritima(logisticaMaritima))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {logisticaMaritima.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{logisticaMaritimaId:int}", Name = "BorrarLogisticaMaritima")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarLogisticaMaritima(int logisticaMaritimaId)
        {
            if (!_logisticaMaritimaRepositorio.ExisteLogisticaMaritima(logisticaMaritimaId))
            {
                return NotFound();
            }

            var logisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritima(logisticaMaritimaId);
            if (!_logisticaMaritimaRepositorio.BorrarLogisticaMaritima(logisticaMaritima))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {logisticaMaritima.NumeroGuia}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpGet("GetLogisticaMaritimasPorCliente/{clienteId:int}")]
        public IActionResult GetLogisticaMaritimasPorCliente(int clienteId)
        {
            var listaLogisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritimasPorCliente(clienteId);

            if (listaLogisticaMaritima == null)
            {
                return NotFound();
            }

            var itemLogisticaMaritimaDto = new List<LogisticaMaritimaDto>();

            foreach (var item in listaLogisticaMaritima)
            {
                itemLogisticaMaritimaDto.Add(_mapper.Map<LogisticaMaritimaDto>(item));
            }
            return Ok(itemLogisticaMaritimaDto);
        }

        [HttpGet("GetLogisticaMaritimasPorPuerto/{puertoId:int}")]
        public IActionResult GetLogisticaMaritimasPorPuerto(int puertoId)
        {
            var listaLogisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritimasPorPuerto(puertoId);

            if (listaLogisticaMaritima == null)
            {
                return NotFound();
            }

            var itemLogisticaMaritimaDto = new List<LogisticaMaritimaDto>();

            foreach (var item in listaLogisticaMaritima)
            {
                itemLogisticaMaritimaDto.Add(_mapper.Map<LogisticaMaritimaDto>(item));
            }
            return Ok(itemLogisticaMaritimaDto);
        }

        [HttpGet("GetLogisticaMaritimasPorTipoProducto/{tipoProductoId:int}")]
        public IActionResult GetLogisticaMaritimasPorTipoProducto(int tipoProductoId)
        {
            var listaLogisticaMaritima = _logisticaMaritimaRepositorio.GetLogisticaMaritimasPorTipoProducto(tipoProductoId);

            if (listaLogisticaMaritima == null)
            {
                return NotFound();
            }

            var itemLogisticaMaritimaDto = new List<LogisticaMaritimaDto>();

            foreach (var item in listaLogisticaMaritima)
            {
                itemLogisticaMaritimaDto.Add(_mapper.Map<LogisticaMaritimaDto>(item));
            }
            return Ok(itemLogisticaMaritimaDto);
        }
    }
}
