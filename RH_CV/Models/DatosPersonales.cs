using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace RH_CV.Models
{
    public class DatosPersonales
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string UsuarioId { get; set; } //ForeignKey
        public Usuario? Usuario { get; set; }
        //[Required(ErrorMessage = "El tipo de documento es obligatorio")]
        //public string TipoDocumento { get; set; } //ForeignKey
        //[Required(ErrorMessage = "El tipo de vinculo es obligatorio")]
        //public int TipoVinculoId { get; set; }
        //public TipoVinculo? TipoVinculo { get; set; }
        //[Required(ErrorMessage = "El tipo de contrato es obligatorio")]
        //public int TipoContratoId { get; set; }
        //public TipoContrato? TipoContrato { get; set; }
        public int? LibretaMilitar { get; set; }
        //[Required(ErrorMessage = "La información del documento es obligatorio")]
        //public int InfoDocumentoId { get; set; } //ForeignKey
        //public InfoDocumento? InfoDocumento { get; set; }
        //public List<DatosFamiliares> Familiares { get; set; }
        //[Required(ErrorMessage = "El primer nombre es obligatorio")]
        //public string PrimerNombre { get; set; }
        //public string? SegundoNombre { get; set; }
        //[Required(ErrorMessage = "El primer apellido es obligatorio")]
        //public string PrimerApellido { get; set; }
        //public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatorio")]
        [Column(TypeName = "DATE")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El país de nacimiento es obligatorio")]
        public string PaisNacimiento { get; set; }
        public string? MunicipioNacimiento { get; set; }
        [Required(ErrorMessage = "El celular es obligatorio")]
        public int Celular { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El sexo es obligatorio")]
        public string Sexo { get; set; }
        //[Required(ErrorMessage = "La dirección es obligatoria")]
        //public string Direccion { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        public int DireccionId { get; set; }
        public virtual Direccion? Direccion { get; set; }
        [Required(ErrorMessage = "El municipio de residencia es obligatorio")]
        public string MunicipioResidencia { get; set; }
        [Required(ErrorMessage = "El estrato es obligatorio")]
        public int Estrato { get; set; }

        [Required(ErrorMessage = "Con quién vive es obligatorio")]
        public string ViveCon { get; set; }
        [Required(ErrorMessage = "El grupo étnico es obligatorio")]
        public string GrupoEtnico { get; set; }
        [Required(ErrorMessage = "Las personas a cargo es obligatorio")]
        public int PersonasACargoId { get; set; } //Foreign Key
        public PersonasACargo? PersonasACargo { get; set; }
        [Required(ErrorMessage = "El estado civil es obligatorio")]
        public string EstadoCivil { get; set; }
        [Required(ErrorMessage = "La EPS es obligatoria")]
        public int EPSId { get; set; }
        public EPS? EPS { get; set; }
        public int? FondoPensionesId { get; set; }
        public FondoPensiones? FondoPensiones { get; set; }
        public int? FondoCesantiasId { get; set; }
        public FondoCesantias? FondoCesantias { get; set; }

        //public string Eps { get; set; }
        //public string? FondoPensiones { get; set; }
        //public string? FondoCesantias { get; set; }
        //[Required]
        //public int ReferenciasPersonalesId { get; set; }
        //public ReferenciasPersonales? ReferenciasPersonales { get; set; }
        //[Required]
        //public int ReferenciasFamiliaresId { get; set; }
        //public ReferenciasFamiliares? ReferenciasFamiliares { get; set; }
        public int? DatosGeneralesId { get; set; }
        public DatosGenerales? DatosGenerales { get; set; }
        public int? PracticasId { get; set; }
        public Practicas? Practicas { get; set; }
        [Required]
        [Column(TypeName = "DATE")]
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<InfoLaboral> InfoLaboral { get; set; } = new List<InfoLaboral>();
        public virtual ICollection<Escolaridad> Escolaridad { get; set; } = new List<Escolaridad>();
        public virtual ICollection<ReferenciasPersonales> ReferenciasPersonales { get; set; } = new List<ReferenciasPersonales>();
        public virtual ICollection<ReferenciasFamiliares> ReferenciasFamiliares { get; set; } = new List<ReferenciasFamiliares>();
        public virtual ICollection<DatosFamiliares> DatosFamiliares { get; set; } = new List<DatosFamiliares>();
        public virtual ICollection<ContactoEmergencia> ContactoEmergencia { get; set; } = new List<ContactoEmergencia>();
    }
}
