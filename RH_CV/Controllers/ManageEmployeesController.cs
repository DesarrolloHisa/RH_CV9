using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;

namespace RH_CV.Controllers
{
    [Authorize]
    public class ManageEmployeesController : Controller 
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageEmployeesController(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllEmployees()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Empleado> empleados = await _contexto.Empleado.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(empleados);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Usuarios
        public IActionResult CreateEmployee()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                //object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                //ViewBag.TipoContrato = drop[1];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                //ViewBag.TipoCargo = drop[7];
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(Empleado empleado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                //object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                //ViewBag.TipoContrato = drop[1];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];

                //if (empleado.TipoContratoId == null)
                //{
                //    ModelState.Remove("TipoContratoId");
                //}

                if (ModelState.IsValid)
                {
                    if (await _contexto.Empleado.AnyAsync(u => u.Documento == empleado.Documento))
                    {
                        ViewData["Mensaje"] = "Ya existe un usuario con ese documento";
                        return View(empleado);
                    }

                    empleado.Estado = 1;

                    Empleado empleado_creado = await _userService.SaveEmpleado(empleado);

                    if (empleado_creado != null)
                    {
                        return RedirectToAction("AllEmployees", "ManageEmployees");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el usuario";
                    return View(empleado);
                }

                return View(empleado);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleUsuarios
        [HttpGet]
        public IActionResult DetailEmployee(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var employee = _contexto.Empleado.Find(doc);
                if (employee == null)
                {
                    return NotFound();
                }

                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(employee);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetailEmployee
        [HttpGet]
        public IActionResult EditEmployee(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var employee = _contexto.Empleado.Find(doc);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Empleado empleado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (ModelState.IsValid)
                {
                    empleado.Estado = 1;
                    _contexto.Update(empleado);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllEmployees", "ManageEmployees");

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
                return View(empleado);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        ////DetalleUsuarios
        //[HttpGet]
        //public IActionResult DetailUser(string? user)
        //{
        //    string userRol = Utilities.GetRol(HttpContext, _contexto);
        //    if (userRol == "Admin")
        //    {
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        var usuario = _contexto.Usuario.Find(user);
        //        if (usuario == null)
        //        {
        //            return NotFound();
        //        }

        //        _contexto.Rol.Find(usuario.RolId);
        //        _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
        //        _contexto.TipoContrato.Find(usuario.TipoContratoId);
        //        var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
        //        _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
        //        //if (rol == null)
        //        //{
        //        //    return NotFound();
        //        //}
        //        //ViewData["RolTipo"] = rol.Tipo;

        //        return View(usuario);
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Home");
        //    }
        //}

        ////EditarUsuarios
        //[HttpGet]
        //public IActionResult EditUser(string? user)
        //{
        //    string userRol = Utilities.GetRol(HttpContext, _contexto);
        //    if (userRol == "Admin")
        //    {
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        var usuario = _contexto.Usuario.Find(user);
        //        if (usuario == null)
        //        {
        //            return NotFound();
        //        }

        //        object[] drop = Utilities.DropDownList(_contexto);
        //        ViewBag.TipoVinculo = drop[0];
        //        ViewBag.TipoContrato = drop[1];
        //        ViewBag.TipoDocumento = drop[2];
        //        ViewBag.Rol = drop[6];

        //        _contexto.Rol.Find(usuario.RolId);
        //        _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
        //        _contexto.TipoContrato.Find(usuario.TipoContratoId);
        //        var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
        //        _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);

        //        return View(usuario);
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Home");
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditUser(Usuario modelo)
        //{
        //    string userRol = Utilities.GetRol(HttpContext, _contexto);
        //    if (userRol == "Admin")
        //    {
        //        var roles = _contexto.Rol.Select(r => new SelectListItem
        //        {
        //            Value = r.Id.ToString(),
        //            Text = r.Tipo,
        //        });
        //        ViewBag.Roles = roles;
        //        bool change = true;
        //        if (modelo.Password == null)
        //        {
        //            var usuario = _contexto.Usuario.Find(modelo.User);
        //            modelo.Password = usuario.Password;
        //            change = false;
        //            _contexto.Entry(usuario).State = EntityState.Detached;
        //            ModelState.Remove("Password");
        //        }

        //        if (modelo.TipoContratoId == null)
        //        {
        //            ModelState.Remove("TipoContratoId");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            if (change)
        //            {
        //                modelo.Password = Utilities.EncryptPassword(modelo.Password);
        //            }
        //            _contexto.Update(modelo);
        //            await _contexto.SaveChangesAsync();
        //            return RedirectToAction("AllUsers", "ManageUsers");

        //            //// Obtener el usuario original de la base de datos
        //            //var usuarioOriginal = _contexto.Usuario.Find(modelo.User);

        //            //if (string.IsNullOrEmpty(modelo.Password))
        //            //{
        //            //    // Si la contraseña está vacía, se mantiene la misma
        //            //    modelo.Password = usuarioOriginal.Password;
        //            //}
        //            //else
        //            //{
        //            //    // Si la contraseña no está vacía, se encripta la nueva contraseña
        //            //    modelo.Password = Utilities.EncryptPassword(modelo.Password);
        //            //}

        //            //_contexto.Update(modelo);
        //            //await _contexto.SaveChangesAsync();
        //            //return RedirectToAction("AllUsers", "ManageUsers");
        //        }

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Home");
        //    }
        //}
    }
}
