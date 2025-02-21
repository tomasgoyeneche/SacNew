using Core.Interfaces;

namespace GestionOperativa.Views.AdministracionDocumental
{
    public interface IMenuAdministracionDocumentalView : IViewConMensajes
    {
        void MostrarRelevamiento(int cantidad);
    }
}