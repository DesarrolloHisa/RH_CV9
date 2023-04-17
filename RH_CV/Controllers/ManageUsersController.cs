using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Services.implementation;
using RH_CV.Sources;
using System.Diagnostics.Contracts;

namespace RH_CV.Controllers
{
    //[Authorize(Roles = "1")]
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageUsersController(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                List<Usuario> usuarios = await _contexto.Usuario.Include(x => x.Rol).ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Usuarios
        public IActionResult CreateUser()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                //var roles = _contexto.Rol.Select(r => new SelectListItem
                //{
                //    Value = r.Id.ToString(),
                //    Text = r.Tipo,
                //});
                //ViewBag.Roles = roles;
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.TipoDocumento = drop[2];
                ViewBag.Rol = drop[6];
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(Usuario modelo)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.TipoDocumento = drop[2];
                ViewBag.Rol = drop[6];

                //var roles = _contexto.Rol.Select(r => new SelectListItem
                //{
                //    Value = r.Id.ToString(),
                //    Text = r.Tipo,
                //});
                //ViewBag.Roles = roles;
                if (modelo.TipoContratoId == null)
                {
                    ModelState.Remove("TipoContratoId");
                }

                if (ModelState.IsValid)
                {
                    if (await _contexto.Usuario.AnyAsync(u => u.User == modelo.User))
                    {
                        ViewData["Mensaje"] = "Ya existe un usuario con este nombre de usuario";
                        return View(modelo);
                    }

                    modelo.Password = Utilities.EncryptPassword(modelo.Password);

                    modelo.Estado = 1;

                    Usuario usuario_creado = await _userService.SaveUsuario(modelo);

                    if (usuario_creado.User != "")
                    {
                        return RedirectToAction("AllUsers", "ManageUsers");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el usuario";
                    return View();
                }

                return View(modelo);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleUsuarios
        [HttpGet]
        public IActionResult DetailUser(string? user)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (user == null)
                {
                    return NotFound();
                }

                var usuario = _contexto.Usuario.Find(user);
                if (usuario == null)
                {
                    return NotFound();
                }

                _contexto.Rol.Find(usuario.RolId);
                _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
                _contexto.TipoContrato.Find(usuario.TipoContratoId);
                var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
                _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);
                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(usuario);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //EditarUsuarios
        [HttpGet]
        public IActionResult EditUser(string? user)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (user == null)
                {
                    return NotFound();
                }

                var usuario = _contexto.Usuario.Find(user);
                if (usuario == null)
                {
                    return NotFound();
                }

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.TipoDocumento = drop[2];
                ViewBag.Rol = drop[6];

                _contexto.Rol.Find(usuario.RolId);
                _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
                _contexto.TipoContrato.Find(usuario.TipoContratoId);
                var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
                _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);

                return View(usuario);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Usuario modelo)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var roles = _contexto.Rol.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Tipo,
                });
                ViewBag.Roles = roles;

                bool change = true;
                if (modelo.Password== null)
                {
                    var usuario = _contexto.Usuario.Find(modelo.User);
                    modelo.Password = usuario.Password;
                    change = false;
                    _contexto.Entry(usuario).State = EntityState.Detached;
                    ModelState.Remove("Password");
                }

                if (modelo.TipoContratoId == null)
                {
                    ModelState.Remove("TipoContratoId");
                }

                if (ModelState.IsValid)
                {
                    if (change)
                    {
                        modelo.Password = Utilities.EncryptPassword(modelo.Password);
                    }
                    modelo.Estado = 1;
                    _contexto.Update(modelo);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllUsers", "ManageUsers");

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

                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DehabilitarUsuarios
        [HttpGet]
        public IActionResult UserEstatus(string? user)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (user == null)
                {
                    return NotFound();
                }

                var usuario = _contexto.Usuario.Find(user);
                if (usuario == null)
                {
                    return NotFound();
                }

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.TipoDocumento = drop[2];
                ViewBag.Rol = drop[6];

                _contexto.Rol.Find(usuario.RolId);
                _contexto.TipoVinculo.Find(usuario.TipoVinculoId);
                _contexto.TipoContrato.Find(usuario.TipoContratoId);
                var infoDocumento = _contexto.InfoDocumento.Find(usuario.InfoDocumentoId);
                _contexto.TipoDocumento.Find(infoDocumento.TipoDocumentoId);

                return View(usuario);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEstatus(string userId, int estado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var usuario = _contexto.Usuario.Find(userId);
                if (usuario == null)
                {
                    return NotFound();
                }

                usuario.Estado = estado;

                _contexto.Update(usuario);
                await _contexto.SaveChangesAsync();

                return RedirectToAction("AllUsers", "ManageUsers");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UserEstatus(Usuario modelo)
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

        //        if (ModelState.IsValid)
        //        {
        //            if (change)
        //            {
        //                modelo.Password = Utilities.EncryptPassword(modelo.Password);
        //            }
        //            _contexto.Update(modelo);
        //            await _contexto.SaveChangesAsync();
        //            return RedirectToAction("AllUsers", "ManageUsers");
        //        }

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Home");
        //    }
        //}

        //BorrarUsuario
        [HttpGet]
        public IActionResult DeleteUser(string? user)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (user == null)
                {
                    return NotFound();
                }

                var usuario = _contexto.Usuario.Find(user);
                if (usuario == null)
                {
                    return NotFound();
                }

                var rol = _contexto.Rol.Find(usuario.RolId);
                if (rol == null)
                {
                    return NotFound();
                }
                ViewData["RolTipo"] = rol.Tipo;

                return View(usuario);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserAction(string? user)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var usuario = await _contexto.Usuario.FindAsync(user);
                if (usuario == null)
                {
                    return View();
                }
                //Borrado
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("AllUsers", "ManageUsers");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }
    }
}
