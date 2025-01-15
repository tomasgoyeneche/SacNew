using Core.Interfaces;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IMenuAltasView : IViewConMensajes
    {
        // Métodos para manipular la UI desde el Presenter
        void MostrarEntidades<T>(List<T> entidades);

        // Propiedades de entrada del usuario
        string TextoBusqueda { get; }
    }
}