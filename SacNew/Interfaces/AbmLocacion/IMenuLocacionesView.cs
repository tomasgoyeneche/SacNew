using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IMenuLocacionesView : IViewConMensajes, IViewConUsuario
    {
        string CriterioBusqueda { get; }

        void CargarLocaciones(List<Locacion> locaciones);  // Trabajamos directamente con la entidad

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}