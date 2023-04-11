using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;
using System.Security.Claims;

namespace RH_CV.Controllers
{
    public class InicioSesionController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        private readonly IUserService _userService;
        public InicioSesionController(IUserService userService, ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _userService = userService;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string user, string password)
        {
            Usuario usuario_encontrado = await _userService.GetUsuario(user, Utilities.EncryptPassword(password));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            if (usuario_encontrado.Estado == 0)
            {
                ViewData["Mensaje"] = "Usuario Desabilitado";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.User) };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            ////ControlUsuarios
            //ClaimsPrincipal claimsRol = HttpContext.User;
            //string userId = "";
            //userId = claimsRol.Claims.Where(c => c.Type == ClaimTypes.Name)
            //        .Select(c => c.Value).SingleOrDefault();

            //// Obtener el rol del usuario
            //Rol rol = _contexto.Usuario.Where(u => u.User == userId).Select(u => u.Rol).FirstOrDefault();

            //// Verificar si el usuario es un administrador
            //string userRol = "";
            //if (rol != null && rol.Id == 1)
            //{
            //    userRol = "Admin";
            //}
            //else if (rol != null && rol.Id == 2)
            //{
            //    userRol = "Observador";
            //}
            //else { userRol = "Empleado"; }

            //////ViewBag.UserId = userId;
            //ViewData["userRol"] = userRol;

            return RedirectToAction("Index", "Home");
        }
    }
}
