using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models.Dtos
{
    public class UsuariosLoginRespuestaDto
    {

        public Usuarios Usuarios { get; set; }

        public string Token { get; set; }
    }
}
