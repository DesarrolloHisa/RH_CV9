using RH_CV.Models;

namespace RH_CV.Services.Contract
{
    public interface IUserService
    {
        Task<Usuario> GetUsuario(string user, string password);
        Task<Usuario> SaveUsuario(Usuario modelo);
        Task<Empleado> SaveEmpleado(Empleado empleado);
        Task<Estudiante> SaveStudent(Estudiante estudiante);
        Task<DocenciaServicio> SaveTeachingService(DocenciaServicio docenciaServicio);
        Task<Voluntario> SaveVolunteer(Voluntario voluntario);
        Task<Interdependencia> SaveInterdependence(Interdependencia interdependencia);
        Task<Contrato> SaveContract(Contrato contrato);
        //Task<DatosPersonales> SaveHV(DatosPersonales modelo/*, string? tipoDocumento, string? paisExpedicion, string? municipioExpedicion, int hijo, int conyugue, int padres, int otros*/);
    }
}
