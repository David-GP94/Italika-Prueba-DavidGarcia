using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Application.Dtos
{
    public class ProfesorConAlumnosDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string? ApellidoMaterno { get; set; }
        public EscuelaDTO Escuela { get; set; } = null!;
        public List<AlumnoDTO> Alumnos { get; set; } = new();
    }
    public class AlumnoConEscuelaDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string? ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public EscuelaDTO? Escuela { get; set; }
    }
}
