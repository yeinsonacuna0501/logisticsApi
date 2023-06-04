using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface ILogisticaTerrestreRepositorio
    {
        ICollection<LogisticaTerrestre> GetLogisticaTerrestres();

        LogisticaTerrestre GetLogisticaTerrestre(int logisticaTerrestreId);

        bool ExisteLogisticaTerrestre(string nombre);
        bool ExisteLogisticaTerrestre(int id);

        bool CrearLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre);
        bool ActualizarLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre);
        bool BorrarLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre);
        ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorCliente(int clienteId);
        ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorTipoProducto(int tipoProductoId);
        ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorBodega(int bodegaId);

        bool Guardar();
    }
}
