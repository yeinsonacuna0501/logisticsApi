using AutoMapper;
using logisticsApi.Models;
using logisticsApi.Models.Dtos;
using logisticsApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace logisticsApi.Controllers
{

    [Authorize]
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        //[AllowAnonymous] 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetClientes()
        {
            var listaClientes = _clienteRepositorio.GetClientes();

            var listaClientesDto =new List<ClientesDto>();

            foreach (var lista in listaClientes)
            {
                listaClientesDto.Add(_mapper.Map<ClientesDto>(lista));
            }
            return Ok(listaClientesDto);
        }

       // [AllowAnonymous]
        [HttpGet("{clienteId:int}", Name ="GetCliente")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCliente(int clienteId)
        {
            var itemCliente = _clienteRepositorio.GetCliente(clienteId);

            if(itemCliente == null)
                return NotFound();

            var itemClienteDto = _mapper.Map<ClientesDto>(itemCliente);
            
            return Ok(itemClienteDto);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClientesDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearCliente([FromBody] CrearClientesDto crearClientesDto)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           if(crearClientesDto == null)
            {
                return BadRequest(ModelState);
            }
           if(_clienteRepositorio.ExisteCliente(crearClientesDto.Nombre))
            {
                ModelState.AddModelError("", "El cliente ya existe");
                return StatusCode(404,ModelState);
            }

            var cliente = _mapper.Map<Clientes>(crearClientesDto);
            if (!_clienteRepositorio.CrearCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCliente", new { clienteId = cliente.ClienteID }, cliente);

        }

        [HttpPatch("{clienteId:int}", Name = "ActualizarPatchCliente")]
        [ProducesResponseType(201, Type = typeof(ClientesDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult ActualizarPatchCliente(int clienteId,[FromBody] ClientesDto ClientesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ClientesDto == null || clienteId != ClientesDto.ClienteID)
            {
                return BadRequest(ModelState);
            }

            var cliente = _mapper.Map<Clientes>(ClientesDto);
            if (!_clienteRepositorio.ActualizarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{clienteId:int}", Name = "BorrarCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarCliente(int clienteId)
        {
            if (!_clienteRepositorio.ExisteCliente(clienteId))
            {
                return NotFound();
            }

            var cliente = _clienteRepositorio.GetCliente(clienteId);
            if (!_clienteRepositorio.BorrarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
