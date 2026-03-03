using Core.Interfaces;
using Shared.Models;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public interface IIngresaGasoilView : IViewConMensajes
    {
        Concepto TipoGasoilSeleccionado { get; }
        decimal? Litros { get; }
        string NumeroVale { get; }
        string Observaciones { get; }
        DateTime FechaCarga { get; }

        bool Dolar { get; }

        void MostrarLitrosAutorizados(decimal litrosAutorizados, decimal kilometros, string origen, string destino, string albaran);

        void InicializarParaEdicion(ConsumoGasoil consumo);

        void CargarTiposGasoil(List<Concepto> tiposGasoil, string poc, POCDto pocdto);

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