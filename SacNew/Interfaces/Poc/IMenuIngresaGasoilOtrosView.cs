namespace SacNew.Interfaces
{
    public interface IMenuIngresaGasoilOtrosView : IViewConMensajes, IViewConUsuario
    {
        string NumeroPoc { get; set; }
        string CreditoTotal { get; set; }
        int IdPoc { get; set; } // Exponer el IdPOC desde la vista
        string CreditoDisponible { get; set; }
    }
}