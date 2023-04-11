using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class ContactoEmergencia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DatosPersonalesId { get; set; }
        public virtual DatosPersonales? DatosPersonales { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Parentesco { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Celular { get; set; }
    }
}
