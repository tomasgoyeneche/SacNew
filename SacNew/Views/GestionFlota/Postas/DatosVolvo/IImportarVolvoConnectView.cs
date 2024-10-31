using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.DatosVolvo
{
    public interface IImportarVolvoConnectView
    {
        Periodo PeriodoSeleccionado { get; }

        void CargarPeriodos(IEnumerable<Periodo> periodos);

        void MostrarDatos(List<ImportVolvoConnect> datos);

        List<ImportVolvoConnect> ObtenerDatos();

        void MostrarMensaje(string mensaje);
    }
}