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
    public class EscuelaService
    {
        private readonly IEscuelaRepository _escuelaRepository;

        public EscuelaService(IEscuelaRepository escuelaRepository)
        {
            _escuelaRepository = escuelaRepository;
        }

        public async Task<Guid> CrearAsync(CreateEscuelaDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre es requerido.");

            var escuela = new Escuela
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };
            return await _escuelaRepository.CrearAsync(escuela);
        }

        public async Task<EscuelaDTO?> ObtenerPorIdAsync(Guid id)
        {
            var escuela = await _escuelaRepository.ObtenerPorIdAsync(id);
            if (escuela == null) return null;
            return new EscuelaDTO
            {
                Id = escuela.Id,
                Nombre = escuela.Nombre,
                Descripcion = escuela.Descripcion
            };
        }

        public async Task ActualizarAsync(Guid id, CreateEscuelaDTO dto)
        {
            var escuela = await _escuelaRepository.ObtenerPorIdAsync(id);
            if (escuela == null)
                throw new ArgumentException("Escuela no encontrada.");

            escuela.Nombre = dto.Nombre;
            escuela.Descripcion = dto.Descripcion;
            await _escuelaRepository.ActualizarAsync(escuela);
        }

        public async Task EliminarAsync(Guid id)
        {
            var escuela = await _escuelaRepository.ObtenerPorIdAsync(id);
            if (escuela == null)
                throw new ArgumentException("Escuela no encontrada.");
            await _escuelaRepository.EliminarAsync(id);
        }

        public async Task<IEnumerable<EscuelaDTO>> ObtenerTodasAsync()
        {
            var escuelas = await _escuelaRepository.ObtenerTodasAsync();
            return escuelas.Select(e => new EscuelaDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion
            });
        }
    }
}
