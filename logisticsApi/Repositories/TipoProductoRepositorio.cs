using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;

namespace logisticsApi.Repositories
{
    public class TipoProductoRepositorio : ITipoProductoRepositorio
    {
        private readonly LogisticsDbContext _context;

        public TipoProductoRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarTipoProducto(TiposProductos tipoProducto)
        {
            _context.tiposProductos.Update(tipoProducto);
            return Guardar();
        }

        public bool BorrarTipoProducto(TiposProductos tipoProducto)
        {
           _context.tiposProductos.Remove(tipoProducto);
            return Guardar();
        }

        public bool CrearTipoProducto(TiposProductos tipoProducto)
        {
            _context.tiposProductos.Add(tipoProducto);
            return Guardar();
        }

        public bool ExisteTipoProducto(string descripcion)
        {
            bool valor = _context.tiposProductos.Any(c => c.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim());
            return valor;
        }

        public bool ExisteTipoProducto(int id)
        {
            return _context.tiposProductos.Any(c => c.TipoProductoID == id);
        }

        public TiposProductos GetTipoProducto(int TipoProductoId)
        {
            return _context.tiposProductos.FirstOrDefault(c => c.TipoProductoID == TipoProductoId);
        }

        public ICollection<TiposProductos> GetTipoProductos()
        {
            return _context.tiposProductos.OrderBy(c => c.Descripcion).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
