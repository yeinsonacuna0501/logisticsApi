using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;

namespace logisticsApi.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly LogisticsDbContext _context;

        public ClienteRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarCliente(Clientes cliente)
        {
            _context.clientes.Update(cliente);
            return Guardar();
        }

        public bool BorrarCliente(Clientes cliente)
        {
           _context.clientes.Remove(cliente);
            return Guardar();
        }

        public bool CrearCliente(Clientes cliente)
        {
            _context.clientes.Add(cliente);
            return Guardar();
        }

        public bool ExisteCliente(string nombre)
        {
            bool valor = _context.clientes.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteCliente(int id)
        {
            return _context.clientes.Any(c => c.ClienteID == id);
        }

        public Clientes GetCliente(int clienteId)
        {
            return _context.clientes.FirstOrDefault(c => c.ClienteID == clienteId);
        }

        public ICollection<Clientes> GetClientes()
        {
            return _context.clientes.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
