using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ManageContractsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _contexto;

        public ManageContractsController(IUserService userService, ApplicationDbContext contexto)
        {
            _userService = userService;
            _contexto = contexto;
        }

        //MostrarContratos
        [HttpGet]
        public async Task<IActionResult> AllContracts()
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Contrato> contrato = await _contexto.Contrato.ToListAsync();
                //ViewBag.Usuario = usuarios;
                return View(contrato);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Contratos HISA");
                var row = 1;
                var column = 1;
                var dataTable = new DataTable();
                using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
                {
                    using (var command = new SqlCommand("ObtenerDatosEmpleado", connection))
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
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ContratosHISA.xlsx");
                //using (var stream = new MemoryStream())
                //{
                //    workbook.SaveAs(stream);
                //    var contentDisposition = new System.Net.Mime.ContentDisposition
                //    {
                //        FileName = "Contratos.xlsx",
                //        Inline = false
                //    };
                //    Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                //    stream.Position = 0;
                //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //}
            }

            //using (var workbook = new XLWorkbook())
            //{
            //    var worksheet = workbook.Worksheets.Add(dataTable, "Contratos");

            //    worksheet.Tables.FirstOrDefault()?.SetShowAutoFilter(false);

            //    worksheet.Columns().AdjustToContents();

            //    var fileName = "Contratos.xlsx";

            //    Response.Clear();
            //    Response.EnableBuffering();
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename={fileName}");

            //    using (var stream = new MemoryStream())
            //    {
            //        workbook.SaveAs(stream);
            //        stream.WriteTo(Response.Body);
            //        Response.Body.Flush();
            //        Response.CompleteAsync();
            //    }
            //}
        }

        //[HttpGet]
        //public IActionResult DownloadExcel()
        //{
        //    // Llamar al método que crea el archivo de Excel
        //    CreateExcelFile();

        //    // Descargar el archivo
        //    string fileName = "Contratos.xlsx";
        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        stream.CopyTo(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //}

        //private DataTable GetContractData()
        //{
        //    using (var connection = new SqlConnection("tu_cadena_de_conexion"))
        //    {
        //        using (var command = new SqlCommand("tu_procedimiento_almacenado", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            connection.Open();

        //            var dataTable = new DataTable();
        //            using (var dataAdapter = new SqlDataAdapter(command))
        //            {
        //                dataAdapter.Fill(dataTable);
        //            }

        //            return dataTable;
        //        }
        //    }
        //}
        //private void CreateExcelFile()
        //{
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Contratos HISA");
        //        var row = 1;
        //        var column = 1;
        //        var dataTable = new DataTable();
        //        using (var connection = new SqlConnection("Data Source=JEFF_PC\\SQLEXPRESS;Initial Catalog=DB_CV;Integrated Security=True;Encrypt=false"))
        //        {
        //            using (var command = new SqlCommand("ObtenerDatosEmpleado", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                using (var adapter = new SqlDataAdapter(command))
        //                {
        //                    adapter.Fill(dataTable);
        //                }
        //            }
        //        }
        //        foreach (DataColumn dataColumn in dataTable.Columns)
        //        {
        //            worksheet.Cell(row, column).Value = dataColumn.ColumnName;
        //            column++;
        //        }
        //        row++;
        //        column = 1;
        //        foreach (DataRow dataRow in dataTable.Rows)
        //        {
        //            foreach (DataColumn dataColumn in dataTable.Columns)
        //            {
        //                worksheet.Cell(row, column).Value = dataRow[dataColumn.ColumnName].ToString();
        //                column++;
        //            }
        //            row++;
        //            column = 1;
        //        }
        //        workbook.SaveAs("Contratos.xlsx");
        //    }
        //}

        //[HttpGet]
        //public IActionResult DownloadExcel()
        //{
        //    // Llamar al método que crea el archivo de Excel
        //    CreateExcelFile();

        //    // Descargar el archivo
        //    string fileName = "Contratos.xlsx";
        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        stream.CopyTo(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //}

        //MostrarDeUsuarioContratos
        [HttpGet]
        public async Task<IActionResult> AllEmployeeContracts(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            var employee = _contexto.Empleado.Find(doc);
            int documento = employee.Documento;
            ViewData["Documento"] = documento;

            if (userRol == "Admin" || userRol == "Observador")
            {
                List<Contrato> contrato = await _contexto.Contrato.Where(c => c.EmpleadoId == employee.Documento).ToListAsync();
                //ViewBag.Usuario = usuarios;
                if (contrato.Count > 0)
                {
                    foreach (var item in contrato)
                    {
                        var tipoContrato = _contexto.TipoContrato.Find(item.TipoContratoId);
                        var tipoCargo = _contexto.TipoCargo.Find(item.TipoCargoId);

                        item.Empleado = employee;
                        item.TipoContrato = tipoContrato;
                        item.TipoCargo = tipoCargo;

                    }
                }
                return View(contrato);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Contrato
        [HttpGet]
        public IActionResult CreateContract(int? doc)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                string primerNombre;
                string segundoNombre;
                string primerApellido;
                string segundoApellido;
                int documento;
                string lugarExpedicion;
                string fechaNacimiento;
                string sexo;
                int employeeId;


                object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.Eps = drop[3];
                ViewBag.FondoPensiones = drop[4];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                ViewBag.TipoCargo = drop[7];

                var empleado = _contexto.Empleado.Find(doc);

                if (empleado == null)
                {
                    return NotFound();
                }

                primerNombre = empleado.PrimerNombre;
                ViewData["primerNombre"] = primerNombre;

                segundoNombre = empleado.SegundoNombre;
                if (segundoNombre == null)
                {
                    segundoNombre = "ㅤ";
                }
                ViewData["segundoNombre"] = segundoNombre;

                primerApellido = empleado.PrimerApellido;
                ViewData["primerApellido"] = primerApellido;

                segundoApellido = empleado.SegundoApellido;
                if (segundoApellido == null)
                {
                    segundoApellido = "ㅤ";
                }
                ViewData["segundoApellido"] = segundoApellido;

                documento = empleado.Documento;
                ViewData["documento"] = documento;

                lugarExpedicion = empleado.LugarExpedicion;
                ViewData["lugarExpedicion"] = lugarExpedicion;

                fechaNacimiento = empleado.FechaNacimiento.ToString("dd-MM-yyyy");
                ViewData["fechaNacimiento"] = fechaNacimiento;

                sexo = empleado.Sexo;
                ViewData["sexo"] = sexo;

                employeeId = empleado.Documento;
                ViewData["idEmpleado"] = employeeId;

                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContract(Contrato contrato)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                object[] drop = Utilities.DropDownList(_contexto);
                ViewBag.TipoContrato = drop[1];
                ViewBag.Eps = drop[3];
                ViewBag.FondoPensiones = drop[4];
                ViewBag.TipoCargo = drop[7];

                //if (contrato.TipoContratoId == null)
                //{
                //    ModelState.Remove("TipoContratoId");
                //}

                if (ModelState.IsValid)
                {
                    //if (await _contexto.Usuario.AnyAsync(u => u.User == modelo.User))
                    //{
                    //    ViewData["Mensaje"] = "Ya existe un usuario con este nombre de usuario";
                    //    return View(modelo);
                    //}

                    Contrato contrract_created = await _userService.SaveContract(contrato);

                    if (contrract_created != null)
                    {
                        return RedirectToAction("AllEmployeeContracts", "ManageContracts");
                    }
                    ViewData["Mensaje"] = "No se pudo crear el contrato";
                    return View();
                }

                return View(contrato);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        //Crear Contrato
        [HttpGet]
        public IActionResult EditContract(int? id)
        {
            string userRol = Utilities.GetRol(HttpContext, _contexto);
            if (userRol == "Admin")
            {
                //string primerNombre;
                //string segundoNombre;
                //string primerApellido;
                //string segundoApellido;
                //int documento;
                //string lugarExpedicion;
                //string fechaNacimiento;
                //string sexo;
                //int employeeId;


                object[] drop = Utilities.DropDownList(_contexto);
                //ViewBag.TipoVinculo = drop[0];
                ViewBag.TipoContrato = drop[1];
                ViewBag.Eps = drop[3];
                ViewBag.FondoPensiones = drop[4];
                //ViewBag.TipoDocumento = drop[2];
                //ViewBag.Rol = drop[6];
                ViewBag.TipoCargo = drop[7];


                var contrato = _contexto.Contrato.Find(id);
                var empleado = _contexto.Empleado.Find(contrato.EmpleadoId);
                //var empleados = _contexto.Empleado.Find(_contexto.Contrato
                //      .Where(id => id. == datosPersonales.UsuarioId)
                //      .Select(u => u.TipoContratoId)
                //      .FirstOrDefault());
                //contrato = _contexto.Empleado.Include(c => c.) == contrato.EmpleadoId);
                //var empleado = _contexto.Empleado.Where(e => e.Documento == contrato.EmpleadoId);

                if (empleado == null)
                {
                    return NotFound();
                }

                //primerNombre = empleado.PrimerNombre;
                //ViewData["primerNombre"] = primerNombre;

                //segundoNombre = empleado.SegundoNombre;
                //if (segundoNombre == null)
                //{
                //    segundoNombre = "ㅤ";
                //}
                //ViewData["segundoNombre"] = segundoNombre;

                //primerApellido = empleado.PrimerApellido;
                //ViewData["primerApellido"] = primerApellido;

                //segundoApellido = empleado.SegundoApellido;
                //if (segundoApellido == null)
                //{
                //    segundoApellido = "ㅤ";
                //}
                //ViewData["segundoApellido"] = segundoApellido;

                //documento = empleado.Documento;
                //ViewData["documento"] = documento;

                //lugarExpedicion = empleado.LugarExpedicion;
                //ViewData["lugarExpedicion"] = lugarExpedicion;

                //fechaNacimiento = empleado.FechaNacimiento.ToString("dd-MM-yyyy");
                //ViewData["fechaNacimiento"] = fechaNacimiento;

                //sexo = empleado.Sexo;
                //ViewData["sexo"] = sexo;

                contrato.Empleado = empleado;
                //ViewData["idEmpleado"] = employeeId;

                return View();
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
