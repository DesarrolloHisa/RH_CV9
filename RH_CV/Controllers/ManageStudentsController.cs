using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RH_CV.Data;
using RH_CV.Models;
using RH_CV.Services.Contract;
using RH_CV.Sources;
using System.Data;

namespace RH_CV.Controllers
{
    [Authorize]
    public class ManageStudentsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageStudentsController(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarUsuarios
        [HttpGet]
        public async Task<IActionResult> AllStudents()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Estudiante> estudiantes = await _contexto.Estudiante.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(estudiantes);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Usuarios
        public IActionResult CreateStudent()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Convenio Especifico de Practicas Academicas"
                                                        || tv.Tipo == "Convenio de Practica Pedagogica"
                                                        || tv.Tipo == "Convenio Interinstitucional de Practicas Academica"
                                                        || tv.Tipo == "Convenio Docencia Servicio")
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
        public async Task<IActionResult> CreateStudent(Estudiante estudiante)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Convenio Especifico de Practicas Academicas"
                                                        || tv.Tipo == "Convenio de Practica Pedagogica"
                                                        || tv.Tipo == "Convenio Interinstitucional de Practicas Academicasa"
                                                        || tv.Tipo == "Convenio Docencia Servicio")
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
                    if (await _contexto.Estudiante.AnyAsync(u => u.Documento == estudiante.Documento))
                    {
                        ViewData["Mensaje"] = "Ya existe un estudiante con ese documento";
                        return View(estudiante);
                    }

                    estudiante.Estado = 1;

                    Estudiante estudiante_creado = await _userService.SaveStudent(estudiante);

                    if (estudiante_creado != null)
                    {
                        return RedirectToAction("AllStudents", "ManageStudents");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el estudiante";
                    return View(estudiante);
                }

                return View(estudiante);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //DetalleStudent
        [HttpGet]
        public IActionResult DetailStudent(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var student = _contexto.Estudiante.Find(doc);
                if (student == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Convenio Especifico de Practicas Academicas"
                                                        || tv.Tipo == "Convenio de Practica Pedagogica"
                                                        || tv.Tipo == "Convenio Interinstitucional de Practicas Academicasa"
                                                        || tv.Tipo == "Convenio Docencia Servicio")
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

                //_contexto.TipoCargo.Find(student.TipoCargoId);
                _contexto.TipoVinculacion.Find(student.TipoVinculacionId);

                //if (rol == null)
                //{
                //    return NotFound();
                //}
                //ViewData["RolTipo"] = rol.Tipo;

                return View(student);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //EditStudent
        [HttpGet]
        public IActionResult EditStudent(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var student = _contexto.Estudiante.Find(doc);
                if (student == null)
                {
                    return NotFound();
                }
                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Convenio Especifico de Practicas Academicas"
                                                        || tv.Tipo == "Convenio de Practica Pedagogica"
                                                        || tv.Tipo == "Convenio Interinstitucional de Practicas Academicasa"
                                                        || tv.Tipo == "Convenio Docencia Servicio")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                //_contexto.TipoCargo.Find(student.TipoCargoId);
                _contexto.TipoVinculacion.Find(student.TipoVinculacionId);

                return View(student);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(Estudiante estudiante)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (ModelState.IsValid)
                {
                    estudiante.Estado = 1;
                    _contexto.Update(estudiante);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("AllStudents", "ManageStudents");

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
                return View(estudiante);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //StudentEstatus
        [HttpGet]
        public IActionResult StudentEstatus(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                if (doc == null)
                {
                    return NotFound();
                }

                var student = _contexto.Estudiante.Find(doc);
                if (student == null)
                {
                    return NotFound();
                }

                ViewBag.TipoVinculacion = _contexto.TipoVinculacion
                                        .Where(tv => tv.Tipo == "Convenio Especifico de Practicas Academicas"
                                                        || tv.Tipo == "Convenio de Practica Pedagogica"
                                                        || tv.Tipo == "Convenio Interinstitucional de Practicas Academicasa"
                                                        || tv.Tipo == "Convenio Docencia Servicio")
                                        .Select(tv => new SelectListItem
                                        {
                                            Value = tv.Id.ToString(),
                                            Text = tv.Tipo
                                        });

                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoCargo = drop[7];
                //ViewBag.TipoVinculacion = drop[8];

                //_contexto.TipoCargo.Find(student.TipoCargoId);
                _contexto.TipoVinculacion.Find(student.TipoVinculacionId);

                return View(student);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentEstatus(int doc, int estado)
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

                return RedirectToAction("AllStudents", "ManageStudents");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //ExcelStudents
        [HttpGet]
        public async Task<IActionResult> GetStudentsExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetStudents", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentsHISA.xlsx");
            }
        }

        //ExcelActiveStuedents
        [HttpGet]
        public async Task<IActionResult> GetActiveStudentsExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Active Students HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                //using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                //using (var connection = new SqlConnection("Data Source=DESARROLLOHISA;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                using (var connection = new SqlConnection("Data Source=SERVER01;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("GetActiveStudents", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveStudentsHISA.xlsx");
            }
        }
    }
}
