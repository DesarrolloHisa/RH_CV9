IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [DatosGenerales] (
        [Id] int NOT NULL IDENTITY,
        [ComoSupo] nvarchar(max) NULL,
        [OtrosIngresos] nvarchar(max) NULL,
        [Ingreso] int NULL,
        [ParientesTrabajando] nvarchar(max) NULL,
        [TipoVivienda] nvarchar(max) NULL,
        CONSTRAINT [PK_DatosGenerales] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Direccion] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        [Num1] nvarchar(max) NOT NULL,
        [Num2] nvarchar(max) NOT NULL,
        [Num3] nvarchar(max) NOT NULL,
        [Complemento] nvarchar(max) NULL,
        [DireccionCompleta] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Direccion] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Empleado] (
        [Documento] int NOT NULL,
        [LugarExpedicion] nvarchar(max) NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaNacimiento] DATE NOT NULL,
        [Sexo] nvarchar(max) NOT NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_Empleado] PRIMARY KEY ([Documento])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [EPS] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_EPS] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [FondoCesantias] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FondoCesantias] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [FondoPensiones] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FondoPensiones] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [PersonasACargo] (
        [Id] int NOT NULL IDENTITY,
        [Hijo] int NOT NULL,
        [Conyugue] int NOT NULL,
        [Padres] int NOT NULL,
        [Otros] int NOT NULL,
        CONSTRAINT [PK_PersonasACargo] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Practicas] (
        [Id] int NOT NULL IDENTITY,
        [Institucion] nvarchar(max) NULL,
        [Programa] nvarchar(max) NULL,
        [Titulo] nvarchar(max) NULL,
        [FechaInicio] DATE NULL,
        [FechaFinalizacion] DATE NULL,
        [DocenciaServicios] nvarchar(max) NULL,
        CONSTRAINT [PK_Practicas] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Rol] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Rol] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [TipoCargo] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoCargo] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [TipoContrato] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoContrato] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [TipoDocumento] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoDocumento] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [TipoVinculacion] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoVinculacion] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [TipoVinculo] (
        [Id] int NOT NULL IDENTITY,
        [Tipo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoVinculo] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Contrato] (
        [Id] int NOT NULL IDENTITY,
        [EmpleadoId] int NOT NULL,
        [TipoCargoId] int NOT NULL,
        [AreaFuncional] nvarchar(max) NOT NULL,
        [Salario] int NOT NULL,
        [EPSId] int NOT NULL,
        [FondoPensionesId] int NULL,
        [TipoContratoId] int NULL,
        [TiempoContratado] nvarchar(max) NOT NULL,
        [RegistroMedico] nvarchar(max) NULL,
        [FechaIngreso] DATE NOT NULL,
        [FechaRetiro] DATE NULL,
        [TiempoVinculacion] nvarchar(max) NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        CONSTRAINT [PK_Contrato] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Contrato_EPS_EPSId] FOREIGN KEY ([EPSId]) REFERENCES [EPS] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Contrato_Empleado_EmpleadoId] FOREIGN KEY ([EmpleadoId]) REFERENCES [Empleado] ([Documento]) ON DELETE CASCADE,
        CONSTRAINT [FK_Contrato_FondoPensiones_FondoPensionesId] FOREIGN KEY ([FondoPensionesId]) REFERENCES [FondoPensiones] ([Id]),
        CONSTRAINT [FK_Contrato_TipoCargo_TipoCargoId] FOREIGN KEY ([TipoCargoId]) REFERENCES [TipoCargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Contrato_TipoContrato_TipoContratoId] FOREIGN KEY ([TipoContratoId]) REFERENCES [TipoContrato] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [InfoDocumento] (
        [Id] int NOT NULL IDENTITY,
        [TipoDocumentoId] int NOT NULL,
        [PaisExpedicion] nvarchar(max) NOT NULL,
        [MunicipioExpedicion] nvarchar(max) NULL,
        CONSTRAINT [PK_InfoDocumento] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InfoDocumento_TipoDocumento_TipoDocumentoId] FOREIGN KEY ([TipoDocumentoId]) REFERENCES [TipoDocumento] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [DocenciaServicio] (
        [Documento] int NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaIngreso] DATE NOT NULL,
        [TipoVinculacionId] int NOT NULL,
        [TipoCargoId] int NOT NULL,
        [Institucion] nvarchar(max) NOT NULL,
        [AreaFuncional] nvarchar(max) NOT NULL,
        [FechaRetiro] DATE NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_DocenciaServicio] PRIMARY KEY ([Documento]),
        CONSTRAINT [FK_DocenciaServicio_TipoCargo_TipoCargoId] FOREIGN KEY ([TipoCargoId]) REFERENCES [TipoCargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DocenciaServicio_TipoVinculacion_TipoVinculacionId] FOREIGN KEY ([TipoVinculacionId]) REFERENCES [TipoVinculacion] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Estudiante] (
        [Documento] int NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaIngreso] DATE NOT NULL,
        [TipoVinculacionId] int NOT NULL,
        [TipoCargoId] int NOT NULL,
        [Institucion] nvarchar(max) NOT NULL,
        [AreaFuncional] nvarchar(max) NOT NULL,
        [FechaRetiro] DATE NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_Estudiante] PRIMARY KEY ([Documento]),
        CONSTRAINT [FK_Estudiante_TipoCargo_TipoCargoId] FOREIGN KEY ([TipoCargoId]) REFERENCES [TipoCargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Estudiante_TipoVinculacion_TipoVinculacionId] FOREIGN KEY ([TipoVinculacionId]) REFERENCES [TipoVinculacion] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Interdependencia] (
        [Documento] int NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaIngreso] DATE NOT NULL,
        [TipoVinculacionId] int NOT NULL,
        [TipoCargoId] int NOT NULL,
        [Institucion] nvarchar(max) NOT NULL,
        [AreaFuncional] nvarchar(max) NOT NULL,
        [FechaRetiro] DATE NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_Interdependencia] PRIMARY KEY ([Documento]),
        CONSTRAINT [FK_Interdependencia_TipoCargo_TipoCargoId] FOREIGN KEY ([TipoCargoId]) REFERENCES [TipoCargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Interdependencia_TipoVinculacion_TipoVinculacionId] FOREIGN KEY ([TipoVinculacionId]) REFERENCES [TipoVinculacion] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Voluntario] (
        [Documento] int NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaIngreso] DATE NOT NULL,
        [TipoVinculacionId] int NOT NULL,
        [TipoCargoId] int NOT NULL,
        [AreaFuncional] nvarchar(max) NOT NULL,
        [FechaRetiro] DATE NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_Voluntario] PRIMARY KEY ([Documento]),
        CONSTRAINT [FK_Voluntario_TipoCargo_TipoCargoId] FOREIGN KEY ([TipoCargoId]) REFERENCES [TipoCargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Voluntario_TipoVinculacion_TipoVinculacionId] FOREIGN KEY ([TipoVinculacionId]) REFERENCES [TipoVinculacion] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Usuario] (
        [User] nvarchar(450) NOT NULL,
        [RolId] int NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [TipoVinculoId] int NOT NULL,
        [TipoContratoId] int NULL,
        [InfoDocumentoId] int NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [Estado] int NOT NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY ([User]),
        CONSTRAINT [FK_Usuario_InfoDocumento_InfoDocumentoId] FOREIGN KEY ([InfoDocumentoId]) REFERENCES [InfoDocumento] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Usuario_Rol_RolId] FOREIGN KEY ([RolId]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Usuario_TipoContrato_TipoContratoId] FOREIGN KEY ([TipoContratoId]) REFERENCES [TipoContrato] ([Id]),
        CONSTRAINT [FK_Usuario_TipoVinculo_TipoVinculoId] FOREIGN KEY ([TipoVinculoId]) REFERENCES [TipoVinculo] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [DatosPersonales] (
        [Id] int NOT NULL IDENTITY,
        [UsuarioId] nvarchar(450) NOT NULL,
        [LibretaMilitar] int NULL,
        [FechaNacimiento] DATE NOT NULL,
        [PaisNacimiento] nvarchar(max) NOT NULL,
        [MunicipioNacimiento] nvarchar(max) NULL,
        [Celular] int NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Sexo] nvarchar(max) NOT NULL,
        [DireccionId] int NOT NULL,
        [MunicipioResidencia] nvarchar(max) NOT NULL,
        [Estrato] int NOT NULL,
        [ViveCon] nvarchar(max) NOT NULL,
        [GrupoEtnico] nvarchar(max) NOT NULL,
        [PersonasACargoId] int NOT NULL,
        [EstadoCivil] nvarchar(max) NOT NULL,
        [EPSId] int NOT NULL,
        [FondoPensionesId] int NULL,
        [FondoCesantiasId] int NULL,
        [DatosGeneralesId] int NULL,
        [PracticasId] int NULL,
        [FechaCreacion] DATE NOT NULL,
        CONSTRAINT [PK_DatosPersonales] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DatosPersonales_DatosGenerales_DatosGeneralesId] FOREIGN KEY ([DatosGeneralesId]) REFERENCES [DatosGenerales] ([Id]),
        CONSTRAINT [FK_DatosPersonales_Direccion_DireccionId] FOREIGN KEY ([DireccionId]) REFERENCES [Direccion] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DatosPersonales_EPS_EPSId] FOREIGN KEY ([EPSId]) REFERENCES [EPS] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DatosPersonales_FondoCesantias_FondoCesantiasId] FOREIGN KEY ([FondoCesantiasId]) REFERENCES [FondoCesantias] ([Id]),
        CONSTRAINT [FK_DatosPersonales_FondoPensiones_FondoPensionesId] FOREIGN KEY ([FondoPensionesId]) REFERENCES [FondoPensiones] ([Id]),
        CONSTRAINT [FK_DatosPersonales_PersonasACargo_PersonasACargoId] FOREIGN KEY ([PersonasACargoId]) REFERENCES [PersonasACargo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DatosPersonales_Practicas_PracticasId] FOREIGN KEY ([PracticasId]) REFERENCES [Practicas] ([Id]),
        CONSTRAINT [FK_DatosPersonales_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([User]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [ContactoEmergencia] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [Nombre] nvarchar(max) NOT NULL,
        [Parentesco] nvarchar(max) NOT NULL,
        [Celular] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ContactoEmergencia] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactoEmergencia_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [DatosFamiliares] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [Nombre] nvarchar(max) NULL,
        [FechaNacimiento] datetime2 NULL,
        [Parentesco] nvarchar(max) NULL,
        [Ocupacion] nvarchar(max) NULL,
        CONSTRAINT [PK_DatosFamiliares] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DatosFamiliares_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [Escolaridad] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [Grado] nvarchar(max) NOT NULL,
        [Titulo] nvarchar(max) NOT NULL,
        [Institucion] nvarchar(max) NOT NULL,
        [Year] int NOT NULL,
        CONSTRAINT [PK_Escolaridad] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Escolaridad_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [InfoLaboral] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [FechaIngreso] datetime2 NULL,
        [FechaRetiro] datetime2 NULL,
        [NombreEmpresa] nvarchar(max) NULL,
        [MotivoRetiro] nvarchar(max) NULL,
        [Celular] int NULL,
        [Cargo] nvarchar(max) NULL,
        CONSTRAINT [PK_InfoLaboral] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InfoLaboral_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [ReferenciasFamiliares] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [Nombre] nvarchar(max) NOT NULL,
        [Parentesco] nvarchar(max) NOT NULL,
        [Celular] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ReferenciasFamiliares] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReferenciasFamiliares_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE TABLE [ReferenciasPersonales] (
        [Id] int NOT NULL IDENTITY,
        [DatosPersonalesId] int NOT NULL,
        [Nombre] nvarchar(max) NOT NULL,
        [Parentesco] nvarchar(max) NOT NULL,
        [Celular] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ReferenciasPersonales] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReferenciasPersonales_DatosPersonales_DatosPersonalesId] FOREIGN KEY ([DatosPersonalesId]) REFERENCES [DatosPersonales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_ContactoEmergencia_DatosPersonalesId] ON [ContactoEmergencia] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Contrato_EmpleadoId] ON [Contrato] ([EmpleadoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Contrato_EPSId] ON [Contrato] ([EPSId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Contrato_FondoPensionesId] ON [Contrato] ([FondoPensionesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Contrato_TipoCargoId] ON [Contrato] ([TipoCargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Contrato_TipoContratoId] ON [Contrato] ([TipoContratoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosFamiliares_DatosPersonalesId] ON [DatosFamiliares] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_DatosGeneralesId] ON [DatosPersonales] ([DatosGeneralesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_DireccionId] ON [DatosPersonales] ([DireccionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_EPSId] ON [DatosPersonales] ([EPSId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_FondoCesantiasId] ON [DatosPersonales] ([FondoCesantiasId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_FondoPensionesId] ON [DatosPersonales] ([FondoPensionesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_PersonasACargoId] ON [DatosPersonales] ([PersonasACargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_PracticasId] ON [DatosPersonales] ([PracticasId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DatosPersonales_UsuarioId] ON [DatosPersonales] ([UsuarioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DocenciaServicio_TipoCargoId] ON [DocenciaServicio] ([TipoCargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_DocenciaServicio_TipoVinculacionId] ON [DocenciaServicio] ([TipoVinculacionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Escolaridad_DatosPersonalesId] ON [Escolaridad] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Estudiante_TipoCargoId] ON [Estudiante] ([TipoCargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Estudiante_TipoVinculacionId] ON [Estudiante] ([TipoVinculacionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_InfoDocumento_TipoDocumentoId] ON [InfoDocumento] ([TipoDocumentoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_InfoLaboral_DatosPersonalesId] ON [InfoLaboral] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Interdependencia_TipoCargoId] ON [Interdependencia] ([TipoCargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Interdependencia_TipoVinculacionId] ON [Interdependencia] ([TipoVinculacionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_ReferenciasFamiliares_DatosPersonalesId] ON [ReferenciasFamiliares] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_ReferenciasPersonales_DatosPersonalesId] ON [ReferenciasPersonales] ([DatosPersonalesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Usuario_InfoDocumentoId] ON [Usuario] ([InfoDocumentoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Usuario_RolId] ON [Usuario] ([RolId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Usuario_TipoContratoId] ON [Usuario] ([TipoContratoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Usuario_TipoVinculoId] ON [Usuario] ([TipoVinculoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Voluntario_TipoCargoId] ON [Voluntario] ([TipoCargoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    CREATE INDEX [IX_Voluntario_TipoVinculacionId] ON [Voluntario] ([TipoVinculacionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230421161316_New')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230421161316_New', N'7.0.4');
END;
GO

COMMIT;
GO

