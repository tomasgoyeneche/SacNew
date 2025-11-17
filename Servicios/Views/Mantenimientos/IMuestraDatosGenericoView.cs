using Core.Interfaces;

namespace Servicios.Views.Mantenimientos
{
    public interface IMuestraDatosGenericoView : IViewConMensajes
    {
        void CargarDatos<T>(List<T> datos);

        void MostrarTitulo(string titulo);
    }
}