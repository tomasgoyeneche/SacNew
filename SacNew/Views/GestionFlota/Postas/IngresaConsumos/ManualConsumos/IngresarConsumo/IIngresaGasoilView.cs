using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public interface IIngresaGasoilView : IViewConMensajes
    {
        Concepto TipoGasoilSeleccionado { get; }
        decimal? Litros { get; }
        string NumeroVale { get; }
        string Observaciones { get; }
        DateTime FechaCarga { get; }

        void CargarTiposGasoil(List<Concepto> tiposGasoil);

        void MostrarTotalCalculado(decimal total);

        void Cerrar();
    }
}