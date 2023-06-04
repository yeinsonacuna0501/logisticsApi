using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;

namespace logisticsApi.Repositories
{
    public class BodegaRepositorio : IBodegaRepositorio
    {
        private readonly LogisticsDbContext _context;

        public BodegaRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarBodega(Bodegas bodega)
        {
            _context.bodegas.Update(bodega);
            return Guardar();
        }

        public bool BorrarBodega(Bodegas bodega)
        {
           _context.bodegas.Remove(bodega);
            return Guardar();
        }

        public bool CrearBodega(Bodegas bodega)
        {
            _context.bodegas.Add(bodega);
            return Guardar();
        }

        public bool ExisteBodega(string nombre)
        {
            bool valor = _context.bodegas.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteBodega(int id)
        {
            return _context.bodegas.Any(c => c.BodegaID == id);
        }

        public Bodegas GetBodega(int BodegaId)
        {
            return _context.bodegas.FirstOrDefault(c => c.BodegaID == BodegaId);
        }   

        public ICollection<Bodegas> GetBodegas()
        {
            return _context.bodegas.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
