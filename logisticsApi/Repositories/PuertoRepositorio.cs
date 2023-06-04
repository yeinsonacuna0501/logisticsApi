using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;

namespace logisticsApi.Repositories
{
    public class PuertoRepositorio : IPuertoRepositorio
    {
        private readonly LogisticsDbContext _context;

        public PuertoRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarPuerto(Puertos puerto)
        {
            _context.puertos.Update(puerto);
            return Guardar();
        }

        public bool BorrarPuerto(Puertos puerto)
        {
           _context.puertos.Remove(puerto);
            return Guardar();
        }

        public bool CrearPuerto(Puertos puerto)
        {
            _context.puertos.Add(puerto);
            return Guardar();
        }

        public bool ExistePuerto(string nombre)
        {
            bool valor = _context.puertos.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExistePuerto(int id)
        {
            return _context.puertos.Any(c => c.PuertoID == id);
        }

        public Puertos GetPuerto(int PuertoId)
        {
            return _context.puertos.FirstOrDefault(c => c.PuertoID == PuertoId);
        }

        public ICollection<Puertos> GetPuertos()
        {
            return _context.puertos.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
