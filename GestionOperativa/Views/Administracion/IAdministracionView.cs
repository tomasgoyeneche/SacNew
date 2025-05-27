using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views
{
    public interface IAdministracionView : IViewConMensajes
    {
        void MostrarGuardia(List<GuardiaDto> guardias);

        void MostrarHistorial(List<GuardiaHistorialDto> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);

        void MostrarResumen(List<(string Descripcion, int Cantidad)> resumen);

        void MostrarDatosTe(TransitoEspecial te);

        void MostrarDatosOtros(GuardiaIngresoOtros otros);

        void MostrarDatosNomina(ChoferDto chofer, TractorDto tractor, SemiDto semi, UnidadDto unidad, string rutaFotoChofer, string rutaFotoUnidad);
    }
}