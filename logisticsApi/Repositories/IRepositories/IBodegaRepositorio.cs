using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface IBodegaRepositorio
    {
        ICollection<Bodegas> GetBodegas();

        Bodegas GetBodega(int bodegaId);

        bool ExisteBodega(string nombre);
        bool ExisteBodega(int id);

        bool CrearBodega(Bodegas bodega);
        bool ActualizarBodega(Bodegas bodega);
        bool BorrarBodega(Bodegas bodega);

        bool Guardar();
    }
}
