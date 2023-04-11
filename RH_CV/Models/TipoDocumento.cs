using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
