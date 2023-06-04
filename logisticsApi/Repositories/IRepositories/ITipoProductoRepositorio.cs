using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface ITipoProductoRepositorio
    {
        ICollection<TiposProductos> GetTipoProductos();

        TiposProductos GetTipoProducto(int tipoProductoId);

        bool ExisteTipoProducto(string nombre);
        bool ExisteTipoProducto(int id);

        bool CrearTipoProducto(TiposProductos tipoProducto);
        bool ActualizarTipoProducto(TiposProductos tipoProducto);
        bool BorrarTipoProducto(TiposProductos tipoProducto);

        bool Guardar();
    }
}
