using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views.Ruteo
{
    public interface ICargaRemitoView : IViewConMensajes
    {
        void CargarLocaciones(List<Locacion> locaciones);

        void CargarProductos(List<Producto> productos);

        void CargarMedidas(List<Medida> medidas);

        void CargarDatosIniciales(Programa programa, Shared.Models.Ruteo ruteo, string tipoRemito);

        string RemitoNumero { get; }
        DateTime? FechaRemito { get; }
        int? IdMedida { get; }
        int? Cantidad { get; }
        int? IdProducto { get; }
        int? IdOrigen { get; }

        int? IdDestino { get; }

        void Close();
    }
}