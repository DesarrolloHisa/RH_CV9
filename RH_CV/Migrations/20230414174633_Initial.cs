using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RH_CV.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosGenerales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComoSupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtrosIngresos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ingreso = table.Column<int>(type: "int", nullable: true),
                    ParientesTrabajando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoVivienda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosGenerales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Num1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Num2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Num3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionCompleta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Documento = table.Column<int>(type: "int", nullable: false),
                    LugarExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "DATE", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Documento);
                });

            migrationBuilder.CreateTable(
                name: "EPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FondoCesantias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondoCesantias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FondoPensiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondoPensiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonasACargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hijo = table.Column<int>(type: "int", nullable: false),
                    Conyugue = table.Column<int>(type: "int", nullable: false),
                    Padres = table.Column<int>(type: "int", nullable: false),
                    Otros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasACargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Practicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Programa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "DATE", nullable: true),
                    FechaFinalizacion = table.Column<DateTime>(type: "DATE", nullable: true),
                    DocenciaServicios = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVinculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVinculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    TipoCargoId = table.Column<int>(type: "int", nullable: false),
                    AreaFuncional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<int>(type: "int", nullable: false),
                    EPSId = table.Column<int>(type: "int", nullable: false),
                    FondoPensionesId = table.Column<int>(type: "int", nullable: true),
                    TipoContratoId = table.Column<int>(type: "int", nullable: true),
                    TiempoContratado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroMedico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "DATE", nullable: false),
                    FechaRetiro = table.Column<DateTime>(type: "DATE", nullable: true),
                    TiempoVinculacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoRetiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_EPS_EPSId",
                        column: x => x.EPSId,
                        principalTable: "EPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_Empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "Documento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_FondoPensiones_FondoPensionesId",
                        column: x => x.FondoPensionesId,
                        principalTable: "FondoPensiones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contrato_TipoCargo_TipoCargoId",
                        column: x => x.TipoCargoId,
                        principalTable: "TipoCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InfoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    PaisExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipioExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoDocumento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoDocumento_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVinculoId = table.Column<int>(type: "int", nullable: false),
                    TipoContratoId = table.Column<int>(type: "int", nullable: true),
                    InfoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.User);
                    table.ForeignKey(
                        name: "FK_Usuario_InfoDocumento_InfoDocumentoId",
                        column: x => x.InfoDocumentoId,
                        principalTable: "InfoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_TipoVinculo_TipoVinculoId",
                        column: x => x.TipoVinculoId,
                        principalTable: "TipoVinculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosPersonales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibretaMilitar = table.Column<int>(type: "int", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "DATE", nullable: false),
                    PaisNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipioNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionId = table.Column<int>(type: "int", nullable: false),
                    MunicipioResidencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estrato = table.Column<int>(type: "int", nullable: false),
                    ViveCon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrupoEtnico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonasACargoId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPSId = table.Column<int>(type: "int", nullable: false),
                    FondoPensionesId = table.Column<int>(type: "int", nullable: true),
                    FondoCesantiasId = table.Column<int>(type: "int", nullable: true),
                    DatosGeneralesId = table.Column<int>(type: "int", nullable: true),
                    PracticasId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosPersonales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosPersonales_DatosGenerales_DatosGeneralesId",
                        column: x => x.DatosGeneralesId,
                        principalTable: "DatosGenerales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatosPersonales_Direccion_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosPersonales_EPS_EPSId",
                        column: x => x.EPSId,
                        principalTable: "EPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosPersonales_FondoCesantias_FondoCesantiasId",
                        column: x => x.FondoCesantiasId,
                        principalTable: "FondoCesantias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatosPersonales_FondoPensiones_FondoPensionesId",
                        column: x => x.FondoPensionesId,
                        principalTable: "FondoPensiones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatosPersonales_PersonasACargo_PersonasACargoId",
                        column: x => x.PersonasACargoId,
                        principalTable: "PersonasACargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosPersonales_Practicas_PracticasId",
                        column: x => x.PracticasId,
                        principalTable: "Practicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatosPersonales_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactoEmergencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoEmergencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactoEmergencia_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosFamiliares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocupacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosFamiliares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosFamiliares_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escolaridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    Grado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolaridad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escolaridad_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoLaboral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRetiro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoRetiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<int>(type: "int", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoLaboral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoLaboral_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenciasFamiliares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenciasFamiliares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenciasFamiliares_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenciasPersonales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenciasPersonales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenciasPersonales_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactoEmergencia_DatosPersonalesId",
                table: "ContactoEmergencia",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EmpleadoId",
                table: "Contrato",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EPSId",
                table: "Contrato",
                column: "EPSId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_FondoPensionesId",
                table: "Contrato",
                column: "FondoPensionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoCargoId",
                table: "Contrato",
                column: "TipoCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoContratoId",
                table: "Contrato",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosFamiliares_DatosPersonalesId",
                table: "DatosFamiliares",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_DatosGeneralesId",
                table: "DatosPersonales",
                column: "DatosGeneralesId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_DireccionId",
                table: "DatosPersonales",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_EPSId",
                table: "DatosPersonales",
                column: "EPSId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_FondoCesantiasId",
                table: "DatosPersonales",
                column: "FondoCesantiasId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_FondoPensionesId",
                table: "DatosPersonales",
                column: "FondoPensionesId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_PersonasACargoId",
                table: "DatosPersonales",
                column: "PersonasACargoId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_PracticasId",
                table: "DatosPersonales",
                column: "PracticasId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_UsuarioId",
                table: "DatosPersonales",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Escolaridad_DatosPersonalesId",
                table: "Escolaridad",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoDocumento_TipoDocumentoId",
                table: "InfoDocumento",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoLaboral_DatosPersonalesId",
                table: "InfoLaboral",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciasFamiliares_DatosPersonalesId",
                table: "ReferenciasFamiliares",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciasPersonales_DatosPersonalesId",
                table: "ReferenciasPersonales",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_InfoDocumentoId",
                table: "Usuario",
                column: "InfoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoContratoId",
                table: "Usuario",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoVinculoId",
                table: "Usuario",
                column: "TipoVinculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactoEmergencia");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "DatosFamiliares");

            migrationBuilder.DropTable(
                name: "Escolaridad");

            migrationBuilder.DropTable(
                name: "InfoLaboral");

            migrationBuilder.DropTable(
                name: "ReferenciasFamiliares");

            migrationBuilder.DropTable(
                name: "ReferenciasPersonales");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "TipoCargo");

            migrationBuilder.DropTable(
                name: "DatosPersonales");

            migrationBuilder.DropTable(
                name: "DatosGenerales");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "EPS");

            migrationBuilder.DropTable(
                name: "FondoCesantias");

            migrationBuilder.DropTable(
                name: "FondoPensiones");

            migrationBuilder.DropTable(
                name: "PersonasACargo");

            migrationBuilder.DropTable(
                name: "Practicas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "InfoDocumento");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "TipoContrato");

            migrationBuilder.DropTable(
                name: "TipoVinculo");

            migrationBuilder.DropTable(
                name: "TipoDocumento");
        }
    }
}
