using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public interface IIngresaGasoilView : IViewConMensajes
    {
        Concepto TipoGasoilSeleccionado { get; }
        decimal? Litros { get; }
        string NumeroVale { get; }
        string Observaciones { get; }
        DateTime FechaCarga { get; }

        void MostrarLitrosAutorizados(decimal litrosAutorizados, decimal kilometros);

        void InicializarParaEdicion(ConsumoGasoil consumo);

        void CargarTiposGasoil(List<Concepto> tiposGasoil);

        bool ConfirmarGuardado(string mensaje);

        void MostrarTotalCalculado(decimal total);

        // Mostrar Consumos en la DataGridView
        void MostrarConsumosTotales(List<ConsumoGasoilAutorizadoDto> consumos);

        void MostrarConsumosAnteriores(List<ConsumoGasoilAutorizadoDto> consumos);

        void ActualizarLabelAnterior(decimal restante);

        void ActualizarLabelTotal(decimal restante);

        void Cerrar();
    }
}