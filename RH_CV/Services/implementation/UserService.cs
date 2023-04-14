using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;

namespace RH_CV.Services.implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string user, string password)
        {
            Usuario usuario_encontrado = await _dbContext.Usuario.Where(u => u.User == user && u.Password == password).FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            var infoDocumento = new InfoDocumento
            {
                TipoDocumento = modelo.InfoDocumento.TipoDocumento,
                PaisExpedicion = modelo.InfoDocumento.PaisExpedicion,
                MunicipioExpedicion = modelo.InfoDocumento.MunicipioExpedicion
            };

            modelo.InfoDocumentoId = infoDocumento.Id;

            _dbContext.Usuario.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }

        public async Task<Usuario> GetEmpleado(string user, string password)
        {
            Usuario usuario_encontrado = await _dbContext.Usuario.Where(u => u.User == user && u.Password == password).FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Empleado> SaveEmpleado(Empleado empleado)
        {
            _dbContext.Empleado.Add(empleado);
            await _dbContext.SaveChangesAsync();
            return empleado;
        }

        public async Task<Contrato> SaveContract(Contrato contrato)
        {
            _dbContext.Contrato.Add(contrato);
            await _dbContext.SaveChangesAsync();
            return contrato;
        }

        //public async Task<DatosPersonales> SaveHV(DatosPersonales modelo/*, string? tipoDocumento, string? paisExpedicion, string? municipioExpedicion, int hijo, int conyugue, int padres, int otros*/)
        //{
        //    //var infoDocumento = new InfoDocumento
        //    //{
        //    //    TipoDocumento = tipoDocumento,
        //    //    PaisExpedicion = paisExpedicion,
        //    //    MunicipioExpedicion = municipioExpedicion
        //    //};

        //    //// Crear una instancia de PersonasACargo y asignar los valores ingresados en el formulario
        //    //var personasACargo = new PersonasACargo
        //    //{
        //    //    Hijo = hijo,
        //    //    Conyugue = conyugue,
        //    //    Padres = padres,
        //    //    Otros = otros
        //    //};

        //    //// Guardar los cambios en la base de datos
        //    //await _dbContext.SaveChangesAsync();


        //    //// Guardar la instancia de PersonasACargo en la base de datos
        //    //_dbContext.PersonasACargo.Add(personasACargo);
        //    //_dbContext.SaveChanges();

        //    //// Asignar el ID de la instancia de PersonasACargo a la propiedad PersonasACargoId de la instancia de DatosPersonales
        //    //modelo.InfoDocumentoId = infoDocumento.Id;
        //    //modelo.PersonasACargoId = personasACargo.Id;

        //    //_dbContext.DatosPersonales.Add(modelo);
        //    //await _dbContext.SaveChangesAsync();
        //    //return modelo;

        //    //// Convertir la lista de objetos DatosFamiliares en formato JSON en una lista de objetos DatosFamiliares
        //    //var datosFamiliaresList = JsonConvert.DeserializeObject<List<DatosFamiliares>>(datosFamiliaresJSON);

        //    var infoDocumento = new InfoDocumento
        //    {
        //        TipoDocumento = modelo.InfoDocumento.TipoDocumento,
        //        PaisExpedicion = modelo.InfoDocumento.PaisExpedicion,
        //        MunicipioExpedicion = modelo.InfoDocumento.MunicipioExpedicion
        //    };

        //    // Crear una instancia de PersonasACargo y asignar los valores ingresados en el formulario
        //    var personasACargo = new PersonasACargo
        //    {
        //        Hijo = modelo.PersonasACargo.Hijo,
        //        Conyugue = modelo.PersonasACargo.Conyugue,
        //        Padres = modelo.PersonasACargo.Padres,
        //        Otros = modelo.PersonasACargo.Otros
        //    };

        //    // Agregar instancia de InfoDocumento y PersonasACargo al contexto de base de datos
        //    _dbContext.InfoDocumento.Add(infoDocumento);
        //    _dbContext.PersonasACargo.Add(personasACargo);

        //    // Asignar el ID de la instancia de PersonasACargo a la propiedad PersonasACargoId de la instancia de DatosPersonales
        //    modelo.InfoDocumentoId = infoDocumento.Id;
        //    modelo.PersonasACargoId = personasACargo.Id;

        //    // Agregar instancia de DatosPersonales al contexto de base de datos
        //    _dbContext.DatosPersonales.Add(modelo);

        //    // Guardar los cambios en la base de datos

        //    //await _dbContext.SaveChangesAsync();*****

        //    //// Asignar el id de DatosPersonales a cada objeto DatosFamiliares
        //    //foreach (var datoFamiliar in datosFamiliaresList)
        //    //{
        //    //    datoFamiliar.DatosPersonalesId = modelo.Documento;
        //    //}
        //    //// Guardar la lista de objetos DatosFamiliares en la base de datos
        //    //_dbContext.DatosFamiliares.AddRange(datosFamiliaresList);
        //    //await _dbContext.SaveChangesAsync();

        //    return modelo;
        //}
    }
}
