using Core.Interfaces;
using Shared.Models;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public interface IIngresoManualYPFView : IViewConMensajes
    {
        DateTime FechaVale { get; }
        List<TicketInfo> Tickets { get; }
        TicketInfo TicketUrea { get; }
        int TipoGasoilSeleccionado { get; }

        void CargarTiposGasoil(List<Concepto> tiposGasoil);

        bool HayConsumosValidos();
    }
}