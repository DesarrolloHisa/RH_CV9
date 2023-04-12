using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RH_CV.Models
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EmpleadoId { get; set; }
        public virtual Empleado? Empleado { get; set; }
        [Required(ErrorMessage = "El id es obligatorio")]
        public int TipoCargoId { get; set; }
        public virtual TipoCargo? TipoCargo { get; set; }
        [Required(ErrorMessage = "El Area Funcional es obligatorio")]
        public string AreaFuncional { get; set; }
        [Required(ErrorMessage = "El Salario es obligatorio")]
        public int Salario { get; set; }
        [Required(ErrorMessage = "El sexo es obligatorio")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "La EPS es obligatoria")]
        public int EPSId { get; set; }
        public virtual EPS? EPS { get; set; }
        public int? FondoPensionesId { get; set; }
        public virtual FondoPensiones? FondoPensiones { get; set; }
        public int? TipoContratoId { get; set; }
        public TipoContrato? TipoContrato { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string TiempoContratado { get; set; }
        public string RegistroMedico { get; set; }


        [Required(ErrorMessage = "La fecha de ingreso es obligatorio")]
        [Column(TypeName = "DATE")]
        public DateTime FechaIngreso { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? FechaRetiro { get; set; }
        public string? TiempoVinculacion { get; set; }
        public string? MotivoRetiro { get; set; }
        public string? Observaciones { get; set; }

    }
}
