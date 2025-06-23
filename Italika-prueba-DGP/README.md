# Prueba Técnica Italika+ - David Garcia Prado

## Descripción
API REST desarrollada en .NET 8 para gestionar escuelas de música, profesores y alumnos. Implementa una arquitectura limpia por capas con el patrón repositorio, usa SQL Server LocalDB y Entity Framework Core, y proporciona stored procedures para operaciones CRUD. La API está documentada con Swagger, incluyendo ejemplos de payloads y respuestas para cada endpoint.

## Arquitectura
- **Dominio**: Entidades e interfaces de repositorios.
- **Aplicación**: Lógica de negocio y DTOs.
- **Infraestructura**: Repositorios y acceso a datos (EF Core).
- **Presentación**: Controladores de la API REST.

## Requerimientos
- .NET 8 SDK (Descargar desde : https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.411-windows-x64-installer)
- SQL Server LocalDB (incluido con Visual Studio o descargable desde https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Opcional: SQL Server Management Studio (SSMS)

## Instrucciones para ejecutar
1. Clonar el repositorio desde Visual Studio: `git clone https://github.com/David-GP94/Italika-Prueba-DavidGarcia.git`
2. Navegar al directorio: `cd Italika-prueba-DGP\Italika-Prueba.Api`
3. Restaurar dependencias: `dotnet restore`
4. Ejecutar la aplicación: `dotnet run`
5. La base de datos `ItalikaDB` se creará automáticamente en `C:\Users\<SuUsuario>\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB`.
6. Acceder a Swagger en `https://localhost:7116/swagger` para explorar los endpoints.

## Configuración adicional (Opcional)
- Para usar otra instancia de SQL Server, modifique `appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=su-servidor;Database=ItalikaDB;Trusted_Connection=True;"
  }

Notas

Repositorio GitHub: https://github.com/David-GP94/Italika-Prueba-DavidGarcia
La base de datos se inicializa automáticamente.
Contacto: david.garciapr@outlook.com
¡Gracias por evaluar mi proyecto!