using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarArticuloView : IViewConMensajes
    {
        int? IdArticulo { get; }
        string Codigo { get; }
        string Nombre { get; }
        string? Descripcion { get; }
        int IdMedida { get; }
        int IdArticuloFamilia { get; }
        int? IdArticuloMarca { get; }
        int? IdArticuloModelo { get; }
        decimal PrecioUnitario { get; }
        decimal? PedidoMinimo { get; }
        decimal? PedidoMaximo { get; }

        decimal? StockCritico { get; }

        void CargarMedidas(List<Medida> medidas);

        void CargarFamilias(List<ArticuloFamilia> familias);

        void CargarMarcas(List<ArticuloMarca> marcas);

        void CargarModelos(List<ArticuloModelo> modelos);

        void LimpiarFormulario();

        int ObtenerCantidad();

        void MostrarDatosArticulo(Articulo articulo, OrdenTrabajoArticulo? orden = null);

        void Cerrar();
    }
}