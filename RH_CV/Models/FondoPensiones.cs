using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class FondoPensiones
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
