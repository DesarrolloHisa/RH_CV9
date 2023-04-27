using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using System.Web;

namespace RH_CV.Controllers
{
    //[Authorize(Policy = "RolPolicy")]
    [Authorize]
    public class ManageHVController : Controller
    {
        private readonly IUserService _userService;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _contexto;
        public ManageHVController(IUserService userService, ApplicationDbContext contexto/*, UserManager<IdentityUser> userManager*/)
        {
            _contexto = contexto;
            _userService = userService;
            //_userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyHV()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            string idUser = "";

            //ViewData["RolTipo"] = rol.Tipo;
            //claimsUser.Claims.Where(c=> c.Type == ClaimTypes.)

            if (claimsUser.Identity.IsAuthenticated)
            {
                idUser = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            //var hojasDeVida = await _contexto.DatosPersonales.Where(hv => hv.Id == id).ToListAsync();
            //return View(hojasDeVida);
            return View(await _contexto.DatosPersonales.Include(hv => hv.Usuario).Where(hv => hv.UsuarioId == idUser).ToListAsync());
        }

        //MostrarHV
        [HttpGet]
        public async Task<IActionResult> AllHV()
        {
            // Obtener el ID del usuario actualmente autenticado
            //string userId = User.Identity.GetUserId();
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                return View(await _contexto.DatosPersonales.Include(x => x.Usuario).ToListAsync());
            }

            //ClaimsPrincipal claimsUser = HttpContext.User;
            //string userId = "";

            //if (claimsUser.Identity.IsAuthenticated)
            //{
            //    userId = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
            //            .Select(c => c.Value).SingleOrDefault();
            //    // Obtener el rol del usuario
            //    Rol rol = _contexto.Usuario.Where(u => u.User == userId).Select(u => u.Rol).FirstOrDefault();

            //    // Verificar si el usuario es un administrador
            //    if (rol != null && rol.Id == 1 || rol != null && rol.Id == 2)
            //    {
            //        return View(await _contexto.DatosPersonales.ToListAsync());
            //    }
            //    else
            //    {
            //        return RedirectToAction("AccessDenied", "Home");
            //    }
            //}
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //CrearHV
        public IActionResult CreateHV()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            string idUser = "";
            string primerNombre = "";
            string segundoNombre = "";
            string primerApellido = "";
            string segundoApellido = "";

            //ViewData["RolTipo"] = rol.Tipo;
            //claimsUser.Claims.Where(c=> c.Type == ClaimTypes.)

            if (claimsUser.Identity.IsAuthenticated)
            {
                idUser = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

                ViewData["idUser"] = idUser;

                var usuario = _contexto.Usuario.Find(idUser);
                if (usuario == null)
                {
                    return NotFound();
                }

                primerNombre = usuario.PrimerNombre;
                ViewData["primerNombre"] = primerNombre;

                segundoNombre = usuario.SegundoNombre;
                if(segundoNombre == null)
                {
                    segundoNombre = "ㅤ";
                }
                ViewData["segundoNombre"] = segundoNombre;

                primerApellido = usuario.PrimerApellido;
                ViewData["primerApellido"] = primerApellido;

                segundoApellido = usuario.SegundoApellido;
                if(segundoApellido == null)
                {
                    segundoApellido = "ㅤ";
                }
                ViewData["segundoApellido"] = segundoApellido;

                var vinculo = _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
                if (vinculo == null)
                {
                    vinculo.Tipo = "ㅤ";
                }
                ViewData["VinculoTipo"] = vinculo.Tipo;

                var contrato = _contexto.TipoContrato.Find(usuario.TipoContratoId);
                if (contrato == null)
                {
                    contrato = new TipoContrato { Tipo = "ㅤ" };
                    //contrato.Tipo = "ㅤ";
                }
                ViewData["ContratoTipo"] = contrato.Tipo;

                var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
                if (infoDocumento.PaisExpedicion == null)
                {
                    infoDocumento.PaisExpedicion = "ㅤ";
                }
                ViewData["PaisExpedicion"] = infoDocumento.PaisExpedicion;

                if (infoDocumento.MunicipioExpedicion == null)
                {
                    infoDocumento.MunicipioExpedicion = "ㅤ";
                }
                ViewData["MunicipioExpedicion"] = infoDocumento.MunicipioExpedicion;

                //var tipoDocumento = _contexto.InfoDocumento
                //    .Where(id => id.TipoDocumentoId == usuario.InfoDocumentoId)
                //    .Select(id => id.TipoDocumento)
                //    .FirstOrDefault();

                //var tipoDocumento = _contexto.TipoDocumento.SingleOrDefault(td => td.Id == infoDocumento.TipoDocumentoId);
                //infoDocumento = _contexto.InfoDocumento
                //    .Include(i => i.TipoDocumento)
                //    .FirstOrDefault(i => i.Id == usuario.InfoDocumentoId);

                //var tipoDocumento = infoDocumento.TipoDocumento?.Tipo;
                var tipoDocumento = _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
                //            var tipoDocumento = _contexto.TipoDocumento
                //.Where(td => td.Id == infoDocumento.TipoDocumentoId)
                //.Select(td => td.Tipo)
                //.FirstOrDefault();
                if (tipoDocumento == null)
                {
                    tipoDocumento.Tipo = "ㅤ";
                }
                ViewData["DocumentoTipo"] = tipoDocumento.Tipo;
            }

            //var rol = _contexto.Rol.Find(idUser);
            //if (rol == null)
            //{
            //    return NotFound();
            //}

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var user = _contexto.Usuario.Select(i => i.User);

            //var user = _userManager.GetUserAsync(User);
            //var UsuarioId = user.Id;

            object[] drop = Utilities.DropDownList(_contexto);

            //ViewBag.TipoVinculo = drop[0];
            //ViewBag.TipoContrato = drop[1];
            //ViewBag.TipoDocumento = drop[2];
            ViewBag.EPS = drop[3];
            ViewBag.FondoPensiones = drop[4];
            ViewBag.FondoCesantias = drop[5];

            var years = Enumerable.Range(DateTime.Now.Year, 1950);
            ViewBag.Years = new SelectList(years, "Año", "Año");

            //TipoDocumento
            //var documentos = _contexto.TipoDocumento.Select(d => new SelectListItem
            //{
            //    Value = d.Id.ToString(),
            //    Text = d.Tipo,
            //});
            //ViewBag.TipoDocumento = documentos;

            //TipoVinculo
            //var tiposVinculo = _contexto.TipoVinculo.ToList();
            //ViewBag.TiposVinculo = tiposVinculo;


            ////TipoDocumento
            //var documentos = _contexto.TipoDocumento.Select(d => new SelectListItem
            //{
            //    Value = d.Id.ToString(),
            //    Text = d.Tipo,
            //});
            //ViewBag.Documentos = documentos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHV(DatosPersonales modelo, List<DatosFamiliares>? datosFamiliares, List<InfoLaboral>? infoLaboral, List<Escolaridad> escolaridad, List<ContactoEmergencia> contactoEmergencia, List<ReferenciasFamiliares> referenciasFamiliares, List<ReferenciasPersonales> referenciasPersonales)
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            string idUser = "";

            if (claimsUser.Identity.IsAuthenticated)
            {
                idUser = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["idUser"] = idUser;

            string userRol = Utilities.GetRol(HttpContext, _contexto);
            //var user = await _userManager.GetUserAsync(User);
            //var UsuarioId = user.Id;

            //var documentos = _contexto.TipoDocumento.Select(d => new SelectListItem
            //{
            //    Value = d.Id.ToString(),
            //    Text = d.Tipo,
            //});
            //ViewBag.TipoDocumento = documentos;
            object[] drop = Utilities.DropDownList(_contexto);

            //ViewBag.TipoVinculo = drop[0];
            //ViewBag.TipoContrato = drop[1];
            //ViewBag.TipoDocumento = drop[2];
            ViewBag.EPS = drop[3];
            ViewBag.FondoPensiones = drop[4];
            ViewBag.FondoCesantias = drop[5];

            if(modelo.DatosGenerales != null)
            {
                if(modelo.DatosGenerales.OtrosIngresos == null)
                {
                    modelo.DatosGenerales.OtrosIngresos = "No";
                }

                if(modelo.DatosGenerales.ParientesTrabajando == null)
                {
                    modelo.DatosGenerales.ParientesTrabajando = "No";
                }
            }

            if (ModelState.IsValid)
            {
                //var infoDocumento = new InfoDocumento
                //{
                //    TipoDocumento = modelo.InfoDocumento.TipoDocumento,
                //    PaisExpedicion = modelo.InfoDocumento.PaisExpedicion,
                //    MunicipioExpedicion = modelo.InfoDocumento.MunicipioExpedicion
                //};

                // Crear una instancia de PersonasACargo y asignar los valores ingresados en el formulario
                var personasACargo = new PersonasACargo
                {
                    Hijo = modelo.PersonasACargo.Hijo,
                    Conyugue = modelo.PersonasACargo.Conyugue,
                    Padres = modelo.PersonasACargo.Padres,
                    Otros = modelo.PersonasACargo.Otros
                };

                var direccion = new Direccion
                {
                    Num1 = modelo.Direccion.Num1,
                    Num2 = modelo.Direccion.Num2,
                    Num3 = modelo.Direccion.Num3,
                    Complemento = modelo.Direccion.Complemento,
                    DireccionCompleta = modelo.Direccion.DireccionCompleta
                };

                if (modelo.Practicas != null /*|| modelo.Practicas.Institucion != null || modelo.Practicas.Programa != null || modelo.Practicas.Titulo != null || modelo.Practicas.FechaInicio != null || modelo.Practicas.FechaInicio != null || modelo.Practicas.DocenciaServicios != null*/)
                {
                    if (modelo.Practicas.Institucion != null || modelo.Practicas.Programa != null || modelo.Practicas.Titulo != null || modelo.Practicas.FechaInicio != null || modelo.Practicas.FechaInicio != null || modelo.Practicas.DocenciaServicios != null) {
                        var practicas = new Practicas
                        {
                            Institucion = modelo.Practicas.Institucion,
                            Programa = modelo.Practicas.Programa,
                            Titulo = modelo.Practicas.Titulo,
                            FechaInicio = modelo.Practicas.FechaInicio,
                            FechaFinalizacion = modelo.Practicas.FechaFinalizacion,
                            DocenciaServicios = modelo.Practicas.DocenciaServicios
                        };
                        modelo.PracticasId = practicas.Id;
                    }
                }

                if (modelo.DatosGenerales !=null /*||modelo.DatosGenerales.ComoSupo != null || modelo.DatosGenerales.OtrosIngresos != null || modelo.DatosGenerales.Ingreso != null || modelo.DatosGenerales.ParientesTrabajando != null || modelo.DatosGenerales.TipoVivienda != null*/)
                {
                    if (modelo.DatosGenerales.ComoSupo != null || modelo.DatosGenerales.OtrosIngresos != null || modelo.DatosGenerales.Ingreso != null || modelo.DatosGenerales.ParientesTrabajando != null || modelo.DatosGenerales.TipoVivienda != null)
                    {
                        var datosGenerales = new DatosGenerales
                        {
                            ComoSupo = modelo.DatosGenerales.ComoSupo,
                            OtrosIngresos = modelo.DatosGenerales.OtrosIngresos,
                            Ingreso = modelo.DatosGenerales.Ingreso,
                            ParientesTrabajando = modelo.DatosGenerales.ParientesTrabajando,
                            TipoVivienda = modelo.DatosGenerales.TipoVivienda
                        };

                        modelo.DatosGeneralesId = datosGenerales.Id;
                    }
                }

                //if (datosFamiliares[0].Nombre != null && datosFamiliares[0].FechaNacimiento != null && datosFamiliares[0].Parentesco != null && datosFamiliares[0].Ocupacion != null)
                //{
                //int iF = 0;
                //foreach (var datosFamiliaresItem in datosFamiliares)
                //{
                //    if (datosFamiliares[iF].Nombre != null && datosFamiliares[iF].FechaNacimiento != null && datosFamiliares[iF].Parentesco != null && datosFamiliares[iF].Ocupacion != null)
                //    {
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        //datosFamiliaresItem.DatosPersonalesId = modelo.Id;

                //        //_contexto.DatosFamiliares.Add(datosFamiliaresItem);
                //        datosFamiliares.RemoveAt(iF);
                //    }
                //    iF++;
                //}

                //int iL = 0;
                //foreach (var infoLaboralItem in infoLaboral)
                //{
                //    if (infoLaboral[iL].FechaIngreso != null && infoLaboral[iL].FechaRetiro != null && infoLaboral[iL].NombreEmpresa != null && infoLaboral[iL].MotivoRetiro != null && infoLaboral[iL].Celular != null && infoLaboral[iL].Cargo != null)
                //    {
                //        //if (!(infoLaboral[i].FechaIngreso == null || infoLaboral[i].FechaRetiro == null || infoLaboral[i].NombreEmpresa == null || infoLaboral[i].MotivoRetiro == null || infoLaboral[i].Celular == null || infoLaboral[i].Cargo == null))
                //        //{
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        //infoLaboralItem.DatosPersonalesId = modelo.Id;

                //        //_contexto.InfoLaboral.Add(infoLaboralItem);
                //        infoLaboral.RemoveAt(iL);
                //    }
                //    iL++;
                //}

                modelo.DatosFamiliares = null;
                modelo.InfoLaboral = null;

                //for (int iF = datosFamiliares.Count - 1; iF >= 0; iF--)
                //{
                //    if (datosFamiliares[iF].Nombre != null && datosFamiliares[iF].FechaNacimiento != null && datosFamiliares[iF].Parentesco != null && datosFamiliares[iF].Ocupacion != null)
                //    {
                //        modelo.DatosFamiliares.Remove(datosFamiliares[iF]);
                //    }
                //}

                //for (int iL = modelo.InfoLaboral.Count - 1; iL >= 0; iL--)
                //{
                //    if (infoLaboral[iL].FechaIngreso != null && infoLaboral[iL].FechaRetiro != null && infoLaboral[iL].NombreEmpresa != null && infoLaboral[iL].MotivoRetiro != null && infoLaboral[iL].Celular != null && infoLaboral[iL].Cargo != null)
                //    {
                //        modelo.InfoLaboral.Remove(infoLaboral[iL]);
                //    }
                //}

                //for (int iL = modelo.InfoLaboral.Count - 1; iL >= 0; iL--)
                //{
                //    if (infoLaboral[iL].FechaIngreso != null && infoLaboral[iL].FechaRetiro != null && infoLaboral[iL].NombreEmpresa != null && infoLaboral[iL].MotivoRetiro != null && infoLaboral[iL].Celular != null && infoLaboral[iL].Cargo != null)
                //    {
                //        modelo.InfoLaboral.Remove(infoLaboral[iL]);
                //    }
                //}

                // Agregar instancia de InfoDocumento y PersonasACargo al contexto de base de datos
                //_contexto.InfoDocumento.Add(infoDocumento);
                //_contexto.PersonasACargo.Add(personasACargo);

                // Asignar el ID de la instancia de PersonasACargo a la propiedad PersonasACargoId de la instancia de DatosPersonales
                //modelo.InfoDocumentoId = infoDocumento.Id;
                modelo.PersonasACargoId = personasACargo.Id;
                modelo.DireccionId = direccion.Id;

                // Agregar instancia de DatosPersonales al contexto de base de datos
                _contexto.DatosPersonales.Add(modelo);

                await _contexto.SaveChangesAsync();

                //if (datosFamiliares[0].Nombre != null || datosFamiliares[0].FechaNacimiento != null || datosFamiliares[0].Parentesco != null || datosFamiliares[0].Ocupacion != null)
                //{
                //    foreach (var datosFamiliaresItem in datosFamiliares)
                //    {
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        datosFamiliaresItem.DatosPersonalesId = modelo.Id;

                //        _contexto.DatosFamiliares.Add(datosFamiliaresItem);
                //    }
                //}

                //foreach (var infoLaboralItem in infoLaboral) {
                //    int i = 0;
                //    if (infoLaboral[i].FechaIngreso != null || infoLaboral[i].FechaRetiro != null || infoLaboral[i].NombreEmpresa != null || infoLaboral[i].MotivoRetiro != null || infoLaboral[i].Celular != null || infoLaboral[i].Cargo != null)
                //    {
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        infoLaboralItem.DatosPersonalesId = modelo.Id;

                //        _contexto.InfoLaboral.Add(infoLaboralItem);
                //    }
                //    i++;
                //}

                //foreach (var referenciasFamiliaresItem in referenciasFamiliares)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasFamiliares
                //    referenciasFamiliaresItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ReferenciasFamiliares.Add(referenciasFamiliaresItem);
                //}

                //foreach (var referenciasPersonalesItem in referenciasPersonales)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasPersonales
                //    referenciasPersonalesItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ReferenciasPersonales.Add(referenciasPersonalesItem);
                //}

                //foreach (var contactoEmergenciaItem in contactoEmergencia)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ContactoEmergencia
                //    contactoEmergenciaItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ContactoEmergencia.Add(contactoEmergenciaItem);
                //}

                //foreach (var escolaridadItem in escolaridad)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de Escolaridad
                //    escolaridadItem.DatosPersonalesId = modelo.Id;

                //    _contexto.Escolaridad.Add(escolaridadItem);
                //}

                if (datosFamiliares[0].Nombre != null && datosFamiliares[0].FechaNacimiento != null && datosFamiliares[0].Parentesco != null && datosFamiliares[0].Ocupacion != null)
                {
                    foreach (var datosFamiliaresItem in datosFamiliares)
                    {
                        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                        datosFamiliaresItem.DatosPersonalesId = modelo.Id;

                        _contexto.DatosFamiliares.Add(datosFamiliaresItem);
                    }
                }

                int i = 0;
                foreach (var infoLaboralItem in infoLaboral)
                {
                    if (infoLaboral[i].FechaIngreso != null && infoLaboral[i].FechaRetiro != null && infoLaboral[i].NombreEmpresa != null && infoLaboral[i].MotivoRetiro != null && infoLaboral[i].Celular != null && infoLaboral[i].Cargo != null)
                    {
                        //if (!(infoLaboral[i].FechaIngreso == null || infoLaboral[i].FechaRetiro == null || infoLaboral[i].NombreEmpresa == null || infoLaboral[i].MotivoRetiro == null || infoLaboral[i].Celular == null || infoLaboral[i].Cargo == null))
                        //{
                        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                        infoLaboralItem.DatosPersonalesId = modelo.Id;

                        _contexto.InfoLaboral.Add(infoLaboralItem);
                    }
                    i++;
                }

                await _contexto.SaveChangesAsync();

                //await _contexto.SaveChangesAsync();

                //if (datosFamiliares[0].Nombre != null && datosFamiliares[0].FechaNacimiento != null && datosFamiliares[0].Parentesco != null && datosFamiliares[0].Ocupacion != null)
                //{
                //    foreach (var datosFamiliaresItem in datosFamiliares)
                //    {
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        datosFamiliaresItem.DatosPersonalesId = modelo.Id;

                //        _contexto.DatosFamiliares.Add(datosFamiliaresItem);
                //    }
                //}

                //int i = 0;
                //foreach (var infoLaboralItem in infoLaboral)
                //{
                //    if (infoLaboral[i].FechaIngreso != null && infoLaboral[i].FechaRetiro != null && infoLaboral[i].NombreEmpresa != null && infoLaboral[i].MotivoRetiro != null && infoLaboral[i].Celular != null && infoLaboral[i].Cargo != null)
                //    {
                //    //if (!(infoLaboral[i].FechaIngreso == null || infoLaboral[i].FechaRetiro == null || infoLaboral[i].NombreEmpresa == null || infoLaboral[i].MotivoRetiro == null || infoLaboral[i].Celular == null || infoLaboral[i].Cargo == null))
                //    //{
                //        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                //        infoLaboralItem.DatosPersonalesId = modelo.Id;

                //        _contexto.InfoLaboral.Add(infoLaboralItem);
                //    }
                //    i++;
                //}

                //foreach (var referenciasFamiliaresItem in referenciasFamiliares)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasFamiliares
                //    referenciasFamiliaresItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ReferenciasFamiliares.Add(referenciasFamiliaresItem);
                //}

                //foreach (var referenciasPersonalesItem in referenciasPersonales)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasPersonales
                //    referenciasPersonalesItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ReferenciasPersonales.Add(referenciasPersonalesItem);
                //}

                //foreach (var contactoEmergenciaItem in contactoEmergencia)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ContactoEmergencia
                //    contactoEmergenciaItem.DatosPersonalesId = modelo.Id;

                //    _contexto.ContactoEmergencia.Add(contactoEmergenciaItem);
                //}

                //foreach (var escolaridadItem in escolaridad)
                //{
                //    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de Escolaridad
                //    escolaridadItem.DatosPersonalesId = modelo.Id;

                //    _contexto.Escolaridad.Add(escolaridadItem);
                //}

                //await _contexto.SaveChangesAsync();**

                if (modelo.Id != null && userRol == "Admin" || modelo.Id != null && userRol == "Observador")
                {
                    return RedirectToAction("AllHV", "ManageHV");
                }
                if (modelo.Id != null && userRol == "Empleado")
                {
                    return RedirectToAction("MyHV", "ManageHV");
                }



                ViewData["Mensaje"] = "No se pudo crear la HV";

                return View(modelo);
            }

            string primerNombre = "";
            string segundoNombre = "";
            string primerApellido = "";
            string segundoApellido = "";

            var usuario = _contexto.Usuario.Find(idUser);
            if (usuario == null)
            {
                return NotFound();
            }

            primerNombre = usuario.PrimerNombre;
            ViewData["primerNombre"] = primerNombre;

            segundoNombre = usuario.SegundoNombre;
            if (segundoNombre == null)
            {
                segundoNombre = "ㅤ";
            }
            ViewData["segundoNombre"] = segundoNombre;

            primerApellido = usuario.PrimerApellido;
            ViewData["primerApellido"] = primerApellido;

            segundoApellido = usuario.SegundoApellido;
            if (segundoApellido == null)
            {
                segundoApellido = "ㅤ";
            }
            ViewData["segundoApellido"] = segundoApellido;

            var vinculo = _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
            if (vinculo == null)
            {
                vinculo.Tipo = "ㅤ";
            }
            ViewData["VinculoTipo"] = vinculo.Tipo;

            var contrato = _contexto.TipoContrato.Find(usuario.TipoContratoId);
            if (contrato == null)
            {
                contrato = new TipoContrato { Tipo = "ㅤ" };
                //contrato.Tipo = "ㅤ";
            }
            ViewData["ContratoTipo"] = contrato.Tipo;

            var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
            if (infoDocumento.PaisExpedicion == null)
            {
                infoDocumento.PaisExpedicion = "ㅤ";
            }
            ViewData["PaisExpedicion"] = infoDocumento.PaisExpedicion;

            if (infoDocumento.MunicipioExpedicion == null)
            {
                infoDocumento.MunicipioExpedicion = "ㅤ";
            }
            ViewData["MunicipioExpedicion"] = infoDocumento.MunicipioExpedicion;
            var tipoDocumento = _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
            if (tipoDocumento == null)
            {
                tipoDocumento.Tipo = "ㅤ";
            }
            ViewData["DocumentoTipo"] = tipoDocumento.Tipo;
            var years = Enumerable.Range(DateTime.Now.Year, 1950);
            ViewBag.Years = new SelectList(years, "Año", "Año");

            ViewData["Mensaje"] = "No se pudo crear la HV, Ingrese correctamente lo datos";
            return View(modelo);
        }

        //DetalleHV
        [HttpGet]
        public IActionResult DetailHV(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosPersonales = _contexto.DatosPersonales.Find(id);
            if (datosPersonales == null)
            {
                return NotFound();
            }

            //var usuario = _contexto.Usuario.Where(id => id.User == datosPersonales.UsuarioId);
            var usuario = _contexto.Usuario.Find(datosPersonales.UsuarioId);

            // Use Include() to load the ContactoEmergencia collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ContactoEmergencia).FirstOrDefault(dp => dp.Id == id);

            // Use Include() to load the DatosFamiliares collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.DatosFamiliares).FirstOrDefault(dp => dp.Id == id);

            // Use Include() to load the Escolaridad collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.Escolaridad).FirstOrDefault(dp => dp.Id == id);

            // Use Include() to load the InfoLaboral collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.InfoLaboral).FirstOrDefault(dp => dp.Id == id);

            // Use Include() to load the ReferenciasFamiliares collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasFamiliares).FirstOrDefault(dp => dp.Id == id);

            // Use Include() to load the ReferenciasPersonales collection
            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasPersonales).FirstOrDefault(dp => dp.Id == id);

            string userRol = Utilities.GetRol(HttpContext, _contexto);

            ////ViewBag.UserId = userId;
            ViewData["userRol"] = userRol;

            //var tipo = _contexto.TipoDocumento.Find(datosPersonales.InfoDocumento.TipoDocumentoId);
            //ViewData["RolTipo"] = tipo.Tipo;

            var tipoVinculo = _contexto.TipoVinculo.Find(_contexto.Usuario
                  .Where(id => id.User == datosPersonales.UsuarioId)
                  .Select(u => u.TipoVinculoId)
                  .FirstOrDefault());

            var tipoContrato = _contexto.TipoContrato.Find(_contexto.Usuario
                  .Where(id => id.User == datosPersonales.UsuarioId)
                  .Select(u => u.TipoContratoId)
                  .FirstOrDefault());

            var infoDocumento = _contexto.InfoDocumento.Find(_contexto.Usuario
                  .Where(id => id.User == datosPersonales.UsuarioId)
                  .Select(u => u.InfoDocumentoId)
                  .FirstOrDefault());

            //var tipoVinculo = _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
            //var tipoContrato = _contexto.TipoContrato.Find(usuario.TipoContratoId);
            //var tipoDocumento = _contexto.TipoDocumento.Find(datosPersonales.InfoDocumentoId);
            //var tipoDocumento = _contexto.TipoDocumento.Find(InfoDocumento.)
            //var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
            var tipoDocumento = _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
            var personasACargo = _contexto.PersonasACargo.Find(datosPersonales.PersonasACargoId);
            var eps = _contexto.EPS.Find(datosPersonales.EPSId);
            var fondoPensiones = _contexto.FondoPensiones.Find(datosPersonales.FondoPensionesId);
            var fondoCesantias = _contexto.FondoCesantias.Find(datosPersonales.FondoCesantiasId);
            var direccion = _contexto.Direccion.Find(datosPersonales.DireccionId);

            //foreach (var item in _contexto.InfoLaboral){
            //    DateTime fecha = item.FechaIngreso.;
            //    string newFecha = item.FechaIngreso.ToString("d/M/yyyy", CultureInfo.InvariantCulture);
            //    item.FechaIngreso = item.FechaIngreso;
            //}

            usuario.TipoVinculo = tipoVinculo;
            usuario.TipoContrato = tipoContrato;
            usuario.InfoDocumento.TipoDocumento = tipoDocumento;
            usuario.InfoDocumento = infoDocumento;

            var fechaNac = datosPersonales.FechaNacimiento;
            var edad = DateTime.Today.Year - fechaNac.Year;
            if (fechaNac > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            ViewData["FechaNacimiento"] = fechaNac.ToString("dd/MM/yyyy");
            ViewData["Edad"] = edad;

            datosPersonales.Usuario = usuario;

            return View(datosPersonales);
        }

        public IActionResult GeneratePdf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosPersonales = _contexto.DatosPersonales.Find(id);
            if (datosPersonales == null)
            {
                return NotFound();
            }

            var usuario = _contexto.Usuario.Find(datosPersonales.UsuarioId);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ContactoEmergencia).FirstOrDefault(dp => dp.Id == id);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.DatosFamiliares).FirstOrDefault(dp => dp.Id == id);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.Escolaridad).FirstOrDefault(dp => dp.Id == id);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.InfoLaboral).FirstOrDefault(dp => dp.Id == id);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasFamiliares).FirstOrDefault(dp => dp.Id == id);

            datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasPersonales).FirstOrDefault(dp => dp.Id == id);

            string userRol = Utilities.GetRol(HttpContext, _contexto);
            ViewData["userRol"] = userRol;

            var tipoVinculo = _contexto.TipoVinculo.Find(_contexto.Usuario
                    .Where(id => id.User == datosPersonales.UsuarioId)
                    .Select(u => u.TipoVinculoId)
                    .FirstOrDefault());

            var tipoContrato = _contexto.TipoContrato.Find(_contexto.Usuario
                    .Where(id => id.User == datosPersonales.UsuarioId)
                    .Select(u => u.TipoContratoId)
                    .FirstOrDefault());

            var infoDocumento = _contexto.InfoDocumento.Find(_contexto.Usuario
                    .Where(id => id.User == datosPersonales.UsuarioId)
                    .Select(u => u.InfoDocumentoId)
                    .FirstOrDefault());
            var tipoDocumento = _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
            var personasACargo = _contexto.PersonasACargo.Find(datosPersonales.PersonasACargoId);
            var eps = _contexto.EPS.Find(datosPersonales.EPSId);
            var fondoPensiones = _contexto.FondoPensiones.Find(datosPersonales.FondoPensionesId);
            var fondoCesantias = _contexto.FondoCesantias.Find(datosPersonales.FondoCesantiasId);
            var direccion = _contexto.Direccion.Find(datosPersonales.DireccionId);

            usuario.TipoVinculo = tipoVinculo;
            usuario.TipoContrato = tipoContrato;
            usuario.InfoDocumento.TipoDocumento = tipoDocumento;
            usuario.InfoDocumento = infoDocumento;

            var fechaNac = datosPersonales.FechaNacimiento;
            var edad = DateTime.Today.Year - fechaNac.Year;
            if (fechaNac > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            ViewData["FechaNacimiento"] = fechaNac.ToString("dd/MM/yyyy");
            ViewData["Edad"] = edad;

            datosPersonales.Usuario = usuario;

            return new ViewAsPdf("PDFHV", datosPersonales)
            {
                FileName = $"CURRICULUM_VITAE_{datosPersonales.Usuario.PrimerNombre}_{datosPersonales.Usuario.PrimerApellido}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        //EditarHV
        [HttpGet]
        public IActionResult EditHV(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosPersonales = _contexto.DatosPersonales.Find(id);
            if (datosPersonales == null)
            {
                return NotFound();
            }

            ClaimsPrincipal claimsUser = HttpContext.User;
            string idUser = "";

            if (claimsUser.Identity.IsAuthenticated)
            {
                idUser = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }
            string userRol = Utilities.GetRol(HttpContext, _contexto);

            // Verificar que el usuario solo tenga acceso a su propia HV
            if (userRol != "Admin" && idUser != datosPersonales.UsuarioId)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewData["UserRol"] = userRol;
            //var infoDocumento = _contexto.InfoDocumento.Find(datosPersonales.InfoDocumentoId);
            //var personasACargo = _contexto.PersonasACargo.Find(datosPersonales.PersonasACargoId);

            //string primerNombre = "";
            //string segundoNombre = "";
            //string primerApellido = "";
            //string segundoApellido = "";

            object[] drop = Utilities.DropDownList(_contexto);

            //ViewBag.TipoVinculo = drop[0];
            //ViewBag.TipoContrato = drop[1];
            //ViewBag.TipoDocumento = drop[2];
            ViewBag.EPS = drop[3];
            ViewBag.FondoPensiones = drop[4];
            ViewBag.FondoCesantias = drop[5];

            //ViewData["RolTipo"] = rol.Tipo;
            //claimsUser.Claims.Where(c=> c.Type == ClaimTypes.)

            if (claimsUser.Identity.IsAuthenticated)
            {
                //idUser = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                //    .Select(c => c.Value).SingleOrDefault();

                ViewData["idUser"] = idUser;

                var usuario = _contexto.Usuario.Find(datosPersonales.UsuarioId);
                //var usuario = _contexto.Usuario.Find(idUser);
                if (usuario == null)
                {
                    return NotFound();
                }

                ViewData["idUser"] = idUser;

                // Use Include() to load the ContactoEmergencia collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ContactoEmergencia).FirstOrDefault(dp => dp.Id == id);

                // Use Include() to load the DatosFamiliares collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.DatosFamiliares).FirstOrDefault(dp => dp.Id == id);

                // Use Include() to load the Escolaridad collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.Escolaridad).FirstOrDefault(dp => dp.Id == id);

                // Use Include() to load the InfoLaboral collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.InfoLaboral).FirstOrDefault(dp => dp.Id == id);

                // Use Include() to load the ReferenciasFamiliares collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasFamiliares).FirstOrDefault(dp => dp.Id == id);

                // Use Include() to load the ReferenciasPersonales collection
                datosPersonales = _contexto.DatosPersonales.Include(dp => dp.ReferenciasPersonales).FirstOrDefault(dp => dp.Id == id);

                //var escolaridad = _contexto.Escolaridad.Find(datosPersonales.Id);
                //Escolaridad[] escolaridadArray = _contexto.Escolaridad.Find(datosPersonales.Id);

                //foreach (var item in escolaridad)
                //{

                //}
                //escolaridad.
                //primerNombre = usuario.PrimerNombre;
                //ViewData["primerNombre"] = primerNombre;

                //segundoNombre = usuario.SegundoNombre;
                //if (segundoNombre == null)
                //{
                //    segundoNombre = "ㅤ";
                //}
                //ViewData["segundoNombre"] = segundoNombre;

                //primerApellido = usuario.PrimerApellido;
                //ViewData["primerApellido"] = primerApellido;

                //segundoApellido = usuario.SegundoApellido;
                //if (segundoApellido == null)
                //{
                //    segundoApellido = "ㅤ";
                //}
                //ViewData["segundoApellido"] = segundoApellido;

                //var vinculo = _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
                //if (vinculo == null)
                //{
                //    vinculo.Tipo = "ㅤ";
                //}
                //ViewData["VinculoTipo"] = vinculo.Tipo;

                //var contrato = _contexto.TipoContrato.Find(usuario.TipoContratoId);
                //if (contrato == null)
                //{
                //    contrato.Tipo = "ㅤ";
                //}
                //ViewData["ContratoTipo"] = contrato.Tipo;

                //var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
                //if (infoDocumento.PaisExpedicion == null)
                //{
                //    infoDocumento.PaisExpedicion = "ㅤ";
                //}
                //ViewData["PaisExpedicion"] = infoDocumento.PaisExpedicion;

                //if (infoDocumento.MunicipioExpedicion == null)
                //{
                //    infoDocumento.MunicipioExpedicion = "ㅤ";
                //}
                //ViewData["MunicipioExpedicion"] = infoDocumento.MunicipioExpedicion;

                //var tipoDocumento = _contexto.InfoDocumento
                //    .Where(id => id.TipoDocumentoId == usuario.InfoDocumentoId)
                //    .Select(id => id.TipoDocumento)
                //    .FirstOrDefault();

                //var tipoDocumento = _contexto.TipoDocumento.SingleOrDefault(td => td.Id == infoDocumento.TipoDocumentoId);
                //infoDocumento = _contexto.InfoDocumento
                //    .Include(i => i.TipoDocumento)
                //    .FirstOrDefault(i => i.Id == usuario.InfoDocumentoId);

                //var tipoDocumento = infoDocumento.TipoDocumento?.Tipo;

                var tipoVinculo = _contexto.TipoVinculo.Find(_contexto.Usuario
                  .Where(id => id.User == datosPersonales.UsuarioId)
                  .Select(u => u.TipoVinculoId)
                  .FirstOrDefault());
                ViewData["VinculoTipo"] = tipoVinculo.Tipo;

                var tipoContrato = _contexto.TipoContrato.Find(_contexto.Usuario
                      .Where(id => id.User == datosPersonales.UsuarioId)
                      .Select(u => u.TipoContratoId)
                      .FirstOrDefault());

                var infoDocumento = _contexto.InfoDocumento.Find(_contexto.Usuario
                      .Where(id => id.User == datosPersonales.UsuarioId)
                      .Select(u => u.InfoDocumentoId)
                      .FirstOrDefault());

                var tipoDocumento = _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
                _contexto.PersonasACargo.Find(datosPersonales.PersonasACargoId);
                _contexto.EPS.Find(datosPersonales.EPSId);
                _contexto.FondoPensiones.Find(datosPersonales.FondoPensionesId);
                _contexto.FondoCesantias.Find(datosPersonales.FondoCesantiasId);
                _contexto.Direccion.Find(datosPersonales.DireccionId);

                _contexto.DatosGenerales.Find(datosPersonales.DatosGeneralesId);
                _contexto.Practicas.Find(datosPersonales.PracticasId);
                //            var tipoDocumento = _contexto.TipoDocumento
                //.Where(td => td.Id == infoDocumento.TipoDocumentoId)
                //.Select(td => td.Tipo)
                //.FirstOrDefault();
                //if (tipoDocumento == null)
                //{
                //    tipoDocumento.Tipo = "ㅤ";
                //}
                //ViewData["DocumentoTipo"] = tipoDocumento.Tipo;

                usuario.TipoVinculo = tipoVinculo;
                usuario.TipoContrato = tipoContrato;
                usuario.InfoDocumento.TipoDocumento = tipoDocumento;
                usuario.InfoDocumento = infoDocumento;

                //var referenciasFamiliares = _contexto.ReferenciasFamiliares.Find(datosPersonales.Id);
                var referenciasFamiliares = _contexto.ReferenciasFamiliares.Where(id => id.DatosPersonalesId == datosPersonales.Id);
            }

            return View(datosPersonales);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHV(DatosPersonales modelo, List<DatosFamiliares>? datosFamiliares, List<InfoLaboral>? infoLaboral, List<Escolaridad> escolaridad, List<ContactoEmergencia> contactoEmergencia, List<ReferenciasFamiliares> referenciasFamiliares, List<ReferenciasPersonales> referenciasPersonales)
        {
            //var personasACargo = _contexto.PersonasACargo.Find(modelo.PersonasACargoId);

            //var datosPersonales = _contexto.DatosPersonales.Find(id);
            //if (datosPersonales == null)
            //{
            //    return NotFound();
            //}

            //var usuario = _contexto.Usuario.Find(datosPersonales.UsuarioId);
            ////var usuario = _contexto.Usuario.Find(idUser);
            //if (usuario == null)
            //{
            //    return NotFound();
            //}
            
            if (ModelState.IsValid)
            {
                if (modelo.DatosGenerales != null)
                {
                    if (modelo.DatosGenerales.OtrosIngresos == null)
                    {
                        modelo.DatosGenerales.OtrosIngresos = "No";
                    }

                    if (modelo.DatosGenerales.ParientesTrabajando == null)
                    {
                        modelo.DatosGenerales.ParientesTrabajando = "No";
                    }
                }

                string userRol = Utilities.GetRol(HttpContext, _contexto);
                modelo.ContactoEmergencia = null;
                modelo.DatosFamiliares = null;
                modelo.Escolaridad = null;
                modelo.InfoLaboral = null;
                modelo.ReferenciasFamiliares = null;
                modelo.ReferenciasPersonales = null;

                var datosPersonales = await _contexto.DatosPersonales
                .Include(d => d.PersonasACargo)
                .FirstOrDefaultAsync(d => d.Id == modelo.Id);
                _contexto.Entry(datosPersonales).State = EntityState.Detached;

                //if (datosPersonales.PersonasACargoId != null)
                //{
                //    var personasACargo = await _contexto.PersonasACargo.FindAsync(datosPersonales.PersonasACargoId);
                //    _contexto.PersonasACargo.Remove(personasACargo);
                //}
                //await _contexto.SaveChangesAsync();

                //if (datosPersonales.PersonasACargoId != null)
                //{
                //    var personasACargo = await _contexto.PersonasACargo.FindAsync(datosPersonales.PersonasACargoId);
                //    _contexto.PersonasACargo.RemoveRange(_contexto.PersonasACargo.Where(p => p.Id == datosPersonales.PersonasACargoId));
                //}

                //_contexto.PersonasACargo.Update(modelo.PersonasACargo);

                _contexto.Update(modelo);

                //await _contexto.SaveChangesAsync();**

                //var datosPersonales = await _contexto.DatosPersonales
                //.Include(d => d.PersonasACargo)
                //.FirstOrDefaultAsync(d => d.Id == modelo.Id);

                var personasACargo = await _contexto.PersonasACargo.FindAsync(datosPersonales.PersonasACargoId);
                var direccion = await _contexto.Direccion.FindAsync(datosPersonales.DireccionId);
                var datosGenerales = await _contexto.DatosGenerales.FindAsync(datosPersonales.DatosGeneralesId);
                var practicas = await _contexto.Practicas.FindAsync(datosPersonales.PracticasId);
                //_contexto.PersonasACargo.RemoveRange(personasACargo.Id);

                _contexto.ContactoEmergencia.RemoveRange(_contexto.ContactoEmergencia.Where(e => e.DatosPersonalesId == modelo.Id));
                foreach (var contactoEmergenciaItem in contactoEmergencia)
                {
                    contactoEmergenciaItem.DatosPersonalesId = modelo.Id;
                    _contexto.ContactoEmergencia.Add(contactoEmergenciaItem);
                }

                _contexto.DatosFamiliares.RemoveRange(_contexto.DatosFamiliares.Where(e => e.DatosPersonalesId == modelo.Id));
                if (datosFamiliares[0].Nombre != null || datosFamiliares[0].FechaNacimiento != null || datosFamiliares[0].Parentesco != null || datosFamiliares[0].Ocupacion != null)
                {
                    foreach (var datosFamiliaresItem in datosFamiliares)
                    {
                        datosFamiliaresItem.DatosPersonalesId = modelo.Id;
                        _contexto.DatosFamiliares.Add(datosFamiliaresItem);
                    }
                }

                _contexto.Escolaridad.RemoveRange(_contexto.Escolaridad.Where(e => e.DatosPersonalesId == modelo.Id));
                foreach (var escolaridadItem in escolaridad)
                {
                    escolaridadItem.DatosPersonalesId = modelo.Id;
                    _contexto.Escolaridad.Add(escolaridadItem);
                }

                _contexto.InfoLaboral.RemoveRange(_contexto.InfoLaboral.Where(e => e.DatosPersonalesId == modelo.Id));
                int i = 0;
                foreach (var infoLaboralItem in infoLaboral)
                {
                    if (infoLaboral[i].FechaIngreso != null || infoLaboral[i].FechaRetiro != null || infoLaboral[i].NombreEmpresa != null || infoLaboral[i].MotivoRetiro != null || infoLaboral[i].Celular != null || infoLaboral[i].Cargo != null)
                    {
                        // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de DatosFamiliares
                        infoLaboralItem.DatosPersonalesId = modelo.Id;

                        _contexto.InfoLaboral.Add(infoLaboralItem);
                    }
                    i++;
                }
                
                _contexto.ReferenciasFamiliares.RemoveRange(_contexto.ReferenciasFamiliares.Where(e => e.DatosPersonalesId == modelo.Id));
                foreach (var referenciasFamiliaresItem in referenciasFamiliares)
                {
                    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasFamiliares
                    referenciasFamiliaresItem.DatosPersonalesId = modelo.Id;

                    _contexto.ReferenciasFamiliares.Add(referenciasFamiliaresItem);
                }

                _contexto.ReferenciasPersonales.RemoveRange(_contexto.ReferenciasPersonales.Where(e => e.DatosPersonalesId == modelo.Id));
                foreach (var referenciasPersonalesItem in referenciasPersonales)
                {
                    // Asignar el ID de la instancia de DatosPersonales a la propiedad DatosPersonalesId de la instancia de ReferenciasPersonales
                    referenciasPersonalesItem.DatosPersonalesId = modelo.Id;

                    _contexto.ReferenciasPersonales.Add(referenciasPersonalesItem);
                }

                await _contexto.SaveChangesAsync();

                _contexto.PersonasACargo.Remove(personasACargo);
                _contexto.Direccion.Remove(direccion);
                if (datosGenerales != null)
                {
                    _contexto.DatosGenerales.Remove(datosGenerales);
                }
                if (practicas != null)
                {
                    _contexto.Practicas.Remove(practicas);
                }

                await _contexto.SaveChangesAsync();

                if (modelo.Id != null && userRol == "Admin" || modelo.Id != null && userRol == "Observador")
                {
                    return RedirectToAction("AllHV", "ManageHV");
                }
                if (modelo.Id != null && userRol == "Empleado")
                {
                    return RedirectToAction("MyHV", "ManageHV");
                }
                ViewData["Mensaje"] = "No se pudo editar la HV";
                return View(modelo);
            }

            return View();
        }

        //ExcelHV
        [HttpGet]
        public async Task<IActionResult> GetHVExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HV HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetAllHV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    worksheet.Cell(row, column).Value = dataColumn.ColumnName;
                    column++;
                }
                row++;
                column = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        worksheet.Cell(row, column).Value = dataRow[dataColumn.ColumnName].ToString();
                        column++;
                    }
                    row++;
                    column = 1;
                }
                var memory = new MemoryStream();
                workbook.SaveAs(memory);
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HVHISA.xlsx");
            }
        }

        //ExcelActiveHV
        [HttpGet]
        public async Task<IActionResult> GetActiveHVExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Active HV HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetActiveHV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    worksheet.Cell(row, column).Value = dataColumn.ColumnName;
                    column++;
                }
                row++;
                column = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        worksheet.Cell(row, column).Value = dataRow[dataColumn.ColumnName].ToString();
                        column++;
                    }
                    row++;
                    column = 1;
                }
                var memory = new MemoryStream();
                workbook.SaveAs(memory);
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveHVHISA.xlsx");
            }
        }

        //EliminaHV
        [HttpGet]
        public IActionResult DeleteHV(int? documento)
        {
            if (documento == null)
            {
                return NotFound();
            }

            var datosPersonales = _contexto.DatosPersonales.Find(documento);
            if (datosPersonales == null)
            {
                return NotFound();
            }

            string userRol = Utilities.GetRol(HttpContext, _contexto);

            // Verificar que solo el rol Admin pueda eliminar HV
            if (userRol != "Admin")
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            //var infoDocumento = _contexto.InfoDocumento.Find(datosPersonales.InfoDocumentoId);
            var personasACargo = _contexto.PersonasACargo.Find(datosPersonales.PersonasACargoId);

            return View(datosPersonales);
        }

        [HttpPost, ActionName("DeleteHV")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHVAction(int? documento)
        {
            var datosPersonales = await _contexto.DatosPersonales.FindAsync(documento);
            if (datosPersonales == null)
            {
                return View();
            }
            //Borrado
            _contexto.DatosPersonales.Remove(datosPersonales);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("AllHV", "ManageHV");
        }
    }
}
