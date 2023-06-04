using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logisticsApi.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string NombreUsuario { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre{ get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
