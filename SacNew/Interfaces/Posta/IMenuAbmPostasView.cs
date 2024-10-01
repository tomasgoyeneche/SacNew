using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IMenuAbmPostasView
    {
        // Métodos para manipular la UI desde el Presenter
        void MostrarPostas(List<Posta> postas);

        void MostrarMensaje(string mensaje);

        // Propiedades de entrada del usuario
        string TextoBusqueda { get; }
    }
}