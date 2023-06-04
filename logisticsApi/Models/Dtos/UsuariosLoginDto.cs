using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models.Dtos
{
    public class UsuariosLoginDto
    {

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]

        public string Password { get; set; }
    }
}
