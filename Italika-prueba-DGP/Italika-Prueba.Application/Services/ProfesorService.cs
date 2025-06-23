using Italika_Prueba.Application.Dtos;
using Italika_Prueba.Domain.Entities;
using Italika_Prueba.Domain.Interfaces;
using Italika_Prueba.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Application.Services
{
    public class ProfesorService
    {
        private readonly IProfesorRepository _profesorRepository;
        private readonly IEscuelaRepository _escuelaRepository;

        public ProfesorService(IProfesorRepository profesorRepository, IEscuelaRepository escuelaRepository)
        {
            _profesorRepository = profesorRepository;
            _escuelaRepository = escuelaRepository;
        }

        public async Task<Guid> CrearAsync(CreateProfesorDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.ApellidoPaterno))
                throw new ArgumentException("Nombre y Apellido Paterno son requeridos.");

            var escuela = await _escuelaRepository.ObtenerPorIdAsync(dto.EscuelaId);
            if (escuela == null)
                throw new ArgumentException("Escuela no encontrada.");

            var profesor = new Profesor
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                EscuelaId = dto.EscuelaId
            };
            return await _profesorRepository.CrearAsync(profesor);
        }

        public async Task<IEnumerable<ProfesorDTO>> ObtenerTodosAsync()
        {
            var profesores = await _profesorRepository.ObtenerTodosAsync();
            if (profesores == null) return null;
            return profesores.Select(p => new ProfesorDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                EscuelaId = p.EscuelaId
            });
        }

        public async Task<ProfesorDTO?> ObtenerPorIdAsync(Guid id)
        {
            var profesor = await _profesorRepository.ObtenerPorIdAsync(id);
            if (profesor == null) return null;
            return new ProfesorDTO
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                ApellidoPaterno = profesor.ApellidoPaterno,
                ApellidoMaterno = profesor.ApellidoMaterno,
                EscuelaId = profesor.EscuelaId
            };
        }

        public async Task ActualizarAsync(Guid id, CreateProfesorDTO dto)
        {
            var profesor = await _profesorRepository.ObtenerPorIdAsync(id);
            if (profesor == null)
                throw new ArgumentException("Profesor no encontrado.");

            var escuela = await _escuelaRepository.ObtenerPorIdAsync(dto.EscuelaId);
            if (escuela == null)
                throw new ArgumentException("Escuela no encontrada.");

            profesor.Nombre = dto.Nombre;
            profesor.ApellidoPaterno = dto.ApellidoPaterno;
            profesor.ApellidoMaterno = dto.ApellidoMaterno;
            profesor.EscuelaId = dto.EscuelaId;
            await _profesorRepository.ActualizarAsync(profesor);
        }

        public async Task EliminarAsync(Guid id)
        {
            var profesor = await _profesorRepository.ObtenerPorIdAsync(id);
            if (profesor == null)
                throw new ArgumentException("Profesor no encontrado.");
            await _profesorRepository.EliminarAsync(id);
        }

        public async Task<ProfesorConAlumnosDTO?> ObtenerConAlumnosYEscuelaAsync(Guid id)
        {
            var profesor = await _profesorRepository.ObtenerConAlumnosYEscuelaAsync(id);
            if (profesor == null) return null;

            return new ProfesorConAlumnosDTO
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                ApellidoPaterno = profesor.ApellidoPaterno,
                ApellidoMaterno = profesor.ApellidoMaterno,
                Escuela = new EscuelaDTO
                {
                    Id = profesor.Escuela.Id,
                    Nombre = profesor.Escuela.Nombre,
                    Descripcion = profesor.Escuela.Descripcion
                },
                Alumnos = profesor.Alumnos.Select(a => new AlumnoDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    FechaNacimiento = a.FechaNacimiento
                }).ToList()
            };
        }
    }
}
