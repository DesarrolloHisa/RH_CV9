using Microsoft.EntityFrameworkCore;
using RH_CV.Models;

namespace RH_CV.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //agregar modelo
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoContrato> TipoContrato { get; set; }
        public DbSet<TipoVinculo> TipoVinculo { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<EPS> EPS { get; set; }
        public DbSet<FondoCesantias> FondoCesantias { get; set; }
        public DbSet<FondoPensiones> FondoPensiones { get; set; }
        public DbSet<PersonasACargo> PersonasACargo { get; set; }
        public DbSet<Escolaridad> Escolaridad { get; set; }
        public DbSet<InfoDocumento> InfoDocumento { get; set; }
        public DbSet<InfoLaboral> InfoLaboral { get; set; }
        public DbSet<Practicas> Practicas { get; set; }
        public DbSet<ReferenciasFamiliares> ReferenciasFamiliares { get; set; }
        public DbSet<ReferenciasPersonales> ReferenciasPersonales { get; set; }
        public DbSet<ContactoEmergencia> ContactoEmergencia { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<DatosGenerales> DatosGenerales { get; set; }
        public DbSet<DatosFamiliares> DatosFamiliares { get; set; }
        public DbSet<DatosPersonales> DatosPersonales { get; set; }

        //Contratos
        public DbSet<TipoCargo> TipoCargo { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<DocenciaServicio> DocenciaServicio { get; set; }
        public DbSet<Voluntario> Voluntario { get; set; }
        public DbSet<Interdependencia> Interdependencia { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<TipoVinculacion> TipoVinculacion { get; set; }


    }
}
