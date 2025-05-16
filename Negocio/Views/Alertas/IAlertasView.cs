using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views.Alertas
{
    public interface IAlertasView : IViewConMensajes
    {
        void MostrarAlertas(List<AlertaDto> alertas);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}