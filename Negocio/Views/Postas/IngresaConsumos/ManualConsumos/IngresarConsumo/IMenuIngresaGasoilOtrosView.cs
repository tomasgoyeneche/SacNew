using Core.Interfaces;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public interface IMenuIngresaGasoilOtrosView : IViewConMensajes, IViewConUsuario
    {
        string NumeroPoc { get; set; }
        string CreditoTotal { get; set; }
        int IdPoc { get; set; } // Exponer el IdPOC desde la vista
        string CreditoDisponible { get; set; }
        string CreditoConsumido { get; set; }

        void MostrarConsumos(List<ConsumosUnificadosDto> consumos, POCDto poc);

        string CreditoEnPoc { get; set; }
    }
}