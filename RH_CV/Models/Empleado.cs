using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El lugar de expedicion es obligatorio")]
        public string LugarExpedicion { get; set; }
        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatorio")]
        [Column(TypeName = "DATE")]
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();
    }
}
