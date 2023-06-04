using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logisticsApi.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteID { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Direccion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Ciudad { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Pais { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CorreoElectronico { get; set; }
    }
}
