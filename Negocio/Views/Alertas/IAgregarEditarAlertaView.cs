using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IAgregarEditarAlertaView : IViewConMensajes
    {
        int? IdChofer { get; }
        int? IdTractor { get; }
        int? IdSemi { get; }
        string Descripcion { get; }

        void CargarChoferes(List<Chofer> choferes);

        void CargarTractores(List<Tractor> tractores);

        void CargarSemis(List<Semi> semis);

        void Close();

        void MostrarDatosAlerta(Alerta alerta);
    }
}