using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IMenuLocacionesView
    {
        string CriterioBusqueda { get; }
        void CargarLocaciones(List<Locacion> locaciones);  // Trabajamos directamente con la entidad
        void MostrarMensaje(string mensaje);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}
