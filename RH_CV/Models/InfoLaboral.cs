using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class InfoLaboral
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DatosPersonalesId { get; set; }
        public virtual DatosPersonales? DatosPersonales { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? MotivoRetiro { get; set; }
        public int? Celular { get; set; }
        public string? Cargo { get; set; }
    }
}
