using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Entities
{
    public class Escuela
    {
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage = "El nombre de la escuela es requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage ="La descripcion de la escuela es requerida")]
        public required string Descripcion { get; set; }
        public List<Profesor> Profesores { get; set; } = new();
        public List<Alumno> Alumnos { get; set; } = new();
    }
}
