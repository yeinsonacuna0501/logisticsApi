using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace logisticsApi.Repositories
{
    public class LogisticaTerrestreRepositorio : ILogisticaTerrestreRepositorio
    {
        private readonly LogisticsDbContext _context;

        public LogisticaTerrestreRepositorio(LogisticsDbContext context)
        {
            _context = context;
        }

        public bool ActualizarLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre)
        {
            _context.logisticaTerrestre.Update(logisticaTerrestre);
            return Guardar();
        }

        public bool BorrarLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre)
        {
           _context.logisticaTerrestre.Remove(logisticaTerrestre);
            return Guardar();
        }

        public bool CrearLogisticaTerrestre(LogisticaTerrestre logisticaTerrestre)
        {
            _context.logisticaTerrestre.Add(logisticaTerrestre);
            return Guardar();
        }

        public bool ExisteLogisticaTerrestre(string numeroGuia)
        {
            bool valor = _context.logisticaTerrestre.Any(c => c.NumeroGuia.ToLower().Trim() == numeroGuia.ToLower().Trim());
            return valor;
        }

        public bool ExisteLogisticaTerrestre(int id)
        {
            return _context.logisticaTerrestre.Any(c => c.LogisticaTerrestreID == id);
        }

        public LogisticaTerrestre GetLogisticaTerrestre(int logisticaTerrestreId)
        {
            return _context.logisticaTerrestre.FirstOrDefault(c => c.LogisticaTerrestreID == logisticaTerrestreId);
        }

        public ICollection<LogisticaTerrestre> GetLogisticaTerrestres()
        {
            return _context.logisticaTerrestre.OrderBy(c => c.NumeroGuia).ToList();
        }

        public ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorCliente(int clienteId)
        {
            return _context.logisticaTerrestre.Include(cli => cli.clientes).Where(cli => cli.ClienteID == clienteId).ToList();
        }

        public ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorBodega(int bodegaId)
        {
            return _context.logisticaTerrestre.Include(bod => bod.bodegas).Where(bod => bod.BodegaID == bodegaId).ToList();
        }

        public ICollection<LogisticaTerrestre> GetLogisticaTerrestresPorTipoProducto(int tipoProductoId)
        {
            return _context.logisticaTerrestre.Include(tip => tip.tiposProductos).Where(tip => tip.TipoProductoID == tipoProductoId).ToList();
        }


        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }
    }
}
