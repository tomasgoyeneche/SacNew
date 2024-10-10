using SacNew.Models;

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