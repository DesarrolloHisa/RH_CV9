using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class DatosGenerales
    {
        [Key]
        public int Id { get; set; }
        public string? ComoSupo { get; set; }
        public string? OtrosIngresos { get; set; }
        public int? Ingreso { get; set; }
        public string? ParientesTrabajando { get; set; }
        public string? TipoVivienda { get; set; }
    }
}
