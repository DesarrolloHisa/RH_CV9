using System.ComponentModel.DataAnnotations;

namespace RH_API.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
