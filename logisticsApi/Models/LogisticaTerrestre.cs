using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models
{
    public class LogisticaTerrestre
    {
        [Key]
        public int LogisticaTerrestreID { get; set; }

        public int ClienteID { get; set; }

        [ForeignKey("ClienteID")]
        public Clientes clientes { get; set; }

        public int TipoProductoID { get; set; }

        [ForeignKey("TipoProductoID")]
        public TiposProductos tiposProductos { get; set; }

        public int CantidadProducto { get; set; }

        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEntrega { get; set; }

        public int BodegaID { get; set; }

        [ForeignKey("BodegaID")]
        public Bodegas bodegas { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioEnvio { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string NumeroFlota { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string NumeroGuia { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Descuento { get; set; }
    }
}
