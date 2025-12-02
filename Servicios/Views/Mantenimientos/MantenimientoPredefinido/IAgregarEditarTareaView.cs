using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimientos
{
    public interface IAgregarEditarTareaView : IViewConMensajes
    {
        int IdTarea { get; set; }
        string TipoVista { get; set; }
        string Codigo { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        decimal Horas { get; set; }
        decimal ManoObra { get; set; }
        decimal Repuestos { get; set; }

        int IdArticuloSeleccionado { get; }
        decimal CantidadArticulo { get; }

        void MostrarMovimientoStock(bool estado);

        void CargarArticulos(List<Articulo> articulos);

        void CargarArticulosAsociados(List<TareaArticuloDto> articulos);

        void Cerrar();
    }
}