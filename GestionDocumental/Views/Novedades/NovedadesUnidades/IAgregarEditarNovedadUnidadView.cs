using Core.Interfaces;
using Shared.Models;

namespace GestionDocumental.Views.Novedades.NovedadesUnidades
{
    public interface IAgregarEditarNovedadUnidadView : IViewConMensajes
    {
        int IdUnidad { get; }
        int IdMantenimientoEstado { get; }
        DateTime FechaInicio { get; }
        DateTime FechaFin { get; }
        string Observaciones { get; }
        int Odometro { get; }

        void CargarEstados(List<UnidadMantenimientoEstado> estados);

        void CargarUnidades(List<UnidadDto> unidades);

        void Close();

        void MostrarDatosNovedad(UnidadMantenimientoDto novedadUnidad);

        void MostrarDiasAusente(int dias);

        void MostrarAusenciasChofer(string texto);

        void MostrarFechaReincorporacion(DateTime fecha);
    }
}