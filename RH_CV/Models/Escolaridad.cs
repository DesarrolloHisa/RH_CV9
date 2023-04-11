using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class Escolaridad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DatosPersonalesId { get; set; }
        public virtual DatosPersonales? DatosPersonales { get; set; }
        [Required(ErrorMessage = "El grado es obligatorio")]
        public string Grado { get; set; }
        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La institución es obligatoria")]
        public string Institucion { get; set; }
        [Required(ErrorMessage = "El año es obligatorio")]
        public int Year { get; set; }
    }
}
