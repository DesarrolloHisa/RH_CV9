using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class DatosFamiliares
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DatosPersonalesId { get; set; }
        public virtual DatosPersonales? DatosPersonales { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Parentesco { get; set; }
        public string? Ocupacion { get; set; }
    }
}
