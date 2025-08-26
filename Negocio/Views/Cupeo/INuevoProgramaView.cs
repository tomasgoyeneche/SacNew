using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface INuevoProgramaView : IViewConMensajes
    {
        void MostrarAlertas(List<AlertaDto> alertas);

        void CargarOrigenes(List<Locacion> origenes, int? idOrigenSeleccionado);

        void CargarDestinos(List<Locacion> destinos);

        void CargarProductos(List<Producto> productos);

        void CargarViajesAnteriores (List<VistaPrograma> viajes);
        void CargarCupos(List<int> cupos);

        int? Cupo { get; }
        int? IdOrigenSeleccionado { get; }
        int? IdDestinoSeleccionado { get; }
        int? IdProductoSeleccionado { get; }
        DateTime FechaCarga { get; }
        DateTime FechaEntrega { get; }
        string Albaran { get; }
        string Pedido { get; }
        string Observaciones { get; }

        void Cerrar();
    }
}