using System.ComponentModel.DataAnnotations;

namespace RH_CV.Models
{
    public class InfoDocumento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int TipoDocumentoId { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        [Required(ErrorMessage = "El país de expedición es obligatorio")]
        public string PaisExpedicion { get; set; }
        public string? MunicipioExpedicion { get; set; }
    }
}
