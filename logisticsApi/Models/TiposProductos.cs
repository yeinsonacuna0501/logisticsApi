using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logisticsApi.Models
{
    public class TiposProductos
    {
        [Key]
        public int TipoProductoID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Descripcion { get; set; }
    }
}
