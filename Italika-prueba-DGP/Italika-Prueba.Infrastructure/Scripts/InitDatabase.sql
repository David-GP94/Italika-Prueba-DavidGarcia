-- Crear tabla Escuelas si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Escuelas]') AND type in (N'U'))
BEGIN
    CREATE TABLE Escuelas (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(500)
    );
END;

-- Crear tabla Profesores si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profesores]') AND type in (N'U'))
BEGIN
    CREATE TABLE Profesores (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Nombre NVARCHAR(50) NOT NULL,
        ApellidoPaterno NVARCHAR(50) NOT NULL,
        ApellidoMaterno NVARCHAR(50),
        EscuelaId UNIQUEIDENTIFIER,
        FOREIGN KEY (EscuelaId) REFERENCES Escuelas(Id)
    );
END;

-- Crear tabla Alumnos si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alumnos]') AND type in (N'U'))
BEGIN
    CREATE TABLE Alumnos (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Nombre NVARCHAR(50) NOT NULL,
        ApellidoPaterno NVARCHAR(50) NOT NULL,
        ApellidoMaterno NVARCHAR(50),
        FechaNacimiento DATE NOT NULL
    );
END;

-- Crear tabla AlumnoProfesor si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlumnoProfesor]') AND type in (N'U'))
BEGIN
    CREATE TABLE AlumnoProfesor (
        AlumnoId UNIQUEIDENTIFIER,
        ProfesorId UNIQUEIDENTIFIER,
        PRIMARY KEY (AlumnoId, ProfesorId),
        FOREIGN KEY (AlumnoId) REFERENCES Alumnos(Id),
        FOREIGN KEY (ProfesorId) REFERENCES Profesores(Id)
    );
END;

-- Crear tabla AlumnoEscuela si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlumnoEscuela]') AND type in (N'U'))
BEGIN
    CREATE TABLE AlumnoEscuela (
        AlumnoId UNIQUEIDENTIFIER,
        EscuelaId UNIQUEIDENTIFIER,
        PRIMARY KEY (AlumnoId, EscuelaId),
        FOREIGN KEY (AlumnoId) REFERENCES Alumnos(Id),
        FOREIGN KEY (EscuelaId) REFERENCES Escuelas(Id)
    );
END;

-- Stored Procedures para Escuelas
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CrearEscuela')
BEGIN
    EXEC('CREATE PROCEDURE sp_CrearEscuela
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @Descripcion NVARCHAR(500)
    AS
    BEGIN
        INSERT INTO Escuelas (Id, Nombre, Descripcion)
        VALUES (@Id, @Nombre, @Descripcion);
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ObtenerEscuela')
BEGIN
    EXEC('CREATE PROCEDURE sp_ObtenerEscuela
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        SELECT Id, Nombre, Descripcion
        FROM Escuelas
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ActualizarEscuela')
BEGIN
    EXEC('CREATE PROCEDURE sp_ActualizarEscuela
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @Descripcion NVARCHAR(500)
    AS
    BEGIN
        UPDATE Escuelas
        SET Nombre = @Nombre, Descripcion = @Descripcion
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_EliminarEscuela')
BEGIN
    EXEC('CREATE PROCEDURE sp_EliminarEscuela
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        DELETE FROM Escuelas WHERE Id = @Id;
    END');
END;

-- Stored Procedures para Profesores
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CrearProfesor')
BEGIN
    EXEC('CREATE PROCEDURE sp_CrearProfesor
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @ApellidoPaterno NVARCHAR(100),
        @ApellidoMaterno NVARCHAR(100) = NULL,
        @EscuelaId UNIQUEIDENTIFIER
    AS
    BEGIN
        INSERT INTO Profesores (Id, Nombre, ApellidoPaterno, ApellidoMaterno, EscuelaId)
        VALUES (@Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @EscuelaId);
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ObtenerProfesor')
BEGIN
    EXEC('CREATE PROCEDURE sp_ObtenerProfesor
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, EscuelaId
        FROM Profesores
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ActualizarProfesor')
BEGIN
    EXEC('CREATE PROCEDURE sp_ActualizarProfesor
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @ApellidoPaterno NVARCHAR(100),
        @ApellidoMaterno NVARCHAR(100) = NULL,
        @EscuelaId UNIQUEIDENTIFIER
    AS
    BEGIN
        UPDATE Profesores
        SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, EscuelaId = @EscuelaId
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_EliminarProfesor')
BEGIN
    EXEC('CREATE PROCEDURE sp_EliminarProfesor
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        DELETE FROM Profesores WHERE Id = @Id;
    END');
END;

-- Stored Procedures para Alumnos
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CrearAlumno')
BEGIN
    EXEC('CREATE PROCEDURE sp_CrearAlumno
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @ApellidoPaterno NVARCHAR(100),
        @ApellidoMaterno NVARCHAR(100) = NULL,
        @FechaNacimiento DATE
    AS
    BEGIN
        INSERT INTO Alumnos (Id, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento)
        VALUES (@Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento);
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ObtenerAlumno')
BEGIN
    EXEC('CREATE PROCEDURE sp_ObtenerAlumno
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento
        FROM Alumnos
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ActualizarAlumno')
BEGIN
    EXEC('CREATE PROCEDURE sp_ActualizarAlumno
        @Id UNIQUEIDENTIFIER,
        @Nombre NVARCHAR(100),
        @ApellidoPaterno NVARCHAR(100),
        @ApellidoMaterno NVARCHAR(100) = NULL,
        @FechaNacimiento DATE
    AS
    BEGIN
        UPDATE Alumnos
        SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, FechaNacimiento = @FechaNacimiento
        WHERE Id = @Id;
    END');
END;

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_EliminarAlumno')
BEGIN
    EXEC('CREATE PROCEDURE sp_EliminarAlumno
        @Id UNIQUEIDENTIFIER
    AS
    BEGIN
        DELETE FROM Alumnos WHERE Id = @Id;
    END');
END;

-- Insertar datos iniciales (opcional)
IF NOT EXISTS (SELECT * FROM Escuelas)
BEGIN
    INSERT INTO Escuelas (Id, Nombre, Descripcion)
    VALUES (NEWID(), 'Escuela de Música Beethoven', 'Clases de piano y violín.'),
           (NEWID(), 'Escuela de Arte Mozart', 'Clases de guitarra y canto.');
END;