using Core.Interfaces;
using Shared.Models;

namespace GestionDocumental.Views
{
    public interface IAgregarEditarNovedadChoferView : IViewConMensajes
    {
        int IdChofer { get; }
        int IdEstado { get; }
        DateTime FechaInicio { get; }
        DateTime FechaFin { get; }
        string Observaciones { get; }
       // bool Disponible { get; }

        void CargarEstados(List<ChoferTipoEstado> estados);

        void CargarChoferes(List<Chofer> choferes);

        void Close();

        void MostrarMantenimientosUnidad(string texto);

        void MostrarDatosNovedad(NovedadesChoferesDto novedadChofer);

        void MostrarDiasAusente(int dias);

        void MostrarFechaReincorporacion(DateTime fecha);
    }
}