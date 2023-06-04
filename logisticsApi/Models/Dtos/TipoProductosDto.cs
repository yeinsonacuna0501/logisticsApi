using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models.Dtos
{
    public class TipoProductosDto
    {
        public int TipoProductoID { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }
    }
}
