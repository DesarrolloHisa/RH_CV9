using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RH_CV.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string User { get; set; }
        [Required]
        public int RolId { get; set; } //ForeignKey
        public Rol? Rol { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El tipo de vinculo es obligatorio")]
        public int TipoVinculoId { get; set; }
        public TipoVinculo? TipoVinculo { get; set; }
        public int? TipoContratoId { get; set; }
        public TipoContrato? TipoContrato { get; set; }
        [Required(ErrorMessage = "La información del documento es obligatorio")]
        public int InfoDocumentoId { get; set; } //ForeignKey
        public InfoDocumento? InfoDocumento { get; set; }
        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio")]
        public int Estado { get; set; }
    }
}
