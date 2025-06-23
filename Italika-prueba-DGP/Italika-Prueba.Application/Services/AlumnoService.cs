using Italika_Prueba.Application.Dtos;
using Italika_Prueba.Domain.Entities;
using Italika_Prueba.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Application.Services
{
    public class AlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IProfesorRepository _profesorRepository;
        private readonly IEscuelaRepository _escuelaRepository;

        public AlumnoService(IAlumnoRepository alumnoRepository, IProfesorRepository profesorRepository, IEscuelaRepository escuelaRepository)
        {
            _alumnoRepository = alumnoRepository;
            _profesorRepository = profesorRepository;
            _escuelaRepository = escuelaRepository;
        }

        public async Task<Guid> CrearAsync(CreateAlumnoDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.ApellidoPaterno))
                throw new ArgumentException("Nombre y Apellido Paterno son requeridos.");

            var alumno = new Alumno
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                FechaNacimiento = dto.FechaNacimiento
            };
            return await _alumnoRepository.CrearAsync(alumno);
        }
        public async Task<IEnumerable<AlumnoDTO>> ObtenerTodosAsync()
        {
            var alumnos = await _alumnoRepository.ObtenerTodosAsync();
            if (alumnos == null) return null;
            return alumnos.Select(a => new AlumnoDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                ApellidoPaterno = a.ApellidoPaterno,
                ApellidoMaterno = a.ApellidoMaterno,
                FechaNacimiento = a.FechaNacimiento
            });
        }
        public async Task<AlumnoDTO?> ObtenerPorIdAsync(Guid id)
        {
            var alumno = await _alumnoRepository.ObtenerPorIdAsync(id);
            if (alumno == null) return null;
            return new AlumnoDTO
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                ApellidoPaterno = alumno.ApellidoPaterno,
                ApellidoMaterno = alumno.ApellidoMaterno,
                FechaNacimiento = alumno.FechaNacimiento
            };
        }

        public async Task ActualizarAsync(Guid id, CreateAlumnoDTO dto)
        {
            var alumno = await _alumnoRepository.ObtenerPorIdAsync(id);
            if (alumno == null)
                throw new ArgumentException("Alumno no encontrado.");

            alumno.Nombre = dto.Nombre;
            alumno.ApellidoPaterno = dto.ApellidoPaterno;
            alumno.ApellidoMaterno = dto.ApellidoMaterno;
            alumno.FechaNacimiento = dto.FechaNacimiento;
            await _alumnoRepository.ActualizarAsync(alumno);
        }

        public async Task EliminarAsync(Guid id)
        {
            var alumno = await _alumnoRepository.ObtenerPorIdAsync(id);
            if (alumno == null)
                throw new ArgumentException("Alumno no encontrado.");
            await _alumnoRepository.EliminarAsync(id);
        }

        public async Task AsignarProfesorAsync(Guid alumnoId, Guid profesorId)
        {
            var profesor = await _profesorRepository.ObtenerPorIdAsync(profesorId);
            if (profesor == null)
                throw new ArgumentException("Profesor no encontrado.");
            await _alumnoRepository.AsignarProfesorAsync(alumnoId, profesorId);
        }

        public async Task AsignarEscuelaAsync(Guid alumnoId, Guid escuelaId)
        {
            var escuela = await _escuelaRepository.ObtenerPorIdAsync(escuelaId);
            if (escuela == null)
                throw new ArgumentException("Escuela no encontrada.");
            await _alumnoRepository.AsignarEscuelaAsync(alumnoId, escuelaId);
        }

        public async Task<IEnumerable<AlumnoConEscuelaDTO>> ObtenerPorProfesorIdAsync(Guid profesorId)
        {
            var profesor = await _profesorRepository.ObtenerPorIdAsync(profesorId);
            if (profesor == null)
                throw new ArgumentException("Profesor no encontrado.");

            var alumnos = await _alumnoRepository.ObtenerPorProfesorIdAsync(profesorId);
            return alumnos.Select(a => new AlumnoConEscuelaDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                ApellidoPaterno = a.ApellidoPaterno,
                ApellidoMaterno = a.ApellidoMaterno,
                FechaNacimiento = a.FechaNacimiento,
                Escuela = a.Escuelas.Select(e => new EscuelaDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion
                }).FirstOrDefault()
            });
        }
    }
}
