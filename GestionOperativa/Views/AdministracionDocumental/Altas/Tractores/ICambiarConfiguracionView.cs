using Core.Interfaces;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public interface ICambiarConfiguracionView : IViewConMensajes
    {
        string ConfiguracionSeleccionada { get; }

        void CargarOpcionesConfiguracion(List<string> configuraciones);

        void SeleccionarConfiguracionActual(string configuracionActual);

        void ConfigurarVistaPorEntidad(string tipoEntidad);
    }
}