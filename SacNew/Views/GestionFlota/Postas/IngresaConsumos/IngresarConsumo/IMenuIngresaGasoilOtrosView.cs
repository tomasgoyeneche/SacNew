using SacNew.Interfaces;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public interface IMenuIngresaGasoilOtrosView : IViewConMensajes, IViewConUsuario
    {
        string NumeroPoc { get; set; }
        string CreditoTotal { get; set; }
        int IdPoc { get; set; } // Exponer el IdPOC desde la vista
        string CreditoDisponible { get; set; }
    }
}