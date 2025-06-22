using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Entities
{
    public class Profesor
    {
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage ="El Nombre del profesor es requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido Paterno del profesor es requerido")]
        public required string ApellidoPaterno { get; set; }
        public  string? ApellidoMaterno { get; set; }
        public Guid EscuelaId { get; set; }
        public Escuela Escuela { get; set; } = null!;
        public List<Alumno> Alumnos { get; set; } = new();
    }
}
