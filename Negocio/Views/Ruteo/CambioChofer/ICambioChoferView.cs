using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface ICambioChoferView : IViewConMensajes
    {
        void CargarChoferes(List<Chofer> choferes);

        void CargarFrancos(List<NovedadesChoferesDto> francos);

        int? IdChoferSeleccionado { get; }
        DateTime FechaCambio { get; }

        string Observacion { get; }
        string NombreChoferSeleccionado { get; } // Para descripción de registro

        void Cerrar();
    }
}