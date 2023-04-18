using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;
using Microsoft.EntityFrameworkCore;

namespace RH_CV.Controllers
{
    [Authorize]
    public class ManageInterdependences : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageInterdependences(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllInterdependences()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Interdependencia> interdependencia = await _contexto.Interdependencia.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Usuarios
        public IActionResult CreateInterdependence()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Interdependencia")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                //ViewBag.TipoContrato = drop[1];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInterdependence(Interdependencia interdependencia)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Interdependencia")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                //ViewBag.TipoContrato = drop[1];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                //if (empleado.TipoContratoId == null)
                //{
                //    ModelState.Remove("TipoContratoId");
                //}

                if (ModelState.IsValid)
                {
                    if (await _contexto.Interdependencia.AnyAsync(u => u.Documento == interdependencia.Documento))
                    {
                        ViewData["Mensaje"] = "Ya existe un estudiante con ese documento";
                        return View(interdependencia);
                    }

                    interdependencia.Estado = 1;

                    Interdependencia interdependencia_creado = await _userService.SaveInterdependence(interdependencia);

                    if (interdependencia_creado != null)
                    {
                        return RedirectToAction("AllInterdependences", "ManageInterdependences");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el estudiante";
                    return View(interdependencia);
                }

                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleStudent
        [HttpGet]
        public IActionResult DetailInterdependence(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var interdependencia = _contexto.Interdependencia.Find(doc);
                if (interdependencia == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Interdependencia")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });

                object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                //ViewBag.TipoContrato = drop[1];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(interdependencia.TipoCargoId);
                _contexto.TipoVinculacion.Find(interdependencia.TipoVinculacionId);

                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //EditStudent
        [HttpGet]
        public IActionResult EditInterdependence(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var interdependencia = _contexto.Interdependencia.Find(doc);
                if (interdependencia == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Interdependencia")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(interdependencia.TipoCargoId);
                _contexto.TipoVinculacion.Find(interdependencia.TipoVinculacionId);

                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInterdependence(Interdependencia interdependencia)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (ModelState.IsValid)
                {
                    interdependencia.Estado = 1;
                    _contexto.Update(interdependencia);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllInterdependences", "ManageInterdependences");

                    //// Obtener el usuario original de la base de datos
                    //var usuarioOriginal = _contexto.Usuario.Find(modelo.User);

                    //if (string.IsNullOrEmpty(modelo.Password))
                    //{
                    //    // Si la contraseña está vacía, se mantiene la misma
                    //    modelo.Password = usuarioOriginal.Password;
                    //}
                    //else
                    //{
                    //    // Si la contraseña no está vacía, se encripta la nueva contraseña
                    //    modelo.Password = Utilities.EncryptPassword(modelo.Password);
                    //}

                    //_contexto.Update(modelo);
                    //await _contexto.SaveChangesAsync();
                    //return RedirectToAction("AllUsers", "ManageUsers");
                }
                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //StudentEstatus
        [HttpGet]
        public IActionResult InterdependenceEstatus(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var interdependencia = _contexto.Interdependencia.Find(doc);
                if (interdependencia == null)
                {
                    return NotFound();
                }

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Interdependencia")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(interdependencia.TipoCargoId);
                _contexto.TipoVinculacion.Find(interdependencia.TipoVinculacionId);

                return View(interdependencia);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InterdependenceEstatus(int doc, int estado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var student = _contexto.Estudiante.Find(doc);
                if (student == null)
                {
                    return NotFound();
                }

                student.Estado = estado;

                _contexto.Update(student);
                await _contexto.SaveChangesAsync();

                return RedirectToAction("AllInterdependences", "ManageInterdependences");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }
    }
}
