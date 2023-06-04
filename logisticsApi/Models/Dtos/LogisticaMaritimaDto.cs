using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logisticsApi.Models.Dtos
{
    public class LogisticaMaritimaDto
    {
        public int LogisticaMaritimaID { get; set; }

        public int ClienteID { get; set; }

        public int TipoProductoID { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es obligatoria")]
        public int CantidadProducto { get; set; }

        [Required(ErrorMessage = "La fecha de registro es obligatoria")]
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria")]
        public DateTime FechaEntrega { get; set; }

        public int PuertoID { get; set; }

        [Required(ErrorMessage = "El precio de envio es obligatorio")]
        public decimal PrecioEnvio { get; set; }

        [Required(ErrorMessage = "El Numero de flota es obligatorio")]
        public string NumeroFlota { get; set; }

        [Required(ErrorMessage = "El Numero de guia es obligatorio")]
        public string NumeroGuia { get; set; }
    }
}
