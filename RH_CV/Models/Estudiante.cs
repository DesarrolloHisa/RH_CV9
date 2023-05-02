using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RH_CV.Models
{
    public class Estudiante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Documento { get; set; }
        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La fecha de ingreso es obligatorio")]
        [Column(TypeName = "DATE")]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El id es obligatorio")]
        public int TipoVinculacionId { get; set; }
        public virtual TipoVinculacion? TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El id es obligatorio")]
        public string TipoCargo { get; set; }
        //public virtual TipoCargo? TipoCargo { get; set; }
        public string Institucion { get; set; }
        public string AreaFuncional { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? FechaRetiro { get; set; }
        public string? MotivoRetiro { get; set; }
        public string? Observaciones { get; set; }
        public int Estado { get; set; }
    }
}
