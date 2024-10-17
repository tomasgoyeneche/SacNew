using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IMenuAbmPostasView : IViewConMensajes
    {
        // Métodos para manipular la UI desde el Presenter
        void MostrarPostas(List<Posta> postas);

        // Propiedades de entrada del usuario
        string TextoBusqueda { get; }
    }
}