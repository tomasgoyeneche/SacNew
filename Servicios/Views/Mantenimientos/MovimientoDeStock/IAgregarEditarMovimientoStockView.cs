using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views
{
    public interface IAgregarEditarMovimientoStockView : IViewConMensajes
    {
        int IdMovimientoStock { get; }
        int IdTipoMovimiento { get; set; }
        bool Autorizado { get; set; }
        DateTime FechaEmision { get; set; }
        DateTime? FechaIngreso { get; set; }
        string Observaciones { get; set; }

        List<MovimientoStockDetalleDto> ObtenerDetalles();

        void CargarTiposMovimiento(List<TipoMovimientoStock> tipos);

        void CargarDetalles(List<MovimientoStockDetalleDto> detalles);

        void CargarComprobantes(List<MovimientoComprobanteDto> comprobantes);

        void HabilitarConfirmar(bool habilitar);

        void HabilitarFechaIngreso(bool habilitar);

        void MostrarMensaje(string mensaje);

        void Cerrar();
    }
}