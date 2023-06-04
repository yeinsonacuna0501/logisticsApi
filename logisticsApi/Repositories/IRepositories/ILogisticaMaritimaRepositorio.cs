using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface ILogisticaMaritimaRepositorio
    {
        ICollection<LogisticaMaritima> GetLogisticaMaritimas();

        LogisticaMaritima GetLogisticaMaritima(int logisticaMaritimaId);

        bool ExisteLogisticaMaritima(string nombre);
        bool ExisteLogisticaMaritima(int id);

        bool CrearLogisticaMaritima(LogisticaMaritima logisticaMaritima);
        bool ActualizarLogisticaMaritima(LogisticaMaritima logisticaMaritima);
        bool BorrarLogisticaMaritima(LogisticaMaritima logisticaMaritima);

        ICollection<LogisticaMaritima> GetLogisticaMaritimasPorCliente(int clienteId);
        ICollection<LogisticaMaritima> GetLogisticaMaritimasPorTipoProducto(int tipoProductoId);
        ICollection<LogisticaMaritima> GetLogisticaMaritimasPorPuerto(int puertoId);

        bool Guardar();
    }
}
