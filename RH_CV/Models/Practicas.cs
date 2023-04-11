using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RH_CV.Models
{
    public class Practicas
    {
        [Key]
        public int Id { get; set; }
        public string? Institucion { get; set; }
        public string? Programa { get; set; }
        public string? Titulo { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? FechaFinalizacion { get; set; }
        public string? DocenciaServicios { get; set; }
    }
}
