using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models.Dtos
{
    public class UsuariosDto
    {
        public int UsuarioID { get; set; }

        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Nombre { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
