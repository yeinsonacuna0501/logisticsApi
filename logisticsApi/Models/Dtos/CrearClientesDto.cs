using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace logisticsApi.Models.Dtos
{
    public class CrearClientesDto
    {
 
        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }    
        public string Ciudad { get; set; }
        public string Pais { get; set; }   
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
