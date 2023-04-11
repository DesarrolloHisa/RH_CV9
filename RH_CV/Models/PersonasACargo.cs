using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class PersonasACargo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Hijo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Conyugue { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Padres { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Otros { get; set; }
    }
}
