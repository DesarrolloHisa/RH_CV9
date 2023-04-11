using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class Direccion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Num1 { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Num2 { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Num3 { get; set; }
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string DireccionCompleta { get; set; }
    }
}
