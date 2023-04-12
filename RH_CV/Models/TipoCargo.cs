using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class TipoCargo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Cargo es obligatorio")]
        public string Tipo { get; set; }
    }
}
