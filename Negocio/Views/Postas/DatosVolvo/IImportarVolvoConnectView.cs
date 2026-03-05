using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.DatosVolvo
{
    public interface IImportarVolvoConnectView
    {
        DateTime PeriodoSeleccionado { get; }
        int QuincenaSeleccionada { get; }


        void MostrarDatos(List<ImportVolvoConnectDto> datos);

        List<ImportVolvoConnectDto> ObtenerDatos();

        void MostrarMensaje(string mensaje);
    }
}