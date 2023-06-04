using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace logisticsApi.Repositories
{
    public class LogisticaMaritimaRepositorio : ILogisticaMaritimaRepositorio
    {
        private readonly LogisticsDbContext _context;

        public LogisticaMaritimaRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarLogisticaMaritima(LogisticaMaritima logisticaMaritima)
        {
            _context.logisticaMaritima.Update(logisticaMaritima);
            return Guardar();
        }

        public bool BorrarLogisticaMaritima(LogisticaMaritima logisticaMaritima)
        {
           _context.logisticaMaritima.Remove(logisticaMaritima);
            return Guardar();
        }

        public bool CrearLogisticaMaritima(LogisticaMaritima logisticaMaritima)
        {
            _context.logisticaMaritima.Add(logisticaMaritima);
            return Guardar();
        }

        public bool ExisteLogisticaMaritima(string numeroGuia)
        {
            bool valor = _context.logisticaMaritima.Any(c => c.NumeroGuia.ToLower().Trim() == numeroGuia.ToLower().Trim());
            return valor;
        }

        public bool ExisteLogisticaMaritima(int id)
        {
            return _context.logisticaMaritima.Any(c => c.LogisticaMaritimaID == id);
        }

        public LogisticaMaritima GetLogisticaMaritima(int logisticaMaritimaId)
        {
            return _context.logisticaMaritima.FirstOrDefault(c => c.LogisticaMaritimaID == logisticaMaritimaId);
        }

        public ICollection<LogisticaMaritima> GetLogisticaMaritimas()
        {
            return _context.logisticaMaritima.OrderBy(c => c.NumeroGuia).ToList();
        }

        public ICollection<LogisticaMaritima> GetLogisticaMaritimasPorCliente(int clienteId)
        {
            return _context.logisticaMaritima.Include(cli => cli.clientes).Where(cli => cli.ClienteID == clienteId).ToList();
        }

        public ICollection<LogisticaMaritima> GetLogisticaMaritimasPorPuerto(int puertoId)
        {
            return _context.logisticaMaritima.Include(pue => pue.puertos).Where(pue => pue.PuertoID == puertoId).ToList();
        }

        public ICollection<LogisticaMaritima> GetLogisticaMaritimasPorTipoProducto(int tipoProductoId)
        {
            return _context.logisticaMaritima.Include(tip => tip.tiposProductos).Where(tip => tip.TipoProductoID == tipoProductoId).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
