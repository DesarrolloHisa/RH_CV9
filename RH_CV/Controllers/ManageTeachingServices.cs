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
    public class ManageTeachingServices : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageTeachingServices(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllTeachingServices()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<DocenciaServicio> teachingService = await _contexto.DocenciaServicio.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(teachingService);
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
                                        .Where(tv => tv.Tipo == "Docencia Servicio")
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
        public async Task<IActionResult> CreateTeachingService(DocenciaServicio teachingService)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Docencia Servicio")
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
                    if (await _contexto.DocenciaServicio.AnyAsync(u => u.Documento == teachingService.Documento))
                    {
                        ViewData["Mensaje"] = "Ya existe un docente con ese documento";
                        return View(teachingService);
                    }

                    teachingService.Estado = 1;

                    DocenciaServicio teachingService_creado = await _userService.SaveTeachingService(teachingService);

                    if (teachingService_creado != null)
                    {
                        return RedirectToAction("AllTeachingServices", "ManageTeachingServices");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el docente";
                    return View(teachingService);
                }

                return View(teachingService);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleStudent
        [HttpGet]
        public IActionResult DetailTeachingService(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var teachingService = _contexto.DocenciaServicio.Find(doc);
                if (teachingService == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Docencia Servicio")
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

                _contexto.TipoCargo.Find(teachingService.TipoCargoId);
                _contexto.TipoVinculacion.Find(teachingService.TipoVinculacionId);

                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(teachingService);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //EditStudent
        [HttpGet]
        public IActionResult EditTeachingService(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var teachingService = _contexto.DocenciaServicio.Find(doc);
                if (teachingService == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Docencia Servicio")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(teachingService.TipoCargoId);
                _contexto.TipoVinculacion.Find(teachingService.TipoVinculacionId);

                return View(teachingService);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeachingService(DocenciaServicio teachingService)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (ModelState.IsValid)
                {
                    teachingService.Estado = 1;
                    _contexto.Update(teachingService);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllTeachingServices", "ManageTeachingServices");

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
                return View(teachingService);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //StudentEstatus
        [HttpGet]
        public IActionResult TeachingServiceEstatus(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var teachingService = _contexto.DocenciaServicio.Find(doc);
                if (teachingService == null)
                {
                    return NotFound();
                }

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Docencia Servicio")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                _contexto.TipoCargo.Find(teachingService.TipoCargoId);
                _contexto.TipoVinculacion.Find(teachingService.TipoVinculacionId);

                return View(teachingService);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeachingServiceEstatus(int doc, int estado)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                var teachingService = _contexto.DocenciaServicio.Find(doc);
                if (teachingService == null)
                {
                    return NotFound();
                }

                teachingService.Estado = estado;

                _contexto.Update(teachingService);
                await _contexto.SaveChangesAsync();

                return RedirectToAction("AllTeachingServices", "ManageTeachingServices");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //ExcelTeachingServices
        [HttpGet]
        public async Task<IActionResult> GetTeachingServicesExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Teaching Services HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetTeachingServices", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TeachingServicesHISA.xlsx");
            }
        }

        //ExcelActiveStuedents
        [HttpGet]
        public async Task<IActionResult> GetActiveTeachingServicesExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Teaching Services HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetActiveTeachingServices", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveTeachingServicesHISA.xlsx");
            }
        }
    }
}
