using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface IClienteRepositorio
    {
        ICollection<Clientes> GetClientes();

        Clientes GetCliente(int clienteId);

        bool ExisteCliente(string nombre);
        bool ExisteCliente(int id);

        bool CrearCliente(Clientes cliente);
        bool ActualizarCliente(Clientes cliente);
        bool BorrarCliente(Clientes cliente);

        bool Guardar();
    }
}
