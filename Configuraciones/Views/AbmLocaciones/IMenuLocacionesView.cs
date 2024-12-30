using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public interface IMenuLocacionesView : IViewConMensajes, IViewConUsuario
    {
        string CriterioBusqueda { get; }

        void CargarLocaciones(List<Locacion> locaciones);  // Trabajamos directamente con la entidad

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}