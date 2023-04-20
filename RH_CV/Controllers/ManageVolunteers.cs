using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace RH_CV.Controllers
{
    [Authorize]
    public class ManageVolunteers : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageVolunteers(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllVolunteers()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Voluntario> volunteer = await _contexto.Voluntario.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Usuarios
        public IActionResult CreateTeachingService()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Voluntario")
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
        public async Task<IActionResult> CreateVolunteers(Voluntario volunteer)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Voluntario")
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
                    if (await _contexto.Voluntario.AnyAsync(u => u.Documento == volunteer.Documento))
                    {
                        ViewData["Mensaje"] = "Ya existe un docente con ese documento";
                        return View(volunteer);
                    }

                    volunteer.Estado = 1;

                    Voluntario volunteer_creado = await _userService.SaveVolunteer(volunteer);

                    if (volunteer_creado != null)
                    {
                        return RedirectToAction("AllVolunteers", "ManageVolunteers");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el docente";
                    return View(volunteer);
                }

                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleStudent
        [HttpGet]
        public IActionResult DetailVolunteer(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var volunteer = _contexto.Voluntario.Find(doc);
                if (volunteer == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Voluntario")
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

                _contexto.TipoCargo.Find(volunteer.TipoCargoId);
                _contexto.TipoVinculacion.Find(volunteer.TipoVinculacionId);

                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //EditStudent
        [HttpGet]
        public IActionResult EditVolunteer(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var volunteer = _contexto.Voluntario.Find(doc);
                if (volunteer == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Voluntario")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(volunteer.TipoCargoId);
                _contexto.TipoVinculacion.Find(volunteer.TipoVinculacionId);

                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVolunteer(Voluntario volunteer)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (ModelState.IsValid)
                {
                    volunteer.Estado = 1;
                    _contexto.Update(volunteer);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllVolunteers", "ManageVolunteers");

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
                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //StudentEstatus
        [HttpGet]
        public IActionResult VolunteerEstatus(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var volunteer = _contexto.Voluntario.Find(doc);
                if (volunteer == null)
                {
                    return NotFound();
                }

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Voluntario")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(volunteer.TipoCargoId);
                _contexto.TipoVinculacion.Find(volunteer.TipoVinculacionId);

                return View(volunteer);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VolunteerEstatus(int doc, int estado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var volunteer = _contexto.Voluntario.Find(doc);
                if (volunteer == null)
                {
                    return NotFound();
                }

                volunteer.Estado = estado;

                _contexto.Update(volunteer);
                await _contexto.SaveChangesAsync();

                return RedirectToAction("AllVolunteers", "ManageVolunteers");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //ExcelStudents
        [HttpGet]
        public async Task<IActionResult> GetVolunteersExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Volunteers HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetVolunteers", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VolunteersHISA.xlsx");
            }
        }

        //ExcelActiveStuedents
        [HttpGet]
        public async Task<IActionResult> GetActiveVolunteersExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Active Volunteers HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetActiveVolunteers", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveVolunteersHISA.xlsx");
            }
        }
    }
}
