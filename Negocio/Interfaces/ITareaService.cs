using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface ITareaService
    {
        Task<Tarea> CrearTareaAsync(Tarea tarea);
        Task<List<Tarea>> ObtenerTodasTareasAsync(Estado? estado = null);
        Task<Tarea> ActualizarTareaAsync(int id, Tarea tareaActualizada);
        Task<bool> EliminarTareaAsync(int id);
    }
}
