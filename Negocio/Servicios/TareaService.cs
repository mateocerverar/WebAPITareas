using Datos.Entidades;
using Datos;
using Microsoft.EntityFrameworkCore;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly AppDbContext _context;

        public TareaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Tarea> CrearTareaAsync(Tarea tarea)
        {
            if (string.IsNullOrWhiteSpace(tarea.Titulo))
                throw new ArgumentException("El título es obligatorio.");

            if (tarea.FechaVencimiento < DateTime.Today)
                throw new ArgumentException("La fecha de vencimiento no puede ser anterior al día actual.");

            bool codigoExiste = await _context.Tareas.AnyAsync(t => t.Codigo == tarea.Codigo);
            if (codigoExiste)
                throw new InvalidOperationException("El código ya está registrado.");

            tarea.FechaCreacion = DateTime.Now;

            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }
        public async Task<List<Tarea>> ObtenerTodasTareasAsync(Estado? estado = null)
        {
            IQueryable<Tarea> query = _context.Tareas.AsQueryable();

            if (estado.HasValue)
                query = query.Where(t => t.Estado == estado.Value);

            return await query.ToListAsync();
        }
        public async Task<Tarea> ActualizarTareaAsync(int id, Tarea tareaActualizada)
        {
            var tareaExistente = await _context.Tareas.FindAsync(id);
            if (tareaExistente == null)
                throw new KeyNotFoundException("Tarea no encontrada.");

            if (tareaExistente.Codigo != tareaActualizada.Codigo)
            {
                bool codigoExiste = await _context.Tareas.AnyAsync(t => t.Codigo == tareaActualizada.Codigo);
                if (codigoExiste)
                    throw new InvalidOperationException("El nuevo código ya está en uso.");
            }

            tareaExistente.Titulo = tareaActualizada.Titulo;
            tareaExistente.Descripcion = tareaActualizada.Descripcion;
            tareaExistente.FechaVencimiento = tareaActualizada.FechaVencimiento;
            tareaExistente.Estado = tareaActualizada.Estado;
            tareaExistente.Codigo = tareaActualizada.Codigo;

            await _context.SaveChangesAsync();
            return tareaExistente;
        }
        public async Task<bool> EliminarTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
                throw new KeyNotFoundException("Tarea no encontrada.");

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
