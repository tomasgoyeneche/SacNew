using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarMovimientoStockDetalleView : IViewConMensajes
    {
        int IdMovimientoStock { get; set; }
        int IdArticulo { get; set; }
        int IdPosta { get; set; }
        decimal Cantidad { get; set; }
        decimal PrecioUnitario { get; set; }
        decimal PrecioTotal { get; set; }
        bool Dolarizado { get; set; }
        void CargarArticulos(List<Articulo> articulos);

        void MostrarArticuloSeleccionado(Articulo articulo);

        void SuspenderEventoArticulo();

        void ReanudarEventoArticulo();

        void CalcularTotal();

        void Cerrar();
    }
}