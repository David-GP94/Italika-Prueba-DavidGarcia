using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Entities
{
    public class Alumno
    {
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage ="El nombre del alumno es requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido Paterno del alumno es requerido")]
        public required string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "La Fecha de nacimiento del alumno es requerida")]
        public required DateTime FechaNacimiento { get; set; }
        public List<Profesor> Profesores { get; set; } = new();
        public List<Escuela> Escuelas { get; set; } = new();
    }
}
