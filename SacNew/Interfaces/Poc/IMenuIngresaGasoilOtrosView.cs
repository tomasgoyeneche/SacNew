namespace SacNew.Interfaces
{
    public interface IMenuIngresaGasoilOtrosView : IViewConMensajes, IViewConUsuario
    {
        string NumeroPoc { get; set; }
        string CreditoTotal { get; set; }
        string CreditoDisponible { get; set; }
    }
}