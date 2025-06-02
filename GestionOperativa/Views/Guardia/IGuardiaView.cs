using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa
{
    public interface IGuardiaView : IViewConMensajes
    {
        void MostrarGuardia(List<GuardiaDto> guardias);

        void MostrarResumen(List<(string Descripcion, int Cantidad)> resumen);

        void MostrarHistorial(List<GuardiaHistorialDto> historial);

        void MostrarResumenParador(int unidades, int tractores, int semis, int choferes);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);

        string PatenteIngresada { get; set; }
        DateTime? FechaManual { get; } // null si no se eligió
        DateTime? FechaSalidaManual { get; } // null si no se eligió
    }
}