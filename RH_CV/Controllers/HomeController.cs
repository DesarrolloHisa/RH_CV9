using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH_CV.Data;
using RH_CV.Models;
using System.Diagnostics;
using System.Security.Claims;
using RH_CV.Sources;

namespace RH_CV.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        //private readonly UserManager<ApplicationDbContext> _userManager;

        public HomeController(ApplicationDbContext contexto/*, UserManager<ApplicationDbContext> userManager*/)
        {
            _contexto = contexto;
            //_userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
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

            string userRol = Utilities.GetRol(HttpContext, _contexto);

            string name = Utilities.GetName(HttpContext, _contexto);

            ////ViewBag.UserId = userId;
            ViewData["userRol"] = userRol;

            ViewBag.UserName = name;
            //HttpContext.Session.SetString("UserRol", userRol);

            return View(/*"Index", userRol*/);
        }

        public IActionResult ManageEmployeesMenu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "InicioSesion");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}